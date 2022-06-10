using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovementSystem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MoveObjToLeft(transform);
    }
}
