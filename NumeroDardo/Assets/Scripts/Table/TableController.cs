using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TableController : MonoBehaviour
{
    //Lista de todas las casillas que forman el tablero
    //lista de casillas utilizadas

    [Header("Variables")]
    [SerializeField] float selectorSpeed;
    [SerializeField] int numberSquareTable = 100;
    [SerializeField] GameObject squarePrefab;

    [Header("Componentes")]
    [SerializeField] GridLayoutGroup gridLayout;


    void Start()
    {
        InitComponentes();
        InitTable();
    }

    void Update()
    {
        //Input
    }

    void InitComponentes() {
        if (gridLayout == null)
            gridLayout = GetComponent<GridLayoutGroup>();
    }

    void InitTable() {
        for(int i= 0; i< numberSquareTable; i++) {
            Instantiate(squarePrefab, this.gameObject.transform);
        }
    }

    void MoveSelector() {

    }

    void GetCasilla() {

    }

    void ShowAreaEnableCartel() {
        //Mask en la zona libre
    }

    void AddCartel(int newNumberArea) {

    }

    void CheeckCartelArea() {

    }
}
