using System;
using WeChatWASM;
using YGame.Scripts.Config;

namespace YGame.Scripts.ThirdPartyServices.Wechat
{
    public partial class WeChatHelper
    {
        
        private static VibrateShortOption shortOption = new VibrateShortOption();
        private static VibrateLongOption longOption = new VibrateLongOption();

        public void VibrateShort()
        {
            switch (ConfigManager.Instance.WeChatConfig.weChatShotOptionType)
            {
                case WeChatShotOptionType.Heavy:
                    shortOption.type = "heavy";
                    break;
                case WeChatShotOptionType.Medium:
                    shortOption.type = "medium";
                    break;
                case WeChatShotOptionType.Light:
                    shortOption.type = "light";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            WX.VibrateShort(shortOption);
        }

        public void VibrateLong()
        {
            WX.VibrateLong(longOption);
        }
    }
}