using System;
using System.Collections.Generic;

namespace ET
{
    public class DlgGameLevelStoryLayer: Entity, IAwake
    {
        public DlgGameLevelStoryLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgGameLevelStoryLayerViewComponent>();
            
            
            
        }

        public Queue<string> ContentQuene = new Queue<string>();


        public ETCancellationToken EtCancellationToken;

        public bool IsAutoPlay = false; //	是否在自动播放
    }
}