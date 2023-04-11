using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public static TableController Instance { get { return instace; } }
    private static TableController instace;

    public List<SquareController> squareAllList = new List<SquareController>();
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
    [SerializeField] TextMeshProUGUI numberClicksText;
    [SerializeField] Button getSquareButton;
    [SerializeField] GameObject canvasGameOver;



    Vector2Int numbersBtwRandom = new Vector2Int(0,100);
    public bool isGameOver = true;
    int randomNumber = 0;
    int numberClicks = 0;

    SquareController higher;
    SquareController lower;

    private void Awake() {
        instace = this;
        isGameOver = true;

        getSquareButton.onClick.AddListener(() => {
            DeselectAllRandomSquare();
            SelectSquare(selectorController.GetActualNumber());
            AddClick();
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
        ReadjustCellSize();
        for (int i= 0; i< numberSquareTable; i++) {
            GameObject newSquare = Instantiate(squarePrefab, gameObject.transform);
            SquareController newSquareController = newSquare.GetComponent<SquareController>();
            newSquareController.InitSquareData(i);
            squareAllList.Add(newSquareController);

            numbersAvaliableList.Add(i);
            allNumbersAvaliableList.Add(i);
        }
    }

    void ReadjustCellSize() {
        gridLayout.cellSize = new Vector2(widthTable/numberSquareTable, 60);
    }

    void SelectSquare(int newSquare) {
        if (!isGameOver) {
            if(newSquare == 50) {
                newSquare = 49;
            }
            if (CheckSquareIsAvailable(newSquare)) {
                AudioManager.Instance.PlaySFX(Sound.addNumber);
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

    void GetSquareRangeAvailable() {
        numbersBtwRandom = new Vector2Int(0, numberSquareTable);
        int higherNumber = 100;
        int lowerNumber = 0;
        higher = new SquareController(numberSquareTable);
        lower = new SquareController(0);

        GetRandomNumber();

        for (int i = 0; i < squareDisable.Count; i++) {
            if (squareDisable[i].numberSquare > randomNumber) {
                if (higherNumber >= squareDisable[i].numberSquare) {
                    higherNumber = squareDisable[i].numberSquare;
                    higher = squareDisable[i];
                    numbersBtwRandom.y = higherNumber;
                }
            }
            else if (squareDisable[i].numberSquare < randomNumber) {
                if (lowerNumber <= squareDisable[i].numberSquare) {
                    lowerNumber = squareDisable[i].numberSquare;
                    lower = squareDisable[i];
                    numbersBtwRandom.x = lowerNumber;
                }
            }
        }

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

    public void StartGame() {
        isGameOver = false;
        ReturnGameNormal();
    }

    void GameOver() {
        AudioManager.Instance.PlaySFX(Sound.gameOver);
        AchievementsController.Instance.DoLeadBoardPost(numberClicks);
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

        numberClicks = 0;
        numberClicksText.text = "" + numberClicks;
        numbersBtwRandom = new Vector2Int(0, 100);
        GetRandomNumber();
        selectorController.ReturnGameNormal();
    }

    void AddClick() {
        if (!isGameOver) {
            numberClicks++;
            numberClicksText.text = "" + numberClicks;
            AchievementsController.Instance.CheckAchievement(numberClicks);
        }
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
