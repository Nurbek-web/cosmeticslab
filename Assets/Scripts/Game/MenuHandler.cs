using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    // Установите 'Lobby' или индекс сцены лобби (например, 1) в инспекторе.
    public string lobbySceneName = "Lobby";

    /// <summary>
    /// Загружает сцену лобби с уровнями.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(lobbySceneName);
    }

    /// <summary>
    /// Закрывает приложение.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();

        // Для работы в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}