using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartHandler : MonoBehaviour
{
    public void startGame(){
        Debug.Log("Trying to load screen");
        SceneManager.LoadScene("Milo_Workspace");
    }
}
