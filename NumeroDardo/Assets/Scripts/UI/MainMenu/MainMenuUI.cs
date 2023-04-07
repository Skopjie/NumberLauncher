using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button playButton;
    [SerializeField] Button howButton;
    [SerializeField] Button archivesButton;
    [SerializeField] Button moreButton;

    [SerializeField] GameObject gameCanvas;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            PlayGame();
        });

        howButton.onClick.AddListener(() => {
        });

        archivesButton.onClick.AddListener(() => {
            ShowAchievementsUI();
        });

        moreButton.onClick.AddListener(() => {
            ShowMoreGames();
        });
    }

    void PlayGame() {
        gameObject.SetActive(false);
        gameCanvas.SetActive(true);
        CompleteAchievment(GPGSIds.achievement_10_points);
    }

    void ShowAchievementsUI() {
        Social.ShowAchievementsUI();
    }

    void CompleteAchievment(string newArchievement) {
        Social.ReportProgress(newArchievement,
            100,
            (bool success) => {
                if (success) { }
            });
    }

    void ShowMoreGames() {
        Application.OpenURL ("market://developer?id=Skopjie");
    }

    public void ShowCanvas() {
        gameObject.SetActive(true);
    }
}
