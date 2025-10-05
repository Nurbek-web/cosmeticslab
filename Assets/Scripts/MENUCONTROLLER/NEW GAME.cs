using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // --- ������� ������������ ���� ---
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // --- ������� � Lobby ����� ������ �� ������ ---
    private static int nextLobbyIndex = 0; // ����� ����� ������������

    public static void ReturnToLobby(int lobbyIndex)
    {
        nextLobbyIndex = lobbyIndex;
        SceneManager.LoadScene("Lobby"); // ��������� ����� Lobby
    }

    public static int GetNextLobbyIndex()
    {
        return nextLobbyIndex;
    }
}
