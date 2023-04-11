using UnityEngine;
using DG.Tweening;

public enum MenusInteraction {
    mainToGame,
    gameToMain,
    howToMain,
    mainToHow
}
public class ChangeMenuController : MonoBehaviour
{
    public static ChangeMenuController Instance { get { return instace; } }
    private static ChangeMenuController instace;

    [Header("Variables")]
    [SerializeField] float speedChange = 0;
    [SerializeField] Vector2 newPositions = Vector2.zero;

    [Header("Componentes")]
    [SerializeField] RectTransform rectTransformGame;
    [SerializeField] CanvasGroup canvasGroupGame;

    [SerializeField] RectTransform rectTransformMain;
    [SerializeField] CanvasGroup canvasGroupMain;

    [SerializeField] RectTransform rectTransformHow;
    [SerializeField] CanvasGroup canvasGroupHow;


    private void Awake() {
        instace = this;
    }

    public void ShowMenuFade(MenusInteraction newMenu) {
        switch (newMenu) {
            case MenusInteraction.mainToGame:
                FadeOut(rectTransformMain, canvasGroupMain);
                FadeIn(rectTransformGame, canvasGroupGame);
                TableController.Instance.StartGame();
                break;

            case MenusInteraction.gameToMain:
                FadeOut(rectTransformGame, canvasGroupGame);
                FadeIn(rectTransformMain, canvasGroupMain);
                break;

            case MenusInteraction.howToMain:
                FadeOut(rectTransformHow, canvasGroupHow);
                FadeIn(rectTransformMain, canvasGroupMain);
                break;

            case MenusInteraction.mainToHow:
                FadeOut(rectTransformMain, canvasGroupMain);
                FadeIn(rectTransformHow, canvasGroupHow);
                break;
        }
    }

    void FadeIn(RectTransform rectTranformMenu, CanvasGroup canvasGroupMenu) {
        rectTranformMenu.transform.localPosition = new Vector3(0, newPositions.x, 0);
        rectTranformMenu.DOAnchorPos(new Vector2(0, 0), speedChange, false).SetEase(Ease.InOutQuint);
        canvasGroupMenu.DOFade(1, speedChange).OnComplete(() => {
            canvasGroupMenu.blocksRaycasts = true;
        });
    }


    void FadeOut(RectTransform rectTranformMenu, CanvasGroup canvasGroupMenu) {
        canvasGroupMenu.blocksRaycasts = false;
        rectTranformMenu.transform.localPosition = new Vector3(0, 0, 0);
        rectTranformMenu.DOAnchorPos(new Vector2(0, newPositions.y), speedChange, false).SetEase(Ease.InOutQuint);
        canvasGroupMenu.DOFade(0, speedChange);
    }

}
