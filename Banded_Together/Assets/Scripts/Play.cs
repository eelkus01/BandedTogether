using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject creditsUI;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Eleanor_Workspace");

    }

    public void OpenCredits()
    {
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);

    }

}
