using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayAddAttackAngryViewAnimHandler: AEvent<EventType.PlayAddAttackAngryViewAnim>
    {
        protected override  async ETTask Run(PlayAddAttackAngryViewAnim a)
        {
            Diamond diamond = a.Diamond;
            HeroCard heroCard = a.HeroCard;
            GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>("DiamondAddValueTrailEffect");

            GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);

            Vector3 startPos = diamond.GetComponent<GameObjectComponent>().GameObject.transform.position + Vector3.back * 0.1f;
            Vector3 endPos = heroCard.GetComponent<GameObjectComponent>().GameObject.transform.position + Vector3.back * 0.1f;
            go.transform.position = startPos;
            float distance = 1;
            while (distance > 0.1f)
            {
                Vector3 prePos = Vector3.Lerp(go.transform.position, endPos, 0.05f);
                go.transform.position = prePos;
                distance = Vector3.Distance(prePos, endPos);
                await TimerComponent.Instance.WaitFrameAsync();
            }
            
            HeroCardViewCtl heroCardViewCtl = heroCard.GetComponent<GameObjectComponent>().GameObject.GetComponent<HeroCardViewCtl>();
            heroCardViewCtl.UpdateAttackView(a.EndAttack);
            heroCardViewCtl.UpdateAngryView(a.EndAttack);
            await ETTask.CompletedTask;

        }
    }
}