using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsPickup : MonoBehaviour {
    public GameHandler gameHandlerObj;
    public Image part;
    //public Image bigPart;
    public GameObject bigPartObject;
    //public playerVFX playerPowerupVFX;

    void Start(){
        gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        part.gameObject.SetActive(false);
        bigPartObject.gameObject.SetActive(false);
        //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            GetComponent<Collider2D>().enabled = false;
            //GetComponent<AudioSource>().Play();
            StartCoroutine(DestroyThis());
            Destroy(gameObject.transform.GetChild(0).gameObject);
            gameHandlerObj.AddParts();
            bigPartObject.gameObject.SetActive(true);

            StartCoroutine(bigPartScale());

            part.gameObject.SetActive(true);
        }
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    IEnumerator bigPartScale()
    {

        LeanTween.scale(bigPartObject, new Vector3(.5f, .5f, .5f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.move(bigPartObject, bigPartObject.transform.position + new Vector3(200f, 100f, 0f), 1f);
        yield return new WaitForSeconds(1);
        bigPartObject.gameObject.SetActive(false);
    }
}


