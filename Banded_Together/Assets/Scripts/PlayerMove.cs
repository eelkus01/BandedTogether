using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMoveAround : MonoBehaviour {

    //public Animator anim;
    //public AudioSource WalkSFX;
    public Rigidbody2D rb2D;
    private bool FaceRight = true; // determine which way player is facing.
    public static float runSpeed = 7f;
    public float startSpeed = 7f;
    public bool isAlive = true;

    void Start(){
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update(){
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
        Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        if (isAlive == true){
            transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
            //     anim.SetBool ("Walk", true);
            //     if (!WalkSFX.isPlaying){
            //           WalkSFX.Play();
            //     }
            } else {
            //     anim.SetBool ("Walk", false);
            //     WalkSFX.Stop();
            }

            // Turning. Reverse if input is moving the Player right and Player faces left.
            if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                playerTurn();
            }
        }
        clampPlayerMovement();
    }

    private void playerTurn(){
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //keep player within campera bounds
    //REMOVE IF NOT NEEDED
    void clampPlayerMovement() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}