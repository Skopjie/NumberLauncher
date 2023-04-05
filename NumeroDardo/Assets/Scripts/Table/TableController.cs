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

    [SerializeField] const int WIDTH_TABLE = 775;
    [SerializeField] const int MAX_NUM_SQUARES = 100;

    [Header("Variables")]
    [SerializeField] float selectorSpeed;
    [SerializeField] int numberSquareTable = 100;
    [SerializeField] GameObject squarePrefab;

    [Header("Componentes")]
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] GameOverUI gameOverUI;

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
        gridLayout.cellSize = new Vector2(7.45f, 60);
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
            if (cronometroTexto.text != "" + (int)actualNumberSelected) {
                cronometroTexto.text = "" + (int)actualNumberSelected;
                //squareAllList[(int)actualNumberSelected].AnimSquare();
            }
            float newPos = Mathf.Lerp(-360, 360, actualNumberSelected / MAX_NUM_SQUARES);
            selectorTransform.localPosition = new Vector2(newPos, 150);
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
            gameOverUI.ShowCanvas();
            return false;
        }
        if(newSquare < numbersBtwRandom.x || newSquare > numbersBtwRandom.y) {
            print("GameOver");
            isGameOver = true;
            gameOverUI.ShowCanvas();
            return false;
        }
        return true;
    }


    public void ReturnGameNormal() {
        squareDisable.Clear();
        foreach(SquareController sqControl in squareAllList) {
            sqControl.DeselectSquare();
        }
        numbersAvaliableList.Clear();
        foreach (int sqControl in allNumbersAvaliableList) {
            numbersAvaliableList.Add(sqControl);
        }

        isGameOver = false;
        isIncreasing = true;
        actualNumberSelected = 0;
        numbersBtwRandom = new Vector2Int(0, 100);
        GetRandomNumber();
    }

    void GetRange(int newRandomNumber) {
        numbersBtwRandom = new Vector2Int(0, 100);
        int higherNumber = 100;
        int lowerNumber = 0;
        SquareController higher = new SquareController(100);
        SquareController lower= new SquareController(0);

        print(":( "+newRandomNumber);

        for (int i = 0; i < squareDisable.Count; i++) {
            if (squareDisable[i].numberSquare > newRandomNumber) {
                if(higherNumber > squareDisable[i].numberSquare) {
                    higherNumber = squareDisable[i].numberSquare;
                    higher = squareDisable[i];
                    numbersBtwRandom.y = higherNumber;
                }
            }
            else if (squareDisable[i].numberSquare < newRandomNumber) {
                if (lowerNumber < squareDisable[i].numberSquare) {
                    lowerNumber = squareDisable[i].numberSquare;
                    lower = squareDisable[i];
                    numbersBtwRandom.x = lowerNumber;
                }
            }
        }

        Debug.Log(lower.numberSquare + " / " + higher.numberSquare);
        numbersBtwRandom = new Vector2Int(lower.GetSquareId(), higher.GetSquareId());

        for (int i = numbersBtwRandom.x; i < numbersBtwRandom.y; i++) {
            if (!squareAllList[i].GetIsSquareSelected()) {
                squareAllList[i].SetStatePossibleAnswer();
                squareRangeList.Add(squareAllList[i]);
            }
        }
        //Poner que lo gris sea donde no se puede

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

            int newRandomNumber = numbersAvaliableList[randomNumber];
            randomNumberText.text = "" + newRandomNumber;
            numbersAvaliableList.RemoveAt(randomNumber);

            randomNumber = newRandomNumber;
            return newRandomNumber;
        }
        return 0;
    }
}
