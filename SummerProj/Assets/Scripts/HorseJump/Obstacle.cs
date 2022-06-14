using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovementSystem
{
    public Rigidbody2D rb;
    public float destroyObjAfterXSeconds = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MoveObjToLeft(rb);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Score"))
            Destroy(gameObject, destroyObjAfterXSeconds);
    }

}
