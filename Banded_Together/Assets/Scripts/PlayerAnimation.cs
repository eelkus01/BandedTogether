using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public float x, y;

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHurt"))
        {
            if (x != 0 || y != 0) 
            {
                anim.SetFloat("X", x);
                anim.SetFloat("Y", y);
            }
        }
        
    }

    public void triggerHurtAnimation(){
        Debug.Log("This is getting called correctly.");
        anim.SetTrigger("Hurt");
    }
}
