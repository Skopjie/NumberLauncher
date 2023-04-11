using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button playAgainButton;
    [SerializeField] Button exitButton;

    [Header("Componentes")]
    [SerializeField] MainMenuUI mainMenu;
    [SerializeField] TableController tableController;


    [SerializeField] RectTransform rectTransformGameOver;
    [SerializeField] CanvasGroup canvasGroupGameOver;

    [Header("Variables")]
    [SerializeField] float speedAnimation = 1;
    [SerializeField] float addPerDead = 4;

    int numberOfDeaths = 0;

    private void Awake() {
        playAgainButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            Play();
        });

        exitButton.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(Sound.pressButton);
            Exit();
        });
    }

    public void ShowCanvas() {
        FadeInGameOver();

        numberOfDeaths++;
    }

    public void Play() {
        if(numberOfDeaths >= addPerDead) {
            InterstitialAdExample.Instance.ShowAd();
            numberOfDeaths = 0;
        }

        FadeOutGameOver();
        tableController.StartGame();
    } 

    public void Exit() {
        FadeOutGameOver();
        tableController.ReturnGameNormal();
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.gameToMain);
    }

    void FadeOutGameOver() {
        canvasGroupGameOver.blocksRaycasts = false;
        rectTransformGameOver.DOAnchorPos(new Vector2(0, 150), speedAnimation, false).SetEase(Ease.InOutQuint);
        canvasGroupGameOver.DOFade(0, speedAnimation);
    }
    void FadeInGameOver() {
        rectTransformGameOver.transform.localPosition = new Vector3(0, 1500, 0);
        rectTransformGameOver.DOAnchorPos(new Vector2(0, -150), speedAnimation, false).SetEase(Ease.InOutQuint);
        canvasGroupGameOver.DOFade(1, speedAnimation).OnComplete(() => {
            canvasGroupGameOver.blocksRaycasts = true;
        });
    }
}
