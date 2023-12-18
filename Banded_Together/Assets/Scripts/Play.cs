using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("TutorialLevel");
    }

    public void OpenCredits()
    {
        SceneManager.LoadSceneAsync("CreditsScene");
    }

    public void CloseCredits()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
