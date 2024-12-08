using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public float titleMoveSpeed;
    public float titleFinalPosY;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
