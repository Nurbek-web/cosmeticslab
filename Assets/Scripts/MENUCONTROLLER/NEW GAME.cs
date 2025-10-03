using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // --- Обычное переключение сцен ---
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // --- Переход в Lobby после выхода из уровня ---
    private static int nextLobbyIndex = 0; // какой лобби активировать

    public static void ReturnToLobby(int lobbyIndex)
    {
        nextLobbyIndex = lobbyIndex;
        SceneManager.LoadScene("Lobby"); // загружаем сцену Lobby
    }

    public static int GetNextLobbyIndex()
    {
        return nextLobbyIndex;
    }
}
