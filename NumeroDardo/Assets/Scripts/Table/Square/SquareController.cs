using System.Collections;
using System.Collections.Generic;
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
    //squareTransform.DOLocalMove(new Vector3(0, 5, 0), 2).SetEase(Ease.InOutSine).SetLoops(1, LoopType.Yoyo);

    [Header("Componentes")]
    [SerializeField] Image squareImage;
    [SerializeField] RectTransform squareTransform;

    [Header("Variables")]
    [SerializeField] SquareState squareState;
    [SerializeField] int idSquare = 0;
    public int numberSquare = 0;
    [SerializeField] float timeChangeColor;

    [SerializeField] Vector2 textPosition = new Vector2(0,80);
    [SerializeField] Vector2 textPositionDown = new Vector2(0,-80);

    [SerializeField] Color seleccionadaColor;
    [SerializeField] Color DisponibleColor;
    [SerializeField] Color NoDisponibleColor;

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
                squareImage.DOColor(seleccionadaColor, timeChangeColor);
                break;

            case SquareState.Disponible:
                squareImage.DOColor(DisponibleColor, timeChangeColor);
                break;

            case SquareState.NoDisponible:
                squareImage.DOColor(NoDisponibleColor, timeChangeColor);
                break;
        }
    }

    public void AnimSquare() {
        squareImage.color = Color.green;
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
            rectTransformText.localPosition = textPosition;
        else
            rectTransformText.localPosition = textPositionDown;
    }

    public void DeselectSquare() {
        numberSquare = 0;
        numberIdText.gameObject.SetActive(false);
        SetSquareState(SquareState.Disponible);
    }
}
