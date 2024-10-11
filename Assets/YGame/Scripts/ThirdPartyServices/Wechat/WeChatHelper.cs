using System;
using UnityEngine;
using WeChatWASM;

namespace YGame.Scripts.ThirdPartyServices.Wechat
{
    public partial class WeChatHelper
    {
        /// <summary>
        /// 初始化SDK
        /// </summary>
        public static void InitSDK(Action<int> onInitSuccess)
        {
            WX.InitSDK(onInitSuccess);
        }

        /// <summary>
        /// 玩家登录
        /// </summary>
        /// <param name="timeout">登录超时时间</param>
        /// <param name="onSuccess">成功回调</param>
        /// <param name="onFail">失败回调</param>
        /// <param name="onComplete">完成回调</param>
        public static void Login(int timeout = 10000, Action<LoginSuccessCallbackResult> onSuccess = null, Action<RequestFailCallbackErr> onFail = null, Action<GeneralCallbackResult> onComplete = null)
        {
            LoginOption option = new()
            {
                timeout = timeout,
                success = onSuccess,
                fail = onFail,
                complete = onComplete,
            };

            WX.Login(option);
        }
        
        /// <summary>
        /// 创建全屏用户信息按钮
        /// </summary>
        /// <param name="type"></param>
        /// <param name="style"></param>
        /// <param name="textContent"></param>
        /// <param name="image"></param>
        /// <param name="withCredentials"></param>
        /// <param name="lang"></param>
        public static WXUserInfoButton CreateUserInfoButton(string textContent = "", string image = "", bool withCredentials = true, string lang = "en")
        {
            return WX.CreateUserInfoButton(0, 0, Screen.width, Screen.height, "en", false);

            //return null;
        }
        
        /// <summary>
        /// 获取玩家信息
        /// 权限相关的代码需要在外部自己调用
        /// </summary>
        /// <param name="withCredentials">是否带上登录态信息(为true的时候需要在调用这个接口前调用过wx.login接口)</param>
        /// <param name="lang">用户使用的语言</param>
        /// <param name="onSuccess">成功回调</param>
        /// <param name="onFail">失败回调</param>
        /// <param name="onComplete">完成回调</param>
        public static void GetUserInfo(bool withCredentials = false, string lang = "en", Action<GetUserInfoSuccessCallbackResult> onSuccess = null, Action<GeneralCallbackResult> onFail = null, Action<GeneralCallbackResult> onComplete = null)
        {
            GetUserInfoOption option = new()
            {
                withCredentials = withCredentials,
                lang = lang,
                success = onSuccess,
                fail = onFail,
                complete = onComplete,
            };

            WX.GetUserInfo(option);
        }
        
        /// <summary>
        /// 获取玩家设置
        /// </summary>
        /// <param name="option"></param>
        public static void GetSetting(GetSettingOption option)
        {
            WX.GetSetting(option);
        }
        
        
        
    }
}