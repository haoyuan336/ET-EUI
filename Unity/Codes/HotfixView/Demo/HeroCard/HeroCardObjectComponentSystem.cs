using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ET
{
    public class HeroCardObjectComponentAwakeSystem: AwakeSystem<HeroCardObjectComponent, HeroCard>
    {
        public override async void Awake(HeroCardObjectComponent self, HeroCard heroCard)
        {
            //load herocard assetbound
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("HeroCard");
            GameObject card = GameObject.Instantiate(prefab);
            card.name = "Card" + TimeHelper.ClientFrameTime().ToString();
            self.HeroCard = card;
            self.UpdateHeroCardTextView();
            card.transform.position = new Vector3(-3 + heroCard.InTroopIndex * 3, 0, -7 * (heroCard.CampIndex == 0? 1 : -1));
            await ETTask.CompletedTask;
        }
    }

    public static class HeroCardObjectComponentSystem
    {
        public static void UpdateHeroCardTextView(this HeroCardObjectComponent self)
        {
            //更新英雄卡牌
            var attackText = GameObject.Find($"{self.HeroCard.name}/AttackText");
            var angryText = GameObject.Find($"{self.HeroCard.name}/AngryText");
            var HPText = GameObject.Find($"{self.HeroCard.name}/HPText");
            var DiamondAttackText = GameObject.Find($"{self.HeroCard.name}/DiamondAttackText");
            // attackText.GetComponent<Text>().text = diamond.
            HeroCard heroCard = self.GetParent<HeroCard>();
            attackText.GetComponent<TextMesh>().text = $"A:{heroCard.Attack.ToString()}";
            angryText.GetComponent<TextMesh>().text = $"Y:{heroCard.Angry.ToString()}";
            HPText.GetComponent<TextMesh>().text = $"HP:{heroCard.HP.ToString()}";
            DiamondAttackText.GetComponent<TextMesh>().text = $"DA:{heroCard.DiamondAttack.ToString()}";
        }
    }
}