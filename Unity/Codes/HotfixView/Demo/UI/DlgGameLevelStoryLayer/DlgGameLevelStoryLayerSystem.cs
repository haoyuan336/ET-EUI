using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public static class DlgGameLevelStoryLayerSystem
    {
        public static void RegisterUIEvent(this DlgGameLevelStoryLayer self)
        {
            self.View.E_AutoButton.AddListener(() =>
            {
                self.IsAutoPlay = !self.IsAutoPlay;

                self.View.E_AutoMarkText.text = self.IsAutoPlay? "Auto" : "Manual";

                self.View.E_NextButton.gameObject.SetActive(!self.IsAutoPlay);
                if (self.IsAutoPlay)
                {
                    self.ShowNextContent();
                }
            });

            self.View.E_CloseButton.AddListener(self.OnCloseButton);

            self.View.E_NextButton.AddListener(self.OnNextButtonClick);
        }

        public static async ETTask ShowContentAsync(this DlgGameLevelStoryLayer self, string storyConternt)
        {
            await self.SetContent(storyConternt);
            self.CurrentTask = ETTask.Create();
            await self.CurrentTask;
        }

        public static void OnCloseButton(this DlgGameLevelStoryLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_GameLevelStoryLayer);

            if (self.CurrentTask != null)
            {
                self.CurrentTask.SetResult();
            }
        }

        public static void OnNextButtonClick(this DlgGameLevelStoryLayer self)
        {
            //下一句按钮
            self.ShowNextContent();
        }

        public static async void ShowNextContent(this DlgGameLevelStoryLayer self)
        {
            if (self.ContentQuene.Count <= 0)
            {
                self.OnCloseButton();

                return;
            }

            if (self.EtCancellationToken != null)
            {
                self.EtCancellationToken.Cancel();
                self.EtCancellationToken = null;
            }

            var sentenceIdStr = self.ContentQuene.Dequeue();

            SentenceConfig config = SentenceConfigCategory.Instance.Get(int.Parse(sentenceIdStr));

            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(config.AtlasImage, config.HeadImage);

            // self.View.E_
            self.View.E_HeadImage.sprite = sprite;
            self.View.E_ContentText.text = config.SentenceContent;
            await TimerComponent.Instance.WaitAsync(3000, self.EtCancellationToken);
            if (self.IsAutoPlay)
            {
                self.ShowNextContent();
            }
        }

        public static void ShowWindow(this DlgGameLevelStoryLayer self, Entity contextData = null)
        {
        }

        public static async ETTask SetContent(this DlgGameLevelStoryLayer self, string content)
        {
            List<string> list = content.Split(',').ToList();
            self.ContentQuene.Clear();
            foreach (var str in list)
            {
                self.ContentQuene.Enqueue(str);
            }

            // self.ShowContent();
            self.ShowNextContent();

            await ETTask.CompletedTask;
        }
    }
}