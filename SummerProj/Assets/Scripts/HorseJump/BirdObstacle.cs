using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdObstacle : MovementSystem
{
    public Rigidbody2D rb;
    public float destroyObjAfterXSeconds = 1f;
    public float verticalSpeed = 4000f;
    public float addOrSubToY;
    public float newYpos;
    public bool moveUP;
    bool MoveY = false;

    // Start is called before the first frame update
    void Start()
    {
        newYpos = transform.position.y + addOrSubToY;
    }

    void moveObjectVertically()
    {
        if (MoveY)
        {
            if (addOrSubToY > 0)
            {
                if (transform.position.y >= newYpos)
                {
                    Debug.Log("Case A");
                    MoveY = false;
                }
            }
            else if (addOrSubToY < 0)
            {
                if (transform.position.y <= newYpos)
                {
                    Debug.Log("Case B");
                    MoveY = false;
                }
            }

            AddSubToYAxis(rb, verticalSpeed, moveUP);

        }
    }

    private void FixedUpdate()
    {
        MoveObjToLeft(rb);
        moveObjectVertically();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
            Destroy(gameObject, destroyObjAfterXSeconds);
        if (collision.gameObject.CompareTag("DipZone"))
            MoveY = true;
            

    }

}
