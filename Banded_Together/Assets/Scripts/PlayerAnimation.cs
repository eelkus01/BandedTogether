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

        if (x != 0 || y != 0) {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        
    }
}
