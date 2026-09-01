using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.NewGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}