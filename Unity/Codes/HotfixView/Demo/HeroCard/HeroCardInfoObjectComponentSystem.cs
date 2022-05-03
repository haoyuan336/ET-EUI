using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class HeroCardInfoObjectComponentSystem: AwakeSystem<HeroCardInfoObjectComponent>
    {
        public override async void Awake(HeroCardInfoObjectComponent self)
        {
            GameObject prefab =
                    await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/CustomUI/HeroCardInfoUI.prefab");
            self.GameObject = GameObject.Instantiate(prefab, GlobalComponent.Instance.OtherRoot.transform);
            self.HeroMode = self.GetParent<HeroModeObjectCompoent>().HeroMode;

            self.GameObject.name = self.Id.ToString();
            self.HpBarImage = GameObject.Find($"{self.GameObject.name}/HpProgress/Bar");
            self.AttackBarImage = GameObject.Find($"{self.GameObject.name}/AttackProgress/Bar");
            self.AngryBarImage = GameObject.Find($"{self.GameObject.name}/AngryProgress/Bar");

            var HeroColorMark = GameObject.Find($"{self.GameObject.name}/HeroColorMark");

            HeroCard heroCard = self.GetParent<HeroModeObjectCompoent>().GetParent<HeroCard>();

            DiamondTypeConfig diamondTypeConfig = heroCard.GetHeroCardColor();
            var colorStr = diamondTypeConfig.ColorValue;
            Color color = ColorTool.HexToColor(colorStr);
            HeroColorMark.GetComponent<Image>().color = color;
            
            self.HpBarImage.GetComponent<Image>().fillAmount = 0;
            self.AngryBarImage.GetComponent<Image>().fillAmount = 0;
            // self.HeroHeight = self.HeroMode.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y;
            self.HeroHeight = 0;
        }
    }

    public class HeroCardInfoObjectComponentUpdateSystem: UpdateSystem<HeroCardInfoObjectComponent>
    {
        public override void Update(HeroCardInfoObjectComponent self)
        {
            if (self.GameObject != null && self.HeroMode != null)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(self.HeroMode.transform.position);
                self.GameObject.transform.position = pos;
            }
        }
    }
}