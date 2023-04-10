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

    public void Play() {
        gameObject.SetActive(false);
        tableController.StartGame();
    } 

    public void Exit() {
        gameObject.SetActive(false);
        tableController.ReturnGameNormal();
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.gameToMain);
    }
}
