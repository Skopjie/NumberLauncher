using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button playAgainButton;
    [SerializeField] Button exitButton;

    [Header("Componentes")]
    [SerializeField] MainMenuUI mainMenu;
    [SerializeField] TableController tableController;
    [SerializeField] GameObject gameCanvas;

    private void Awake() {
        playAgainButton.onClick.AddListener(() => {
            Play();
        });

        exitButton.onClick.AddListener(() => {
            Exit();
        });
    }

    public void ShowCanvas() {
        gameObject.SetActive(true);
    }

    public void HideCanvas() {
        gameObject.SetActive(false);
        gameCanvas.SetActive(false);
    }

    public void Play() {
        gameObject.SetActive(false);
        tableController.ReturnGameNormal();
    } 

    public void Exit() {
        tableController.ReturnGameNormal();
        HideCanvas();
        mainMenu.ShowCanvas();
    }
}
