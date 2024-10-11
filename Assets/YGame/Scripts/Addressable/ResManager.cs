using System;
using UnityEngine;
using UnityEngine.UI;
using YGame.Scripts.Config;
using YGame.Scripts.Log;
using YGame.Scripts.UI;
using Object = UnityEngine.Object;

namespace YGame.Scripts.Addressable
{
    public static class ResManager
    {
        public static void LoadSpriteAsync(this Image img, string address)
        {
            AddressableLoader.Instance.LoadAsset<Sprite>(address, (sprite) =>
            {
                img.sprite = sprite;
                YLogger.LogInfo("LoadSpriteAsync Success:" + address);
            });
        }

        public static void LoadUIAsync(this UIManager ui, string  address, Action<UIBase> callback = null)
        {
            AddressableLoader.Instance.LoadAsset<UIBase>(address, (go) =>
            {
                if (go != null)
                {
                    callback?.Invoke(go);
                    YLogger.LogInfo("LoadUIAsync Success:" + address);
                }
            });
        }
        
        public static void LoadConfigAsync<T>(this ConfigManager config,string address, Action<IConfig> callback = null) where T : Object, IConfig
        {
            AddressableLoader.Instance.LoadAsset<T>(address, (go) =>
            {
                if (go != null)
                {
                    callback?.Invoke(go);
                    YLogger.LogInfo("LoadConfigAsync Success:" + address);
                }
            });
        }
    }
}