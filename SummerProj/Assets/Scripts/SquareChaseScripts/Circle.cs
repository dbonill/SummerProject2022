using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

    public GameSettings gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && gameManager.startTheGame)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            transform.position = touchPosition;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        gameManager.GameOver = true;
        gameManager.gameOver();
        
    }

}
