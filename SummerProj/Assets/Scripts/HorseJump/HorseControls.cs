using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseControls : MovementSystem
{
    public float jumpForce = 5f;
    public KeyCode Jump = KeyCode.Space;
    public bool isGrounded = true;

    public Rigidbody2D rb;
    public Transform groundChecker;
    public LayerMask Ground;
    public float distance = 1f;

    public GameManagerHJ GameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, groundChecker.position - transform.position, Color.green);
    }


    void checkIfGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, groundChecker.position - transform.position, distance, Ground);
    }


    // Update is called once per frame
    void Update()
    {
        checkIfGrounded();

        if ((Input.touchCount > 0 || Input.GetKeyDown(Jump)) && isGrounded)
        {
            ObjImpulseUp(rb, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
            GameManager.EndGame();
    }


}
