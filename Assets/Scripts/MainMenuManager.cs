using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public float titleMoveSpeed;
    public float titleFinalPosY;

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            PlayGame();
        }

        // Check if any mouse button is clicked (left, right, or middle)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
