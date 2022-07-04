using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseControls : MovementSystem
{
    public float jumpForce = 5f;
    public float jumpCoolDown = 0.1f;
    float jumpCoolDownTimer = 0;
    public KeyCode Jump = KeyCode.Space;
    public bool isGrounded = true;

    public Rigidbody2D rb;
    public Transform groundChecker;
    public LayerMask Ground;
    public float distance = 1f;

    public GameManagerHJ GameManager;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        jumpCoolDownTimer = jumpCoolDown;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, groundChecker.position - transform.position, Color.green);
    }


    void checkIfGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, groundChecker.position - transform.position, distance, Ground);
    }

    void ResetJumpCoolDown()
    {
        jumpCoolDownTimer = jumpCoolDown;
    }

    void SubtractJumpCoolDownTimers()
    {
        if(jumpCoolDownTimer > 0)
            jumpCoolDownTimer -= Time.deltaTime;
    }

    //booleans for animations
    bool endTransitionFunc = false;
    bool falling = false;
    void AnimationBoolsAndFloats()
    {

        //falling bool
        if (rb.velocity.y < 0)
            falling = true;
        else
            falling = false;

        anim.SetBool("Falling", falling);

        //set animation bool
        anim.SetBool("Grounded", isGrounded);

        if (endTransitionFunc)
            return;
        if (GameManager.startGame)
        {
            anim.SetBool("StartGame", true);
            endTransitionFunc = true;
        }
    }


    


    // Update is called once per frame
    void Update()
    {
        checkIfGrounded();
        AnimationBoolsAndFloats();
        if ((Input.touchCount > 0 || Input.GetKeyDown(Jump)) && isGrounded && GameManager.startGame && jumpCoolDownTimer <= 0)
        {
            ObjImpulseUp(rb, jumpForce);
            ResetJumpCoolDown();
        }
        if(GameManager.startGame)
            SubtractJumpCoolDownTimers();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            anim.SetBool("Dead", true);
            GameManager.EndGame();
        }
    }

  


}
