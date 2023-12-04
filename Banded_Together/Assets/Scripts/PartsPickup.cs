using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsPickup : MonoBehaviour {
    public GameHandler gameHandlerObj;
    public Image part;
    //public playerVFX playerPowerupVFX;

    void Start(){
        gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        part.gameObject.SetActive(false);
        //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            GetComponent<Collider2D>().enabled = false;
            //GetComponent< AudioSource>().Play();
            StartCoroutine(DestroyThis());
            gameHandlerObj.AddParts();
            part.gameObject.SetActive(true);
        }
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
