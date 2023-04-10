using System.Collections;
using System.Collections.Generic;
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

    [Header("Componentes")]
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject howCanvas;
    [SerializeField] GameObject[] capturasImage;


    int pageHow=0;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            PlayGame();
        });

        howButton.onClick.AddListener(() => {
            ShowHowToPlay();
        });

        archivesButton.onClick.AddListener(() => {
            ShowAchievementsUI();
        });

        moreButton.onClick.AddListener(() => {
            ShowLeaderBordUI();
        });



        backHowButton.onClick.AddListener(() => {
            ShowMainMenu();
        });

        rightHowButton.onClick.AddListener(() => {
            GetNextCaptura(true);
        });

        leftHowButton.onClick.AddListener(() => {
            GetNextCaptura(false);
        });
    }

    private void Start() {
    }

    void PlayGame() {
        gameObject.SetActive(false);
        gameCanvas.SetActive(true);
    }

    void ShowHowToPlay() {
        gameObject.SetActive(false);
        howCanvas.SetActive(true);
    }
    void ShowMainMenu() {
        gameObject.SetActive(true);
        howCanvas.SetActive(false);
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
