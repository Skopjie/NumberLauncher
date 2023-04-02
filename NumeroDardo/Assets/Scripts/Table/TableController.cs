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
    [SerializeField] Button getSquareButton;


    float actualNumberSelected = 0;
    bool isIncreasing = true;

    private void Awake() {
        getSquareButton.onClick.AddListener(() => {
            SelectSquare((int)actualNumberSelected);
        });
    }

    void Start()
    {
        InitComponentes();

        InitTable();
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
            squareAllList.Add(newSquareController);
            squareEnableList.Add(newSquareController);
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
        if (isIncreasing) {
            actualNumberSelected += Time.deltaTime * selectorSpeed;
            if(actualNumberSelected >= MAX_NUM_SQUARES) {
                isIncreasing = false;
            }
        }
        else {
            actualNumberSelected -= Time.deltaTime * selectorSpeed;
            if (actualNumberSelected <= 0) {
                isIncreasing = true;
            }
        }
        cronometroTexto.text = ""+(int)actualNumberSelected;
    }

    void SelectSquare(int newSquare) {
        Debug.Log(newSquare);
        if (squareAllList[newSquare].GetIsSquareSelected()) {
            print("GameOver");
        }
        else {
            SquareController squareController = squareAllList[newSquare];
            squareController.SelectSquare();
            squareDisableList.Add(squareController);
            squareEnableList.Remove(squareController);
        }
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

    void ShowAreaEnableCartel() {
        //Mask en la zona libre
        //Ver area entre dos carteles mediante lista de desactivados 
        //las casillas deberian de tener un id para saber en donde colocar la mascara
    }

    void CheeckCartelArea() {

    }
}
