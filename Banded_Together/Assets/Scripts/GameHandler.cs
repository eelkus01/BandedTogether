using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour {

    private GameObject player;
    public GameObject checkpointManager;
    private string sceneName;
    public GameObject partsText;
    public int partsGotten = 0;
    public int partsNeeded = 0;
    public List<InstrumentIndicator> instrumentIndicators;
    public int activeInstrumentID;
    public GameObject deathUI;
    public bool hasAllParts = false;
    public GameObject voiceIndicator;
    public GameObject drumIndicator;
    public GameObject pianoIndicator;

    public GameObject[] instrumentIndicatorObjects = new GameObject[3];
    public GameObject healthBar;

    void Start() {
        if (SceneManager.GetActiveScene().name == "LearnEarth" || SceneManager.GetActiveScene().name == "EarthDragonLevel"
            || SceneManager.GetActiveScene().name == "IceDragon" || SceneManager.GetActiveScene().name == "LearnIceAttack"){
            hasAllParts = true;
        } else {
            UpdateParts();
            hasAllParts = false;
        }
        player = GameObject.FindWithTag("Player");
        GameObject deathUI = GameObject.Find("DeadCanvas");
        deathUI.SetActive(false);

        voiceIndicator = GameObject.FindGameObjectWithTag("voiceIndicator");
        drumIndicator = GameObject.FindGameObjectWithTag("drumIndicator");
        pianoIndicator = GameObject.FindGameObjectWithTag("pianoIndicator");


        instrumentIndicatorObjects[0] = voiceIndicator;
        instrumentIndicatorObjects[1] = drumIndicator;
        instrumentIndicatorObjects[2] = pianoIndicator;
        
        instrumentIndicators = new List<InstrumentIndicator>();
        healthBar =  GameObject.FindGameObjectWithTag("HealthBar");

        //restrict this to only show instrument 1 at start of level 1
        //and instruments 1 and 2 at start of level 2
        foreach (var obj in instrumentIndicatorObjects) {
            InstrumentIndicator indicator = obj.GetComponent<InstrumentIndicator>();
            if (indicator != null) {
                instrumentIndicators.Add(indicator);
            }
        }

        //make only some instruments visible to start
        if (SceneManager.GetActiveScene().name == "TutorialLevel"){ // No instruments
            instrumentIndicatorObjects[0].SetActive(false);
            instrumentIndicatorObjects[1].SetActive(false);
            instrumentIndicatorObjects[2].SetActive(false);
        }else if (SceneManager.GetActiveScene().name == "LevelOne") { // Only singing
            instrumentIndicatorObjects[0].SetActive(true);
            instrumentIndicatorObjects[1].SetActive(false);
            instrumentIndicatorObjects[2].SetActive(false);
        } else if (SceneManager.GetActiveScene().name == "LevelTwo" || SceneManager.GetActiveScene().name == "LearnEarth"
                    || SceneManager.GetActiveScene().name == "EarthDragonLevel") { // Singing and Drum
            instrumentIndicatorObjects[0].SetActive(true);
            instrumentIndicatorObjects[1].SetActive(true);
            instrumentIndicatorObjects[2].SetActive(false);
        } else if (SceneManager.GetActiveScene().name == "IceDragon" || SceneManager.GetActiveScene().name == "LearnIceAttack") { //allow all 3
            instrumentIndicatorObjects[0].SetActive(true);
            instrumentIndicatorObjects[1].SetActive(true);
            instrumentIndicatorObjects[2].SetActive(true);
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
        TextMeshProUGUI partsTextB = partsText.GetComponent<TextMeshProUGUI>();
        partsTextB.text = "Parts: " + partsGotten + "/" + partsNeeded;
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
        if (SceneManager.GetActiveScene().name == "TutorialLevel"){ // Make singing
            instrumentIndicatorObjects[0].SetActive(true);
        } else if (SceneManager.GetActiveScene().name == "LevelOne") { // Make earth
            instrumentIndicatorObjects[1].SetActive(true);
        } else if (SceneManager.GetActiveScene().name == "LevelTwo") { // Make ice
            instrumentIndicatorObjects[2].SetActive(true);
        }

        //open Dragon door
        GameObject door = GameObject.Find("NewDoor");
        Destroy(door);
    }

    private void UpdateSelectedInstrument(int selectedInstrumentID){
        //check for all parts before allowing selection
        if (!hasAllParts) {
            if (SceneManager.GetActiveScene().name == "LevelOne") {
                //just allow singing
                if (selectedInstrumentID == 1) {
                    activeInstrumentID = selectedInstrumentID;
                    foreach (var indicator in instrumentIndicators)
                    {
                        indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
                    }
                } else {
                    Debug.Log("Can't select instrument yet");
                }
            } else if (SceneManager.GetActiveScene().name == "LevelTwo") {
                //just allow singing and drum
                if (selectedInstrumentID == 1 || selectedInstrumentID == 2) {
                    activeInstrumentID = selectedInstrumentID;
                    foreach (var indicator in instrumentIndicators)
                    {
                        indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
                    }
                } else {
                    Debug.Log("Can't select instrument yet");
                }
            }

        //once all parts are collected, still don't allow piano selection
        } else if (SceneManager.GetActiveScene().name == "TutorialLevel"){
            if (selectedInstrumentID == 1) {
                activeInstrumentID = selectedInstrumentID;
                foreach (var indicator in instrumentIndicators)
                {
                    indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
                }
            } else {
                Debug.Log("Can't select instrument yet");
            }
        } else if (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LearnEarth"
                    || SceneManager.GetActiveScene().name == "EarthDragonLevel") {
                //allow singing and drum now
                if (selectedInstrumentID == 1 || selectedInstrumentID == 2) {
                    activeInstrumentID = selectedInstrumentID;
                    foreach (var indicator in instrumentIndicators) {
                        indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
                    }
                } else {
                    Debug.Log("Can't select instrument yet");
                }
        } else {
            //assuming we're in LevelTwo, allow all 3 attack selections
            //this applies for IceDragon and LearnIce levels
            activeInstrumentID = selectedInstrumentID;
            foreach (var indicator in instrumentIndicators) {
                indicator.SetSelectedState(indicator.instrumentID == selectedInstrumentID);
            }
        }
    }

    public void handleDeath(){
        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReplayLevel(){
        Transform checkID = checkpointManager.GetComponent<CheckpointManager>().getRespawnPoint();
        if (checkID != null) {
            player.GetComponent<PlayerStateManager>().RespawnPlayer(checkID);
            Time.timeScale = 1f;
            deathUI.SetActive(false);
        } else {
            partsGotten = 0;
            healthBar.GetComponent<HealthBar>().ResetHealth();
            Time.timeScale = 1f;
            deathUI.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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