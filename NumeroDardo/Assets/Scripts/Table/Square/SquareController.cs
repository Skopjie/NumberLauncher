using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image squareImage;

    [Header("Variables")]
    [SerializeField] bool squareIsSelected = false;


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
