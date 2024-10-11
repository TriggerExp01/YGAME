using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Leaderboard;
using Unity.Passport.Runtime;
using UnityEngine;
using YGame.Scripts.Log;

namespace YGame.Scripts.ThirdPartyServices.UOS
{
    public partial class PassportHelper
    {

        private static List<LeaderboardMemberScore> _leaderboardList = new List<LeaderboardMemberScore>();
        public static async UniTask<List<LeaderboardMemberScore>> GetLeaderboard(Action callback,string leaderboardSlugName = "")
        {
            Leaderboard.ListLeaderboardScoresResponse scoreList = await PassportFeatureSDK.Leaderboard.ListLeaderboardScores(leaderboardSlugName);

            _leaderboardList.Clear();
            _leaderboardList.AddRange(scoreList.Scores);
            YLogger.LogInfo("GetLeaderboard  Finish ");
            return _leaderboardList;
        }
    
        public static async UniTask UpdateLeaderboard(int score,string leaderboardSlugName)
        {
            UpdateScoreResponse updatedScore = await PassportFeatureSDK.Leaderboard.UpdateScore(leaderboardSlugName, score); 
            YLogger.LogInfo("UpdateLeaderboard  Finish ");
        }
        
        public static async UniTask UpdateLeaderboard(int score,string memberId,string leaderboardSlugName)
        {
            UpdateScoreResponse updatedScore = await PassportFeatureSDK.Leaderboard.UpdateEntityScore(leaderboardSlugName, memberId,score); 
            YLogger.LogInfo("UpdateLeaderboard  Finish ");
        }

    }
}