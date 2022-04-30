using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ET
{
    public class HeroCardObjectComponentAwakeSystem: AwakeSystem<HeroCardObjectComponent, HeroCard, HeroCardInfo>
    {
        public override async void Awake(HeroCardObjectComponent self, HeroCard heroCard, HeroCardInfo heroCardInfo)
        {
            //load herocard assetbound
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("HeroCard");
            GameObject card = GameObject.Instantiate(prefab);
            card.name = "Card" + IdGenerater.Instance.GenerateId();
            self.HeroCard = card;
            // self.UpdateHeroCardTextView();
            card.transform.position = new Vector3(-3 + heroCard.InTroopIndex * 3, 0, -7 * (heroCard.CampIndex == 0? 1 : -1));
            self.UpdateHeroCardTextView(heroCardInfo);
            await ETTask.CompletedTask;
        }
    }

    public static class HeroCardObjectComponentSystem
    {
        public static void UpdateHeroCardTextView(this HeroCardObjectComponent self, HeroCardInfo heroCardInfo)
        {
            // Log.Debug($"update hero card text view  {heroCardInfo.Attack}");
            //更新英雄卡牌
            // Log.Debug($"hero c ard name {self.HeroCard.name}");
            var attackText = GameObject.Find($"{self.HeroCard.name}/AttackText");
            var angryText = GameObject.Find($"{self.HeroCard.name}/AngryText");
            var HPText = GameObject.Find($"{self.HeroCard.name}/HPText");
            var DiamondAttackText = GameObject.Find($"{self.HeroCard.name}/DiamondAttackText");
            attackText.GetComponent<TextMesh>().text = $"hellow";

            HeroCard heroCard = self.GetParent<HeroCard>();
            attackText.GetComponent<TextMesh>().text = $"A:{heroCardInfo.Attack.ToString()}";
            angryText.GetComponent<TextMesh>().text = $"Y:{heroCardInfo.Angry.ToString()}";
            HPText.GetComponent<TextMesh>().text = $"HP:{heroCardInfo.HP.ToString()}";
            DiamondAttackText.GetComponent<TextMesh>().text = $"DA:{heroCardInfo.DiamondAttack.ToString()}";
        }
    }
}