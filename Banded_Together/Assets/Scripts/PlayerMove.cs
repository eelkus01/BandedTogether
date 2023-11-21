using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMoveAround : MonoBehaviour
{

    //public Animator anim;
    //public AudioSource WalkSFX;
    public Rigidbody2D rb2D;
    private bool FaceRight = true; // determine which way player is facing.
    public static float runSpeed = 7f;
    public float startSpeed = 7f;
    public bool isAlive = true;

    public float doubleTapTimeThreshold = 0.2f;
    private float llastTapTime;
    private float rlastTapTime;
    private float ulastTapTime;
    private float dlastTapTime;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.1f;
    public float dashingCooldown = 1f;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
        if (isDashing)
        {
            return;
        }
        Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        if (isAlive == true)
        {

            transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
            {
                //     anim.SetBool ("Walk", true);
                //     if (!WalkSFX.isPlaying){
                //           WalkSFX.Play();
                //     }
            }
            else
            {
                //     anim.SetBool ("Walk", false);
                //     WalkSFX.Stop();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && canDash)
            {
                if (Time.time - llastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap left arrow action
                    StartCoroutine(Dash(1));
                }

                llastTapTime = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && canDash)
            {
                if (Time.time - rlastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap right arrow action
                    StartCoroutine(Dash(2));
                }

                rlastTapTime = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && canDash)
            {
                if (Time.time - ulastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap up arrow action
                    StartCoroutine(Dash(3));
                }

                ulastTapTime = Time.time;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && canDash)
            {
                if (Time.time - dlastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap down arrow action
                    StartCoroutine(Dash(4));
                }

                dlastTapTime = Time.time;
            }

            // Turning. Reverse if input is moving the Player right and Player faces left.
            // if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
            //     playerTurn();
            // }
        }
        clampPlayerMovement();
    }

    private void playerTurn()
    {
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //keep player within campera bounds
    //REMOVE IF NOT NEEDED
    void clampPlayerMovement()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private IEnumerator Dash(int direction)
{
    canDash = false;
    isDashing = true;

    // Store the original velocity
    Vector2 originalVelocity = rb2D.velocity;
    
    // Calculate the dash direction
    Vector2 dashDirection = Vector2.zero;
    switch (direction)
    {
        case 1: // Left
            dashDirection = Vector2.left;
            break;
        case 2: // Right
            dashDirection = Vector2.right;
            break;
        case 3: // Up
            dashDirection = Vector2.up;
            break;
        case 4: // Down
            dashDirection = Vector2.down;
            break;
    }

    // Apply an immediate force for the dash
    rb2D.velocity = dashDirection * dashingPower;

    // Dash duration
    yield return new WaitForSeconds(dashingTime);

    // Restore the original velocity
    rb2D.velocity = originalVelocity;

    isDashing = false;

    // Cooldown period after dashing
    yield return new WaitForSeconds(dashingCooldown);
    canDash = true;
}


}