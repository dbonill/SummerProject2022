using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjects : MovementSystem
{
    public float ObjectLifeTime = 9f;
    public void objectLife()
    {
        if (ObjectLifeTime > 0)
        {
            //should be in fixed update
            TranslateObjToLeft(transform);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
    }

    private void FixedUpdate()
    {
        objectLife();
    }

    // Update is called once per frame
    void Update()
    {
        ObjectLifeTime -= Time.deltaTime;
    }
}
