using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

[System.Serializable]
struct AchievementPoint {
    public string idAchievement;
    public int points;
}

public class AchievementsController : MonoBehaviour
{
    public static AchievementsController Instance { get { return instace; } }
    private static AchievementsController instace;

    [SerializeField] AchievementPoint [] achievementList;
    [SerializeField] GameObject particleWin;

    private void Start() {
        instace = this;
    }

    public void ShowAchievementsUI() {
        Social.ShowAchievementsUI();
    }

    public void CheckAchievement(int newPoints) {
        foreach(AchievementPoint achievement in achievementList) {
            if(achievement.points == newPoints) {
                CompleteAchievment(achievement.idAchievement);
                particleWin.SetActive(true);
                AudioManager.Instance.PlaySFX(Sound.victory);
                return;
            }
        }
    }

    void CompleteAchievment(string newArchievement) {
        Social.ReportProgress(newArchievement,
            100,
            (bool success) => {
                if (success) { }
            });
    }


    public void ShowLeaderBordUI() {
        Social.ShowLeaderboardUI();
    }

    public void DoLeadBoardPost(int newScore) {
        Social.ReportScore(newScore, GPGSIds.leaderboard_points, (bool success) => { });
    }
}
