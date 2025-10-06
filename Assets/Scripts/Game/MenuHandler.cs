using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    // ���������� 'Lobby' ��� ������ ����� ����� (��������, 1) � ����������.
    public string lobbySceneName = "Lobby";

    /// <summary>
    /// ��������� ����� ����� � ��������.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(lobbySceneName);
    }

    /// <summary>
    /// ��������� ����������.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("����� �� ����...");
        Application.Quit();

        // ��� ������ � ��������� Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}