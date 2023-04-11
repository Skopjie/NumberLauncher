using GooglePlayGames;
using UnityEngine;
using GooglePlayGames.BasicApi;

public class GPGManager : MonoBehaviour
{
    private void Start() {
        SignIntoGPS(SignInInteractivity.CanPromptOnce);

    }
    internal void SignIntoGPS(SignInInteractivity interactivity) {
        PlayGamesPlatform.Activate();


        PlayGamesPlatform.Instance.Authenticate((code) => {
        });
    }
}
