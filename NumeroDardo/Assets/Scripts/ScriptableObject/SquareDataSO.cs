using UnityEngine;

[CreateAssetMenu(fileName = "Square", menuName = "ScriptableObjects/SquareData")]
public class SquareDataSO : ScriptableObject
{
    public float timeChangeColor;

    public Vector2 textPosition;
    public Vector2 textPositionDown;

    public Color seleccionadaColor;
    public Color disponibleColor;
    public Color noDisponibleColor;
    public Color selectorColor;
}
