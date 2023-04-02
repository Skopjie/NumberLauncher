using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GPGManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statusText;


    private void Start() {
        
    }
    internal void ProcessAuthentication(SignInStatus status) {
        statusText.text = "Wait...";
        if (status == SignInStatus.Success) {
            statusText.text = "Good";
            nameText.text = Social.localUser.userName + " / " + Social.localUser.id;
            // Continue with Play Games Services
        }
        else {
            statusText.text = "Fail: " + status;
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
