using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavHandler : MonoBehaviour {

    private GameObject player;
    private string sceneName;
    public GameObject partsText;
    public int partsGotten = 0;
    public int partsNeeded = 0;

    void Start(){
        UpdateParts();
        player = GameObject.FindWithTag("Player");
    }

    public void AddParts(){
        partsGotten += 1;
        UpdateParts();
    }

    void UpdateParts(){
        Text partsTextB = partsText.GetComponent<Text>();
        partsTextB.text = "Parts Obtained: " + partsGotten + "/" + partsNeeded;
    }

    public void StartGame(){
        // Time.timeScale = 1f;
        // GameHandler_PauseMenu.GameisPaused = false;
        // partsGotten = 0;
        // SceneManager.LoadScene("Mailroom");
    }

    public void ReplayLevel(){
        Time.timeScale = 1f;
        // GameHandler_PauseMenu.GameisPaused = false;
        // partsGotten = 0;
        // if (SceneManager.GetActiveScene().name != "LoseScene"){
        //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // } else {
        //         SceneManager.LoadScene("Mailroom");
        // }
    }

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Credits(){
        //SceneManager.LoadScene("CreditScene");
    }

    public void MainMenu(){
        //SceneManager.LoadScene("MainMenu");
    }

    public void EndScene(){
        //SceneManager.LoadScene("EndScene");
    }
}