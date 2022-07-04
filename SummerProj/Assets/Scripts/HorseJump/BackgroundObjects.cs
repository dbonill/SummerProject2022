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
            TranslateObjToLeft(transform);
        }
        else
        {
            Destroy(this.gameObject);
        }

        ObjectLifeTime -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        objectLife();
    }
}
