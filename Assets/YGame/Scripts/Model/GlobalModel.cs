namespace YGame.Scripts.Model
{
    public static class GlobalModel
    {
        public static void RegisterModel()
        {
            new PlayerModel().Register();
        }
        public static PlayerModel PlayerModel { get; set; }
    }
    
    
}