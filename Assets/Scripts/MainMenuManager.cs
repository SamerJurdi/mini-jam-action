using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public float titleMoveSpeed;
    public float titleFinalPosY;
    public GameObject howToPlay;
    //public SpriteRenderer howToPlayImage;
    private bool isHowToPlayImageDisplaying = false;

    void Start()
    {
        //howToPlayImage = howToPlay.GetComponent<Image>();
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
        //howToPlayImage.SetEnabled(true);
        howToPlay.transform.position = new Vector3(533, 333, 0);
        isHowToPlayImageDisplaying = true;
    }
    // show how to play
    public void HideHowToPlay()
    {
        //howToPlayImage.SetEnabled(false);
        howToPlay.transform.position = new Vector3(100000f,0,0);
        isHowToPlayImageDisplaying = false;
    }
}
