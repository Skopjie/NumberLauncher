using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClips {
    pressButton,
    addNumber,
    gameOver
}

public class SoundController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] AudioSource audioSource;

    public void PlaySound(AudioClips audio) {

    }
}
