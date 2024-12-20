using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public float titleMoveSpeed;
    public float titleFinalPosY;
    public GameObject howToPlay;
    //public SpriteRenderer howToPlayImage;
    private float howToPlayhidTimeStamp;

    void Start()
    {
        //HideHowToPlay();
    }
    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // PlayGame();
            if (howToPlay.transform.position.x < 10000)
                HideHowToPlay();
        }

        // Check if any mouse button is clicked (left, right, or middle)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            //  PlayGame();
            if (howToPlay.transform.position.x < 10000)
                HideHowToPlay();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // show how to play
    public void ShowHowToPlay()
    {
        if (Time.time - howToPlayhidTimeStamp > 0.2f)
        {
            howToPlay.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.pixelWidth/2, howToPlay.transform.position.y, 0);
        }
    }
    // show how to play
    public void HideHowToPlay()
    {
        howToPlay.transform.position = new Vector3(100000f, howToPlay.transform.position.y, 0);
        howToPlayhidTimeStamp = Time.time;
    }
}
