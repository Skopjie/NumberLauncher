using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSelectorRandom : MonoBehaviour
{
    int GetRandomNumber() {
        return Random.Range(0, 1000);
    }
}
