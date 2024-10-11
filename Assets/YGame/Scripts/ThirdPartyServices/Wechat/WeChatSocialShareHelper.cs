using System;
using UnityEngine;
using WeChatWASM;
using YGame.Scripts.Log;

namespace YGame.Scripts.ThirdPartyServices.Wechat
{
    public partial class WeChatHelper
    {
        public void Share(string title = "", string url = "", string imageId = "", Action<bool> callback = null)
        {
            WX.ShareAppMessage(new ShareAppMessageOption()
            {
                title = title,
                imageUrl = url,
                imageUrlId = imageId,
            });
            callback?.Invoke(true);
        }

        public void ShareScreenShot(string title = "", int imageWidth = 500, int imageHeight = 400, int x = 0, int y = 0, Action<bool> callback = null)
        {
            WXCanvas.ToTempFilePath(new WXToTempFilePathParam()
            {
                x = x,
                y = y,
                width = imageWidth,
                height = imageHeight,
                destWidth = imageWidth,
                destHeight = imageHeight,
                success = (result) =>
                {
                    YLogger.LogInfo("ToTempFilePath success" + JsonUtility.ToJson(result));
                    WX.ShareAppMessage(new ShareAppMessageOption()
                    {
                        title = title,
                        imageUrl = result.tempFilePath,
                    });
                },
                fail = (result) =>
                {
                    YLogger.LogInfo("ToTempFilePath fail" + JsonUtility.ToJson(result));
                    callback?.Invoke(false);
                },
                complete = (result) =>
                {
                    YLogger.LogInfo("ToTempFilePath complete" + JsonUtility.ToJson(result));
                    callback?.Invoke(true);
                },
            });
        }
    }
}