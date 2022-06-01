using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class ESCommonHeroCard: Entity, IAwake<Transform>, IAwake<Transform, bool>
    {
        public GameObject GameObject;
        public Image E_HeadImage;

        public GameObject E_Add;
        public Text E_LevelText;
        public Image E_ElemanetIconImage;
        public Image E_QaulityIconImage;
        public HeroCardInfo HeroCardInfo;
        public Toggle E_Toggle;
        public List<GameObject> StarList = new List<GameObject>();
        public bool IsCanClick = false;

        public Action OnButtonClick;
    }
}