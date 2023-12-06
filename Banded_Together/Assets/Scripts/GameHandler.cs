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
    public bool hasAllParts = false;
    public GameObject[] instrumentIndicatorObjects;

    void Start(){
        UpdateParts();
        player = GameObject.FindWithTag("Player");
        GameObject deathUI = GameObject.Find("DeadCanvas");
        deathUI.SetActive(false);

        instrumentIndicatorObjects = GameObject.FindGameObjectsWithTag("InstrumentIndicator");
        instrumentIndicators = new List<InstrumentIndicator>();

        //restrict this to only show instrument 1 at start of level 1
        //and instruments 1 and 2 at start of level 2
        foreach (var obj in instrumentIndicatorObjects) {
            InstrumentIndicator indicator = obj.GetComponent<InstrumentIndicator>();
            if (indicator != null) {
                instrumentIndicators.Add(indicator);
            }
        }

        //make only 1st instrument visible to start
        if (SceneManager.GetActiveScene().name == "DrumLevel") {
            instrumentIndicatorObjects[0].SetActive(true);
            instrumentIndicatorObjects[1].SetActive(false);
            instrumentIndicatorObjects[2].SetActive(false);
        } else if (SceneManager.GetActiveScene().name == "PianoLevel") {
            instrumentIndicatorObjects[0].SetActive(true);
            instrumentIndicatorObjects[1].SetActive(true);
            instrumentIndicatorObjects[2].SetActive(false);
        }

        //make only 1st attack available
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
        //check if full instrument is gained
        if (partsGotten == partsNeeded){
            GainInstrument();
        }
    }

    public void StartGame(){
        // Time.timeScale = 1f;
        // GameHandler_PauseMenu.GameisPaused = false;
        // partsGotten = 0;
        // SceneManager.LoadScene("DrumLevel");
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

    //function to made instrument visible only once parts are collected
    //also unlocks door to dragon room
    private void GainInstrument(){
        Debug.Log("Making new attack available");
        hasAllParts = true;
        //make new instrument visible and attack available
        instrumentIndicatorObjects[1].SetActive(true);

        //open Dragon door
        GameObject door = GameObject.Find("NewDoor");
        Destroy(door);
    }

    private void UpdateSelectedInstrument(int selectedInstrumentID){
        //check for all parts before allowing selection
        if (!hasAllParts) {
            if (SceneManager.GetActiveScene().name == "DrumLevel") {
                //just allow singing
                if (selectedInstrumentID == 1) {
                    activeInstrumentID = selectedInstrumentID;
                    foreach (var indicator in instrumentIndicators) {
                        indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
                    }
                } else {
                    Debug.Log("Can't select instrument yet");
                }
            } else if (SceneManager.GetActiveScene().name == "PianoLevel") {
                //just allow singing and drum
                if (selectedInstrumentID == 1 || selectedInstrumentID == 2) {
                    activeInstrumentID = selectedInstrumentID;
                    foreach (var indicator in instrumentIndicators) {
                        indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
                    }
                } else {
                    Debug.Log("Can't select instrument yet");
                }
            }
        }
        // foreach (var indicator in instrumentIndicators) {
        //     indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
        // }
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