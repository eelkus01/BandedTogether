using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseHandler : MonoBehaviour
{
    public Tween tw;
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    //public AudioMixer mixer;
    //public static float volumeLevel = 1.0f;
    //private Slider sliderVolumeCtrl;

    void Awake (){
        //NOTE: EVERYTHING BELOW IS VOR VOLUME SLIDER
        //UNCOMMENT ONCE SLIDER IS IMPLEMENTED
        // SetLevel(volumeLevel);
        // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        // if (sliderTemp != null){
        //     sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
        //     sliderVolumeCtrl.value = volumeLevel;
        // }
    }

    void Start (){
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameisPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        tw.ButtonPop();
        GameisPaused = true;
    }


    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameisPaused = false;
    }

    // public void SetLevel (float sliderValue){
    //     mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
    //     volumeLevel = sliderValue;
    // }

    public void Exit()
    {
        // Application.Quit();
        // Time.timeScale = 1f;
        // pauseMenuUI.SetActive(false);
        // GameisPaused = false;

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
