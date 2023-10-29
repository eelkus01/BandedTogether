using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsPickup : MonoBehaviour {
    public NavHandler navHandlerObj;
    //public playerVFX playerPowerupVFX;

    void Start(){
        navHandlerObj = GameObject.FindWithTag("NavHandler").GetComponent<NavHandler>();
        //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            GetComponent<Collider2D>().enabled = false;
            //GetComponent< AudioSource>().Play();
            StartCoroutine(DestroyThis());
            navHandlerObj.AddParts();
        }
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
