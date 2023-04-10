using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using GooglePlayGames.BasicApi;

public class GPGManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statusText;

    public Transform square;


    private void Start() {
        //square.DOMove(new Vector3(2, 0, 0), 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        SignIntoGPS(SignInInteractivity.CanPromptOnce);

    }
    internal void SignIntoGPS(SignInInteractivity interactivity) {
        PlayGamesPlatform.Activate();


        PlayGamesPlatform.Instance.Authenticate((code) => {
            //statusText.text = "Wait...";

            if (code == SignInStatus.Success) {
                //statusText.text = "Good";
                //nameText.text = Social.localUser.userName + " / " + Social.localUser.id;
                // Continue with Play Games Services
            }
            else {
                //statusText.text = "Fail: " + code;
                // Disable your integration with Play Games Services or show a login button
                // to ask users to sign-in. Clicking it should call
                // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            }
        });
    }
}
