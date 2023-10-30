using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour{
    // NOTE: This script depends on the GameHandler having a public int "thePieces"
    // that is updated with each pickup collected.
    public NavHandler navHandler;
    public string NextLevel = "Jacob_DDR";
    public GameObject exitClosed;
    public GameObject exitOpen;
    public int piecesCollected;
    public int piecesNeeded = 0;

    void Start(){
        navHandler = GameObject.FindWithTag("NavHandler").GetComponent<NavHandler>();
        exitClosed.SetActive(true);
        exitOpen.SetActive(false);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void Update(){
        piecesCollected = navHandler.partsGotten;
        piecesNeeded = navHandler.partsNeeded;
        if (piecesCollected >= piecesNeeded){
            exitClosed.SetActive(false);
            exitOpen.SetActive(true);
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            SceneManager.LoadScene(NextLevel);
        }
    }
}