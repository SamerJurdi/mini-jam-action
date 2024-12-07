using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject GameTitle;
    public float titleMoveSpeed;
    public float titleFinalPosY;

    void Update()
    {
        if(GameTitle.transform.position.y > titleFinalPosY)
        {
            GameTitle.transform.position = new Vector3(GameTitle.transform.position.x, GameTitle.transform.position.y - (titleMoveSpeed * Time.deltaTime), GameTitle.transform.position.z);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
