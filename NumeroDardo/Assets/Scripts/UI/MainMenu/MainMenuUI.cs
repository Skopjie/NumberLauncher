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
            HideCanvas();
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
        Application.OpenURL("market://details?q=pname:com.Skopjie");
    }

    public void ShowCanvas() {
        gameObject.SetActive(true);
    }

    public void HideCanvas() {
        gameObject.SetActive(false);
        gameCanvas.SetActive(true);
    }
}
