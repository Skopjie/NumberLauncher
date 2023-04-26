using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button playButton;
    [SerializeField] Button howButton;
    [SerializeField] Button archivesButton;
    [SerializeField] Button moreButton;



    private void Start() {
        playButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            BannerAdExample.Instance.ShowBannerAd();
            PlayGame();
        });

        howButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            ShowHowToPlay();
        });

        archivesButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            ShowAchievementsUI();
        });

        moreButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            ShowLeaderBordUI();
        });
    }

    void PlayGame() {
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.mainToGame);
    }

    void ShowHowToPlay() {
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.mainToHow);
    }


    void ShowAchievementsUI() {
        AchievementsController.Instance.ShowAchievementsUI();
    }

    void ShowLeaderBordUI() {
        AchievementsController.Instance.ShowLeaderBordUI();
    }
}
