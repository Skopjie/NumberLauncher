using UnityEngine.UI;
using TMPro;
using UnityEngine;
using DG.Tweening;

public enum SquareState { 
    Seleccionada,
    Disponible,
    NoDisponible
}

public class SquareController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image squareImage;
    [SerializeField] RectTransform squareTransform;

    [Header("Variables")]
    [SerializeField] SquareState squareState;
    [SerializeField] int idSquare = 0;
    public int numberSquare = 0;
    [SerializeField] SquareDataSO squareData;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI numberIdText;
    [SerializeField] RectTransform rectTransformText;

    public SquareController(int newIdSquare) {
        InitSquareData(newIdSquare);
    }

    public void InitSquareData(int newIdSquare) {
        if (squareImage != null)
            SetSquareState(SquareState.Disponible);

        idSquare = newIdSquare;
        numberSquare = newIdSquare;
    }

    public int GetSquareId() {
        return idSquare;
    }

    public SquareState GetSquareState() {
        return squareState;
    }

    public void SetSquareState(SquareState newSquareState) {
        squareState = newSquareState;
        ChangeColorSquare();
    }

    void ChangeColorSquare() {
        switch (squareState) {
            case SquareState.Seleccionada:
                squareImage.DOColor(squareData.seleccionadaColor, squareData.timeChangeColor);
                break;

            case SquareState.Disponible:
                squareImage.DOColor(squareData.disponibleColor, squareData.timeChangeColor);
                break;

            case SquareState.NoDisponible:
                squareImage.DOColor(squareData.noDisponibleColor, squareData.timeChangeColor);
                break;
        }
    }

    public void AnimSquare() {
        squareImage.color = squareData.selectorColor;
        ChangeColorSquare();
    }

    public void SelectSquare(int newNumber) {
        SetNumberTextPosition();
        numberSquare = newNumber;
        numberIdText.text = "" + newNumber;
        numberIdText.gameObject.SetActive(true);
        SetSquareState(SquareState.Seleccionada);
    }

    void SetNumberTextPosition() {
        if (idSquare % 2 == 0)
            rectTransformText.localPosition = squareData.textPosition;
        else
            rectTransformText.localPosition = squareData.textPositionDown;
    }

    public void DeselectSquare() {
        numberSquare = 0;
        numberIdText.gameObject.SetActive(false);
        SetSquareState(SquareState.Disponible);
    }
}
