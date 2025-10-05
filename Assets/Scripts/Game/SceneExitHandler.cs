using UnityEngine;
using UnityEngine.SceneManagement; // Для работы со сценами

public class SceneExitHandler : MonoBehaviour
{
    // PNG с надписью "Exit" (подсказка)
    public GameObject exitPromptUI;

    // Индекс сцены, куда переходить (например, 0 = главное меню)
    public int menuSceneIndex = 0;

    private bool isInRange = false; // игрок у двери?

    void Start()
    {
        if (exitPromptUI != null)
            exitPromptUI.SetActive(false); // скрыть подсказку в начале
    }

    void Update()
    {
        // Проверяем нажатие клавиши Е, если игрок в зоне двери
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            LoadMenuScene();
        }
    }

    // Когда игрок входит в триггер двери
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (exitPromptUI != null)
                exitPromptUI.SetActive(true); // показать PNG Exit
        }
    }

    // Когда игрок выходит из триггера двери
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (exitPromptUI != null)
                exitPromptUI.SetActive(false); // скрыть PNG Exit
        }
    }

    // Загрузка сцены по индексу
    private void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }
}
