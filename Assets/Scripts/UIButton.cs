using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public Renderer HoverGraphic;
    public float leftMostCollision;
    public float rightMostCollision;
    public float highestMostCollision;
    public float lowestMostCollision;
    public int functionality;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            if ((mousePosX > leftMostCollision && mousePosX < rightMostCollision) && (mousePosY > lowestMostCollision && mousePosY < highestMostCollision))
            {
                // within the boundaries when clicked
                HoverGraphic.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    ActivateButton();
                }
            }
            else
            {
                HoverGraphic.enabled = false;
            }

    }

    void ActivateButton()
    {
        //Debug.Log("Button Pressed");
        if(functionality == 0) // StartButton
            SceneManager.LoadScene("GameScene");
        else if (functionality == 1) // ResetButton
            SceneManager.LoadScene("GameScene");
    }
}
