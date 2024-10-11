using System;
using UnityEngine;
using YGame.Scripts.Addressable;
using YGame.Scripts.Interface;

namespace YGame.Scripts.Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "WeChatConfig", menuName = "YGame/WeChatConfig")]
    public class WeChatConfig: ScriptableObject,IConfig
    {
        public string weChatADPath;
        public WeChatShotOptionType weChatShotOptionType;

        #region Share
        public string shareTitle;
        public string shareImageUrl;
        public string shareImageId;
        #endregion
        
        
    }
    [Serializable]
    public enum WeChatShotOptionType
    {
        Heavy,//重
        Medium,//中
        Light//轻
    }
}