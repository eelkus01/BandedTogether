using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMoveAround>().GetFrozen();
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
    // Called every frame while the trigger is overlapped
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMoveAround>().GetFrozen();
        }
    }
}
