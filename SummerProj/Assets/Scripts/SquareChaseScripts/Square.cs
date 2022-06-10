using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MovementSystem
{
    public Rigidbody2D rb;
    public Transform circle;
    public GameSettings gameManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameManager.startTheGame)
            MoveTransformToTransform(rb, transform, circle);
    }


}
