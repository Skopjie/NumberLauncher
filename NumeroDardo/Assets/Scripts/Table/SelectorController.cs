using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public List<SquareController> squareAllList = new List<SquareController>();

    [Header("Variables")]
    [SerializeField] float selectorSpeed;

    [Header("UI")]
    [SerializeField] RectTransform selectorTransform;
    [SerializeField] TextMeshProUGUI cronometroTexto;


    float actualNumberSelected = 0;
    float numberSquare = 0;
    bool isIncreasing = true;

    private void Start() {
        numberSquare = TableController.Instance.numberSquareTable;

        squareAllList = TableController.Instance.squareAllList;
    }

    void FixedUpdate() {
        MoveSelector();
    }

    public int GetActualNumber() {
        return (int)actualNumberSelected;
    }

    void MoveSelector() {
        if (!TableController.Instance.isGameOver) {
            if (isIncreasing) {
                actualNumberSelected += Time.deltaTime * selectorSpeed;
                if (actualNumberSelected >= numberSquare) {
                    isIncreasing = false;
                }
            }
            else {
                actualNumberSelected -= Time.deltaTime * selectorSpeed;
                if (actualNumberSelected <= 0) {
                    isIncreasing = true;
                }
            }
            float newPosX = Mathf.Lerp(-370, 370, actualNumberSelected / numberSquare);
            selectorTransform.localPosition = new Vector2(newPosX, selectorTransform.localPosition.y);

            SetChronometerText();
        }
    }

    void SetChronometerText() {
        if (cronometroTexto.text != "" + (int)actualNumberSelected) {
            cronometroTexto.text = "" + (int)actualNumberSelected;
            if ((int)actualNumberSelected < 50)
                squareAllList[(int)actualNumberSelected].AnimSquare();
        }
    }

    public void ReturnGameNormal() {
        isIncreasing = true;
        actualNumberSelected = 0;
    }
}
