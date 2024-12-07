using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public Camera mC;
    public float leftMostCollision;
    public float rightMostCollision;
    public float highestMostCollision;
    public float lowestMostCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            float mousePosX = mC.ScreenToWorldPoint(Input.mousePosition).x;
            float mousePosY = mC.ScreenToWorldPoint(Input.mousePosition).y;
            if ((mousePosX > leftMostCollision && mousePosX < rightMostCollision) && (mousePosY > lowestMostCollision && mousePosY < highestMostCollision))
            {
                // within the boundaries when clicked
                ActivateButton();
            }
        }
    }

    void ActivateButton()
    {
        //Debug.Log("Button Pressed");
        SceneManager.LoadScene("GameScene");
    }
}
