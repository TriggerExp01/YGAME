using System;
using UnityEngine;
using WeChatWASM;
using YGame.Scripts.Config;
using YGame.Scripts.Log;

namespace YGame.Scripts.ThirdPartyServices.Wechat
{
    public partial class WeChatHelper
    {
        private WXRewardedVideoAd _rewardedVideoAd;
        private static string _rewardAdUnitId = "adunit-8e9c880bbf4851b3";
        public void ShowBanner()
        {
            
        }

        public void HideBanner()
        {
        }

        public void ShowInterstitial(Action<bool> callback)
        {
        }


        public void CreateRewardedVideoAd()
        {
            YLogger.LogInfo("CreateRewardedVideoAd");
            _rewardAdUnitId = ConfigManager.Instance.WeChatConfig.weChatADPath;
            var adParam = new WXCreateRewardedVideoAdParam()
            {
                adUnitId = _rewardAdUnitId, //修改AD位ID
                multiton = false
            };
            _rewardedVideoAd = WX.CreateRewardedVideoAd(adParam);
            _rewardedVideoAd.OnLoad((res) =>
            {
                YLogger.LogInfo("LoadRewardedVideoAd Success");
            });
        }

        public void ShowRewardedVideo(Action<bool> callback)
        {
            CreateRewardedVideoAd();
            YLogger.LogInfo("ShowRewardedVideo");
            _rewardedVideoAd.OnClose((res) =>
            {
                if ((res != null && res.isEnded) || res == null)
                {
                    // 正常播放结束，可以下发游戏奖励
                    callback?.Invoke(true);
                }
                else
                {
                    // 播放中途退出，不下发游戏奖励
                    callback?.Invoke(false);
                }
                
            });
            if (_rewardedVideoAd != null)
                _rewardedVideoAd.Show();
        }
    }
}