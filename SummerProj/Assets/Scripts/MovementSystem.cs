using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float speed;
    public void MoveTransformToTransform(Rigidbody2D ObjRB, Transform CurrentPos, Transform newPos)
    {
        Vector3 directionVector = (newPos.position - CurrentPos.position).normalized;
        //ObjRB.AddForce(directionVector * speed * Time.deltaTime, ForceMode2D.Force);
        CurrentPos.Translate(directionVector * speed * Time.deltaTime, Space.World);
    }

    public void MoveObjToLeft(Rigidbody2D ObjRB)
    {
        //Obj.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        ObjRB.AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Force);
    }

    public void ObjImpulseUp(Rigidbody2D ObjRB, float jumpForce)
    {
        ObjRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void AddSubToYAxis(Rigidbody2D ObjRB, float ySpeed, bool goUP)
    {
        if (goUP)
            ObjRB.AddForce(Vector2.up * ySpeed * Time.deltaTime, ForceMode2D.Force);
        else
            ObjRB.AddForce(Vector2.down * ySpeed * Time.deltaTime, ForceMode2D.Force);
    }


}
