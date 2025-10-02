using UnityEngine;
using UnityEngine.SceneManagement; // ����������� ��� ������ �������!

public class SceneExitHandler : MonoBehaviour
{
    // ���������� ���� �� ����������
    public GameObject exitPromptUI; // ��� PNG "Exit"

    // ������ �����, �� ������� ����� ������������� (��������, 0 - ����� ����)
    public int menuSceneIndex = 0;

    private bool isInRange = false; // ����, ��������� �� �������� � ������� �����

    void Start()
    {
        // ��������, ��� ��������� �������� � ������
        if (exitPromptUI != null)
        {
            exitPromptUI.SetActive(false);
        }
    }

    void Update()
    {
        // ���������, ��������� �� �������� � ������� � ������ �� ������� 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // �������� ������� ����� �����
            LoadMenuScene();
        }
    }

    // ��� ������� ����������, ����� ������ ��������� ������ � �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� ����� ������ �������� �� ���� "Player"
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // ���������� PNG "Exit"
            if (exitPromptUI != null)
            {
                exitPromptUI.SetActive(true);
            }
        }
    }

    // ��� ������� ����������, ����� ������ ��������� ������� �� ��������
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // �������� PNG "Exit"
            if (exitPromptUI != null)
            {
                exitPromptUI.SetActive(false);
            }
        }
    }

    // ������� ��� �������� �����
    private void LoadMenuScene()
    {
        // SceneManager.LoadScene ��������� ����� �� �������
        SceneManager.LoadScene(menuSceneIndex);
    }
}