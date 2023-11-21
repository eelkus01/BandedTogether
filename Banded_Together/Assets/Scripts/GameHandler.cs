using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler:MonoBehaviour
{
    public List<InstrumentIndicator> instrumentIndicators;
    public int activeInstrumentID;
    public GameObject deathUI;
    
    void Start() {
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
    
    void Update() {
        CheckForKeyPress();
    }

    private void CheckForKeyPress() {
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

    private void UpdateSelectedInstrument(int selectedInstrumentID) {
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

    public void ReloadLevel(){
        Debug.Log("This is getting called");
        // Get the name of the currently active scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Load the current scene by its name, effectively reloading it
        SceneManager.LoadScene(currentSceneName);

        // Time.timeScale = 1f;
        // GameHandler_PauseMenu.GameisPaused = false;
        // playerScore = levelScore;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
