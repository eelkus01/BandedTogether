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

    public GameObject dashCloud;
    public int numberOfClouds = 3;

    public GameObject dashOutline;
    public int numberOfOutlines = 5;

    public float doubleTapTimeThreshold = 0.2f;
    private float llastTapTime;
    private float rlastTapTime;
    private float ulastTapTime;
    private float dlastTapTime;

    private bool canDash = true;
    private bool isDashing;
    private bool isFrozen;
    private bool canBeFrozen = true;
    public float postFrozenDuration = .5f;
    public float frozenDuration = .5f;
    public float dashingPower = 24f;
    public float dashingTime = 0.1f;
    public float dashingCooldown = 1f;
    public float knockbackDuration = .25f;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
        if (isDashing || isFrozen)
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

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && canDash)
            {
                if (Time.time - llastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap left arrow action
                    StartCoroutine(Dash(1));
                }

                llastTapTime = Time.time;
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && canDash)
            {
                if (Time.time - rlastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap right arrow action
                    StartCoroutine(Dash(2));
                }

                rlastTapTime = Time.time;
            }
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && canDash)
            {
                if (Time.time - ulastTapTime < doubleTapTimeThreshold)
                {
                    // Perform double tap up arrow action
                    StartCoroutine(Dash(3));
                }

                ulastTapTime = Time.time;
            }
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && canDash)
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
                StartCoroutine(SpawnDashClouds("horizontal"));
                StartCoroutine(SpawnDashOutlines("left"));
                break;
            case 2: // Right
                dashDirection = Vector2.right;
                StartCoroutine(SpawnDashClouds("horizontal"));
                StartCoroutine(SpawnDashOutlines("right"));
                break;
            case 3: // Up
                dashDirection = Vector2.up;
                StartCoroutine(SpawnDashClouds("vertical"));
                StartCoroutine(SpawnDashOutlines("up"));
                break;
            case 4: // Down
                dashDirection = Vector2.down;
                StartCoroutine(SpawnDashClouds("vertical"));
                StartCoroutine(SpawnDashOutlines("down"));
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

    public void getKnockedBack(Vector2 forceDirection, int knockback)
    {
        ApplyKnockback(forceDirection * knockback);
    }
    private void ApplyKnockback(Vector2 force)
    {
        rb2D.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(ResetKnockback());
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb2D.velocity = Vector2.zero; 
    }

    public void GetFrozen(){
        if(canBeFrozen) {
            GetComponent<PlayerStateManager>().getDamaged(4);
            isFrozen = true;
            canBeFrozen = false;
            rb2D.velocity = Vector2.zero; 
            StartCoroutine(ResetFrozen());
        }
    }

    private IEnumerator ResetFrozen(){
        yield return new WaitForSeconds(frozenDuration);
        isFrozen = false;
        yield return new WaitForSeconds(postFrozenDuration);
        canBeFrozen = true;
    }

    IEnumerator SpawnDashClouds(string playerOrientation)
    {
        for (int i = 0; i < numberOfClouds; i++)
        {
            // Instantiate the prefab
            GameObject newInstance = Instantiate(dashCloud, transform.position, Quaternion.identity); // Adjust position as needed

            // Access the Animator and set the trigger
            Animator animator = newInstance.GetComponent<Animator>();
            if (playerOrientation == "vertical")
            {
                animator.SetTrigger("ActivateVertical"); // Replace with your trigger name
            }
            else{
                animator.SetTrigger("ActivateHorizontal");
            }

            // Wait for a fraction of the total duration before spawning the next object
            yield return new WaitForSeconds(dashingTime / numberOfClouds);
        }
    }
    IEnumerator SpawnDashOutlines(string playerOrientation)
    {
        for (int i = 0; i < numberOfOutlines; i++)
        {
            // Instantiate the prefab
            GameObject newInstance = Instantiate(dashOutline, transform.position, Quaternion.identity); // Adjust position as needed

            // Access the Animator and set the trigger
            Animator animator = newInstance.GetComponent<Animator>();
            if (playerOrientation == "left")
            {
                animator.SetTrigger("ActivateOutlineLeft"); // Replace with your trigger name
            }
            else if (playerOrientation =="right"){
                animator.SetTrigger("ActivateOutlineRight");
            }
            else if (playerOrientation =="up"){
                animator.SetTrigger("ActivateOutlineUp");
            }
            else{
                animator.SetTrigger("ActivateOutlineDown");
            }

            // Wait for a fraction of the total duration before spawning the next object
            yield return new WaitForSeconds(dashingTime / numberOfOutlines);
        }
    }
}