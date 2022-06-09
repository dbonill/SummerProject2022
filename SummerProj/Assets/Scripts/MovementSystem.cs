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
}
