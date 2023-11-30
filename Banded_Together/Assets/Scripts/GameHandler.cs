using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    private GameObject player;
    private string sceneName;
    public GameObject partsText;
    public int partsGotten = 0;
    public int partsNeeded = 0;
    public List<InstrumentIndicator> instrumentIndicators;
    public int activeInstrumentID;
    public GameObject deathUI;

    void Start(){
        UpdateParts();
        player = GameObject.FindWithTag("Player");
        GameObject deathUI = GameObject.Find("DeadCanvas");
        deathUI.SetActive(false);

        GameObject[] instrumentIndicatorObjects = GameObject.FindGameObjectsWithTag("InstrumentIndicator");
        instrumentIndicators = new List<InstrumentIndicator>();

        foreach (var obj in instrumentIndicatorObjects) {
            InstrumentIndicator indicator = obj.GetComponent<InstrumentIndicator>();
            if (indicator != null) {
                instrumentIndicators.Add(indicator);
            }
        }
    }

    void Update(){
        CheckForKeyPress();
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
        // SceneManager.LoadScene("Milo_Workspace");
    }

    private void CheckForKeyPress(){
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            UpdateSelectedInstrument(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            UpdateSelectedInstrument(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            UpdateSelectedInstrument(3);
        }
    }

    private void UpdateSelectedInstrument(int selectedInstrumentID){
        activeInstrumentID = selectedInstrumentID;
        foreach (var indicator in instrumentIndicators) {
            indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
        }
    }

    public void handleDeath(){
        Debug.Log("handling death.");
        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReplayLevel(){
        Debug.Log("This is getting called");
        Time.timeScale = 1f;
        // GameHandler_PauseMenu.GameisPaused = false;
        partsGotten = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        SceneManager.LoadScene("MainMenu");
    }

    public void EndScene(){
        //SceneManager.LoadScene("EndScene");
    }
}