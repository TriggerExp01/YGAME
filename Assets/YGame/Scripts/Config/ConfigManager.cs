using YGame.Scripts.Addressable;
using YGame.Scripts.Common;

namespace YGame.Scripts.Config
{
    public class ConfigManager : MonoSingleton<ConfigManager>
    {
        public WeChatConfig WeChatConfig { get; private set; }
        
        public void InitializeConfig()
        {
            this.LoadConfigAsync<WeChatConfig>(nameof(Config.WeChatConfig) , (config) => { this.WeChatConfig = config as WeChatConfig; });
        }
    }
}