using UnityEngine;
using UnityEngine.Serialization;

namespace YGame.Scripts.Config
{
    
    [System.Serializable]
    [CreateAssetMenu(fileName = "LeaderboardConfig", menuName = "YGame/LeaderboardConfig")]
    public class LeaderboardConfig: ScriptableObject,IConfig
    {
        public string editorLeaderboardSlugName;
        public string weChatLeaderboardSlugName;

    }
}