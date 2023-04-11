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


    [Header("UIHow")]
    [SerializeField] Button backHowButton;
    [SerializeField] Button rightHowButton;
    [SerializeField] Button leftHowButton;
    [SerializeField] TextMeshProUGUI pageNumberText;


    [SerializeField] GameObject[] capturasImage;


    int pageHow=0;

    private void Awake() {

    }

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



        backHowButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            ShowMainMenu();
        });

        rightHowButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            GetNextCaptura(true);
        });

        leftHowButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            GetNextCaptura(false);
        });
    }

    void PlayGame() {
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.mainToGame);
    }

    void ShowHowToPlay() {
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.mainToHow);
    }

    void ShowMainMenu() {
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.howToMain);
    }


    void ShowAchievementsUI() {
        AchievementsController.Instance.ShowAchievementsUI();
    }

    void ShowLeaderBordUI() {
        AchievementsController.Instance.ShowLeaderBordUI();
    }

    public void ShowCanvas() {
        gameObject.SetActive(true);
    }

    public void GetNextCaptura(bool direction) {
        if (direction) pageHow++;
        else pageHow--;

        if (pageHow == 3) pageHow = 0;
        if (pageHow == -1) pageHow = 2;

        foreach(GameObject captura in capturasImage) {
            captura.SetActive(false);
        }
        capturasImage[pageHow].SetActive(true);
        pageNumberText.text = pageHow +1 + "/3";
    }
}
