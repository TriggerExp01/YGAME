using System.Collections.Generic;
using System.Linq;
using Achievement;
using Cysharp.Threading.Tasks;
using Unity.Passport.Runtime;
using UnityEngine;
using YGame.Scripts.Log;
using Action = System.Action;

namespace YGame.Scripts.ThirdPartyServices.UOS
{
    public class AchievementType
    {
        public const string AchievementLogin = "login";
        public const string AchievementPlayGame = "playGame";
        public const string AchievementWinCount = "winCount";
        public const string AchievementWatchAD = "watch";
        public const string AchievementShare = "share";
    }
    public partial class PassportHelper
    {
        private static List<AchievementInfoExpanded> _achievementInfos = new List<AchievementInfoExpanded>();
        private static List<AchievementInfoExpanded> _achievementNotCompletedInfos = new List<AchievementInfoExpanded>();
        
        public static async UniTask FetchAchievement()
        {
            Achievement.FetchAchievementResponse achievementList = await PassportFeatureSDK.Achievement.FetchAchievement();
        }
        
        public static async UniTask UpdatePersonaAchievement(string achievementSlug, uint value)
        {
            Achievement.AchievementInfo achievement = await PassportFeatureSDK.Achievement.
                UpdatePersonaAchievement(achievementSlug, Achievement.Action.Increase, value);
            YLogger.LogInfo("Updated Achievement: " + achievement.SlugName + " with value: " + value);
        }

        public static async UniTask<List<AchievementInfoExpanded>> GetNotCompletedAchievementByType(string achievementType)
        {
            ListPersonaAchievementsResponse personaAchievements = await 
                PassportFeatureSDK.Achievement.ListPersonaAchievements(achievementType,false,0,100);
            _achievementNotCompletedInfos.Clear();
            if (personaAchievements.Achievements.Count > 0)
            {
                _achievementNotCompletedInfos.AddRange(personaAchievements.Achievements);
            }
            return _achievementNotCompletedInfos;
        }
        
        public static async UniTask<List<AchievementInfoExpanded>> GetPersonaAchievement()
        {
            ListPersonaAchievementsResponse personaAchievements = await 
                PassportFeatureSDK.Achievement.ListPersonaAchievements(null,null,0,20);
            _achievementInfos.Clear();
            var completedList = personaAchievements.Achievements.ToList().FindAll(x => x.Completed);
            var unCompletedList = personaAchievements.Achievements.ToList().FindAll(x => !x.Completed);
            completedList.AddRange(unCompletedList);
            _achievementInfos.AddRange(completedList);
            YLogger.LogInfo("Got Persona Achievements");
            return _achievementInfos;
        }
        
    }
}