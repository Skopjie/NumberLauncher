using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image squareImage;
    [SerializeField] RectTransform squareTransform;

    [Header("Variables")]
    [SerializeField] bool squareIsSelected = false;
    [SerializeField] int idSquare = 0;
    public int numberSquare = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI numberIdText;


    public SquareController(int newIdSquare) {
        InitSquareData(newIdSquare);
    }

    public void InitSquareData(int newIdSquare) {
        idSquare = newIdSquare;
    }

    public int GetSquareId() {
        return idSquare;
    }

    public void SelectSquare(int newNumber) {
        squareIsSelected = true;
        numberSquare = newNumber;
        numberIdText.text = "" + newNumber;
        numberIdText.gameObject.SetActive(true);
        squareImage.color = Color.red;
    }

    public void DeselectSquare() {
        squareIsSelected = false;
        numberIdText.gameObject.SetActive(false);
        numberSquare = 0;
        squareImage.color = Color.white;
    }

    public bool GetIsSquareSelected() {
        return squareIsSelected;
    }

    public void SetStatePossibleAnswer() {
        squareImage.color = Color.grey;
    }

    public void SetActiveSquare(bool newActive) {
        if (newActive) {
            squareImage.color = Color.white;
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
