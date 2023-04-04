using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TableController : MonoBehaviour
{
    //es probable que no neceite tantas listas ya que las casillas guardan info
    List<SquareController> squareAllList = new List<SquareController>();
    List<SquareController> squareRangeList = new List<SquareController>();
    List<SquareController> squareDisable = new List<SquareController>();

    List<int> numbersAvaliableList = new List<int>();
    List<int> allNumbersAvaliableList = new List<int>();

    [SerializeField] const int WIDTH_TABLE = 1300;
    [SerializeField] const int MAX_NUM_SQUARES = 100;

    [Header("Variables")]
    [SerializeField] float selectorSpeed;
    [SerializeField] int numberSquareTable = 100;
    [SerializeField] GameObject squarePrefab;

    [Header("Componentes")]
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] RectTransform rectTransform;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI cronometroTexto;
    [SerializeField] TextMeshProUGUI randomNumberText;
    [SerializeField] RectTransform selectorTransform;
    [SerializeField] Button getSquareButton;
    [SerializeField] GameObject canvasGameOver;


    float actualNumberSelected = 0;
    Vector2Int numbersBtwRandom = new Vector2Int(0,100);
    bool isIncreasing = true;
    bool isGameOver = false;
    int randomNumber = 0;

    private void Awake() {
        getSquareButton.onClick.AddListener(() => {
            DeselectAllRandomSquare();
            SelectSquare((int)actualNumberSelected);
        });
    }

    void Start()
    {
        InitComponentes();

        InitTable();
        GetRandomNumber();
        ActiveSquareRange();

    }

    void Update()
    {
        //Input
        MoveSelector();
    }

    void InitComponentes() {
        if (gridLayout == null)
            gridLayout = GetComponent<GridLayoutGroup>();

        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    void InitTable() {
        ReadjustCellSize(100);
        for (int i= 0; i< MAX_NUM_SQUARES; i++) {
            GameObject newSquare = Instantiate(squarePrefab, gameObject.transform);
            SquareController newSquareController = newSquare.GetComponent<SquareController>();
            newSquareController.InitSquareData(i);
            squareAllList.Add(newSquareController);

            numbersAvaliableList.Add(i);
            allNumbersAvaliableList.Add(i);
        }
    }

    void ActiveSquareRange() {
        ReadjustCellSize(numberSquareTable);
        foreach(SquareController sqControl in squareAllList) {
            sqControl.SetActiveSquare(true);
        }
    }

    void ReadjustCellSize(int newNumberSquare) {
        gridLayout.cellSize = new Vector2(WIDTH_TABLE / newNumberSquare, 100);
    }

    void MoveSelector() {
        if (!isGameOver) {
            if (isIncreasing) {
                actualNumberSelected += Time.deltaTime * selectorSpeed;
                if (actualNumberSelected >= MAX_NUM_SQUARES) {
                    isIncreasing = false;
                }
            }
            else {
                actualNumberSelected -= Time.deltaTime * selectorSpeed;
                if (actualNumberSelected <= 0) {
                    isIncreasing = true;
                }
            }
            cronometroTexto.text = "" + (int)actualNumberSelected;
            float newPos = Mathf.Lerp(-650, 650, actualNumberSelected / MAX_NUM_SQUARES);
            selectorTransform.localPosition = new Vector2(newPos, 90);
        }
    }

    void SelectSquare(int newSquare) {
        if (!isGameOver) {
            Debug.Log(newSquare);
            if (CheckIsSelectedCorrect(newSquare)) {
                SquareController squareController = squareAllList[newSquare];
                squareDisable.Add(squareController);
                squareController.SelectSquare(randomNumber);

                GetRange(GetRandomNumber());
            }
        }

    }

    bool CheckIsSelectedCorrect(int newSquare) {
        if (squareAllList[newSquare].GetIsSquareSelected()) {
            print("GameOver");
            isGameOver = true;
            ReturnGameNormal();
            return false;
        }
        if(newSquare < numbersBtwRandom.x || newSquare > numbersBtwRandom.y) {
            print("GameOver");
            isGameOver = true;
            ReturnGameNormal();
            return false;
        }
        return true;
    }


    void ReturnGameNormal() {
        squareDisable.Clear();
        foreach(SquareController sqControl in squareAllList) {
            sqControl.DeselectSquare();
        }
        numbersAvaliableList.Clear();
        foreach (int sqControl in allNumbersAvaliableList) {
            numbersAvaliableList.Add(sqControl);
        }

        isIncreasing = true;
        actualNumberSelected = 0;
        GetRandomNumber();
        isGameOver = false;
    }

    void GetRange(int newRandomNumber) {
        numbersBtwRandom = new Vector2Int(0, 100);
        print(":( "+newRandomNumber);
        for (int i = 0; i < squareDisable.Count; i++) {
            if(squareDisable[i].numberSquare > newRandomNumber) {
                if(i > 1) {
                    numbersBtwRandom.x = squareDisable[i-1].GetSquareId();
                }
                if(i == squareDisable.Count - 1) {
                    numbersBtwRandom.y = squareDisable[i].GetSquareId();
                }
            }
        }

        for (int i = numbersBtwRandom.x; i < numbersBtwRandom.y; i++) {
            if (!squareAllList[i].GetIsSquareSelected()) {
                squareAllList[i].SetStatePossibleAnswer();
                squareRangeList.Add(squareAllList[i]);
            }
        }
    }

    void DeselectAllRandomSquare() {
        for (int i = 0; i < squareRangeList.Count; i++) {
            if (!squareRangeList[i].GetIsSquareSelected())
                squareRangeList[i].DeselectSquare();
        }
        squareRangeList.Clear();
    }

    int GetRandomNumber() {
        if (!isGameOver) {
            randomNumber = Random.Range(0, numbersAvaliableList.Count);
            randomNumberText.text = "" + numbersAvaliableList[randomNumber];
            numbersAvaliableList.RemoveAt(randomNumber);

            return randomNumber;
        }
        return 0;
    }
}
