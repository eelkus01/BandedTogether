using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject creditsUI;
    public GameObject rules;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("TutorialLevel");

    }

    public void OpenCredits()
    {
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);

    }

    public void ShowRules()
    {
        rules.SetActive(true);

    }

    public void CloseRules()
    {
        rules.SetActive(false);

    }

}
