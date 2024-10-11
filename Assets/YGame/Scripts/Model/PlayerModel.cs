using YGame.Scripts.Interface;

namespace YGame.Scripts.Model
{
    public class PlayerModel : IResigter
    {
        public void Register()
        {
           GlobalModel.PlayerModel = this;
        }

        public void UnRegister()
        {
        }
    }
}