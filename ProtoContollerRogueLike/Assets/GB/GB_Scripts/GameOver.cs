using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        CursorManager.SetMenuCursor();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        CursorManager.SetFightCursor();
        SceneManager.LoadScene("UD_HUBnewCharacter");
    }

}
