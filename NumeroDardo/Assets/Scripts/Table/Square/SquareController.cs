using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image squareImage;
    [SerializeField] Animator squareAnimator;

    [Header("Variables")]
    [SerializeField] bool squareIsSelected = false;
    [SerializeField] int idSquare = 0;

    public SquareController(int newIdSquare) {
        InitSquareData(newIdSquare);
    }

    public void InitSquareData(int newIdSquare) {
        idSquare = newIdSquare;
    }

    public int GetSquareId() {
        return idSquare;
    }

    public void SelectSquare() {
        squareIsSelected = true;
        squareImage.color = Color.red;
    }

    public void DeselectSquare() {
        squareIsSelected = false;
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
