using YGame.Scripts.Config;
using YGame.Scripts.Model;

namespace YGame.Scripts
{
    public static class Application
    {
        public static void YFrameworkInitialize()
        {
            InitializeModel();
            InitializeConfig();
        }

        
        /// <summary>
        /// 初始化Model
        /// </summary>
        private static void InitializeModel()
        {
            GlobalModel.RegisterModel();
        }

        private static void InitializeConfig()
        {
            ConfigManager.Instance.InitializeConfig();
        }

    }
}