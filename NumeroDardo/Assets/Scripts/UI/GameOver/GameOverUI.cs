using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button playAgainButton;
    [SerializeField] Button exitnButton;

    public void ShowCanvas() {
        gameObject.SetActive(true);
    }

    public void HideCanvas() {
        gameObject.SetActive(false);
    }
}
