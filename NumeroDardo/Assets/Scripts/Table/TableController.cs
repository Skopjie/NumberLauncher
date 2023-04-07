using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public static TableController Instance { get { return instace; } }
    private static TableController instace;

    List<SquareController> squareAllList = new List<SquareController>();
    List<SquareController> squareRangeList = new List<SquareController>();
    List<SquareController> squareDisable = new List<SquareController>();

    List<int> numbersAvaliableList = new List<int>();
    List<int> allNumbersAvaliableList = new List<int>();

    [Header("Variables")]
    public int numberSquareTable = 100;
    public float widthTable = 100;
    [SerializeField] GameObject squarePrefab;

    [Header("Componentes")]
    [SerializeField] SelectorController selectorController;
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] GameOverUI gameOverUI;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI randomNumberText;
    [SerializeField] Button getSquareButton;
    [SerializeField] GameObject canvasGameOver;


    Vector2Int numbersBtwRandom = new Vector2Int(0,100);
    public bool isGameOver = false;
    int randomNumber = 0;

    private void Awake() {
        instace = this;

        getSquareButton.onClick.AddListener(() => {
            DeselectAllRandomSquare();
            SelectSquare(selectorController.GetActualNumber());
        });
    }

    void Start() {
        widthTable = transform.GetComponent<RectTransform>().sizeDelta.x;
        InitComponentes();

        InitTable();
        GetRandomNumber();
    }

    void InitComponentes() {
        if (gridLayout == null)
            gridLayout = GetComponent<GridLayoutGroup>();

        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    void InitTable() {
        ReadjustCellSize(100);
        for (int i= 0; i< numberSquareTable; i++) {
            GameObject newSquare = Instantiate(squarePrefab, gameObject.transform);
            SquareController newSquareController = newSquare.GetComponent<SquareController>();
            newSquareController.InitSquareData(i);
            squareAllList.Add(newSquareController);

            numbersAvaliableList.Add(i);
            allNumbersAvaliableList.Add(i);
        }
    }

    void ReadjustCellSize(int newNumberSquare) {
        gridLayout.cellSize = new Vector2(widthTable/numberSquareTable, 60);
    }

    void SelectSquare(int newSquare) {
        if (!isGameOver) {
            Debug.Log(newSquare);
            if (CheckSquareIsAvailable(newSquare)) {
                SquareController squareController = squareAllList[newSquare];
                squareDisable.Add(squareController);
                squareController.SelectSquare(randomNumber);
                GetSquareRangeAvailable();
            }
        }
    }

    bool CheckSquareIsAvailable(int newSquare) {
        if (squareAllList[newSquare].GetSquareState() == SquareState.Seleccionada) {
            GameOver();
            return false;
        }
        if(newSquare < numbersBtwRandom.x || newSquare > numbersBtwRandom.y) {
            GameOver();
            return false;
        }
        return true;
    }

    void GameOver() {
        isGameOver = true;
        gameOverUI.ShowCanvas();
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
        numbersBtwRandom = new Vector2Int(0, 100);
        GetRandomNumber();
        selectorController.ReturnGameNormal();
    }

    void GetSquareRangeAvailable() {
        numbersBtwRandom = new Vector2Int(0, 100);
        int higherNumber = 100;
        int lowerNumber = 0;
        SquareController higher = new SquareController(100);
        SquareController lower= new SquareController(0);

        GetRandomNumber();
        
        for (int i = 0; i < squareDisable.Count; i++) {
            if (squareDisable[i].numberSquare > randomNumber) {
                if(higherNumber > squareDisable[i].numberSquare) {
                    higherNumber = squareDisable[i].numberSquare;
                    higher = squareDisable[i];
                    numbersBtwRandom.y = higherNumber;
                }
            }
            else if (squareDisable[i].numberSquare < randomNumber) {
                if (lowerNumber < squareDisable[i].numberSquare) {
                    lowerNumber = squareDisable[i].numberSquare;
                    lower = squareDisable[i];
                    numbersBtwRandom.x = lowerNumber;
                }
            }
        }

        Debug.Log(lower.numberSquare + " / " + higher.numberSquare);
        numbersBtwRandom = new Vector2Int(lower.GetSquareId(), higher.GetSquareId());
        DrawSquareTable();
    }

    void DrawSquareTable() {
        for (int i = numbersBtwRandom.x; i < numbersBtwRandom.y; i++) {
            if (squareAllList[i].GetSquareState() != SquareState.Seleccionada) {
                squareAllList[i].SetSquareState(SquareState.Disponible);
                squareRangeList.Add(squareAllList[i]);
            }
        }
    }

    void DeselectAllRandomSquare() {
        foreach (SquareController squControl in squareAllList) {
            if (squControl.GetSquareState() != SquareState.Seleccionada)
                squControl.SetSquareState(SquareState.NoDisponible);
        }
        squareRangeList.Clear();
    }

    void GetRandomNumber() {
        if (!isGameOver) {
            int newRandomNumber = Random.Range(0, numbersAvaliableList.Count);

            randomNumber = numbersAvaliableList[newRandomNumber];
            randomNumberText.text = "" + randomNumber;
            numbersAvaliableList.RemoveAt(newRandomNumber);
        }
    }
}
