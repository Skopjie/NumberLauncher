using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//All audios
public enum Sound {
    pressButton,
    victory,
    gameOver,
    addNumber
}

[System.Serializable]
public class SoundAudioClip {
    public Sound sound;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get { return instace; } }
    private static AudioManager instace;

    [Header("UI")]
    [SerializeField] Button activeSoundButton;
    [SerializeField] GameObject crossVolumeImage;

    [Header("Componentes")]
    [SerializeField] AudioSource sfxSource;

    [Header("Variables Importantes")]
    [SerializeField] private List<SoundAudioClip> allAudios = new List<SoundAudioClip>();

    public bool soundIsActive = true;

    private void Awake() {
        instace = this;

        InitializeAudioSource();
    }

    private void Start() {
        activeSoundButton.onClick.AddListener(() => {
            ActiveSound();
            PlaySFX(Sound.pressButton);
        });
        int soundActiveInt = PlayerPrefs.GetInt("soundIsActive");
        soundIsActive = soundActiveInt == 0 ? true : false;
        ShowCrossVolume();
    }

    private void InitializeAudioSource() {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.5f;
    }

    private AudioClip GetAudioClip(Sound sound) {
        foreach (SoundAudioClip soundAudioClip in allAudios) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }

    void ActiveSound() {
        soundIsActive = !soundIsActive;
        int soundActiveInt = soundIsActive == true ? 0 : 1;
        PlayerPrefs.SetInt("soundIsActive", soundActiveInt);
        PlayerPrefs.Save();
        ShowCrossVolume();
    }

    void ShowCrossVolume() {
        if (soundIsActive)
            crossVolumeImage.SetActive(false);
        else
            crossVolumeImage.SetActive(true);
    }

    public void PlaySFX(Sound sound) {
        if (soundIsActive) {
            AudioClip SFXClip = GetAudioClip(sound);
            sfxSource.PlayOneShot(SFXClip);
        }
    }
}
