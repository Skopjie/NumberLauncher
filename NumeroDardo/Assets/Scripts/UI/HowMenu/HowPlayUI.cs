using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class HowPlayUI : MonoBehaviour
{
    [Header("UIHow")]
    [SerializeField] Button backHowButton;
    [SerializeField] Button rightHowButton;
    [SerializeField] Button leftHowButton;
    [SerializeField] TextMeshProUGUI pageNumberText;


    [SerializeField] GameObject[] capturasImage;


    int pageHow = 0;

    private void Awake() {

    }

    private void Start() {
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

    void ShowMainMenu() {
        ChangeMenuController.Instance.ShowMenuFade(MenusInteraction.howToMain);
    }

    public void GetNextCaptura(bool direction) {
        if (direction) pageHow++;
        else pageHow--;

        if (pageHow == 3) pageHow = 0;
        if (pageHow == -1) pageHow = 2;

        foreach (GameObject captura in capturasImage) {
            captura.SetActive(false);
        }
        capturasImage[pageHow].SetActive(true);
        pageNumberText.text = pageHow + 1 + "/3";
    }
}
