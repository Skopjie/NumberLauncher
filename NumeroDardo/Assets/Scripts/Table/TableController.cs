using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TableController : MonoBehaviour
{
    //es probable que no neceite tantas listas ya que las casillas guardan info
    List<SquareController> squareAllList = new List<SquareController>();
    List<SquareController> squareEnableList = new List<SquareController>();
    List<SquareController> squareDisableList = new List<SquareController>();
    List<SquareController> squareRangeList = new List<SquareController>();

    List<int> numbersAvaliableList = new List<int>();

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


    float actualNumberSelected = 0;
    Vector2Int numbersBtwRandom = new Vector2Int(0,100);
    bool isIncreasing = true;
    bool isGameOver = false;
    int randomNumber = 0;

    private void Awake() {
        getSquareButton.onClick.AddListener(() => {
            DeselectAllRandomSquare();
            SelectSquare((int)actualNumberSelected);
            ShowAreaEnableCartel(GetRandomNumber());
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
            squareEnableList.Add(newSquareController);
            numbersAvaliableList.Add(i);
        }
    }

    void ActiveSquareRange() {
        ReadjustCellSize(numberSquareTable);
        for (int i = 0; i < numberSquareTable; i++) {
            squareEnableList[i].SetActiveSquare(true);
        }
        for (int j = numberSquareTable; j < MAX_NUM_SQUARES; j++) {
            squareEnableList[j].SetActiveSquare(false);
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
            selectorTransform.localPosition = new Vector2(newPos, 85);
        }
    }

    void SelectSquare(int newSquare) {
        if (!isGameOver) {
            Debug.Log(newSquare);
            if (CheckIsSelectedCorrect(newSquare)) {
                SquareController squareController = squareAllList[newSquare];
                squareController.SelectSquare();
                squareDisableList.Add(squareController);
                squareEnableList.Remove(squareController);
            }
        }

    }

    bool CheckIsSelectedCorrect(int newSquare) {
        if (squareAllList[newSquare].GetIsSquareSelected()) {
            print("GameOver");
            isGameOver = true;
            return false;
        }
        if(newSquare < numbersBtwRandom.x || newSquare > numbersBtwRandom.y) {
            print("GameOver");
            isGameOver = true;
            return false;
        }
        return true;
    }

    void ReturnGameNormal() {
        squareEnableList.Clear();

        for(int i= 0; i < squareDisableList.Count; i++) {
            squareDisableList[i].DeselectSquare();
        }
        squareDisableList.Clear();

        squareEnableList = squareAllList;
        isIncreasing = true;
        actualNumberSelected = 0;
    }

    void ShowAreaEnableCartel(int newRandomNumber) {
        if (!isGameOver) {
            numbersBtwRandom = new Vector2Int(0, 100);

            for (int i = newRandomNumber - 1; i > 0; i--) {
                if (squareAllList[i].GetIsSquareSelected()) {
                    print("Numero izquierda encontrado en posicion " + squareAllList[i].GetSquareId());
                    numbersBtwRandom.x = i;
                    break;
                }
            }

            for (int i = newRandomNumber + 1; i < squareAllList.Count; i++) {
                if (squareAllList[i].GetIsSquareSelected()) {
                    print("Numero derecha encontrado en posicion " + squareAllList[i].GetSquareId());
                    numbersBtwRandom.y = i;
                    break;
                }
            }

            for (int i = numbersBtwRandom.x; i < numbersBtwRandom.y; i++) {
                if (!squareAllList[i].GetIsSquareSelected()) {
                    squareAllList[i].SetStatePossibleAnswer();
                    squareRangeList.Add(squareAllList[i]);
                }
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
