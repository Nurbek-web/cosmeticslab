using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Room"); 
    }

    public void Continue()
    {
        SceneManager.LoadScene("Room");
    }

    public void Settings()
    {
        Debug.Log("Settings opened");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
}
