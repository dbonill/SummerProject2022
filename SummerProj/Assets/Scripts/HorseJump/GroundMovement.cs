using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MovementSystem
{
    public GameManagerHJ gameManager;
    public Transform Start;
    public Transform End;
    private void FixedUpdate()
    {
        if (transform.position.x <= End.position.x)
        {
            transform.position = Start.position;
        }

        if (gameManager.startGame)
            TranslateObjToLeft(transform);
        

    }

}
