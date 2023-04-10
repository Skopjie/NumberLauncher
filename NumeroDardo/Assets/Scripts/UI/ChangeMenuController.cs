using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeMenuController : MonoBehaviour
{
    [SerializeField] RectTransform mainMenuPanel;
    [SerializeField] RectTransform gamePanel;


    private void Start() {
        ChangeToGamePanel();
    }

    public void ChangeToGamePanel() {
        mainMenuPanel.DOMoveX(1500, 2);
        gamePanel.DOMoveX(500, 2);
    }

    public void ChangeToMainMenu() {

    }
}
