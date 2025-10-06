using UnityEngine;
using UnityEngine.SceneManagement; // Для работы со сценами

public class SceneExitHandler : MonoBehaviour
{
    // PNG с надписью "Exit" (подсказка)
    public GameObject exitPromptUI;

    // !!! ИЗМЕНЕНИЕ: Название сцены, куда переходить (должно быть "Lobby")
    [Tooltip("Название сцены, куда возвращаться после выхода из уровня (должно быть 'Lobby')")]
    public string targetSceneName = "Lobby"; // Устанавливаем по умолчанию "Lobby"

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
            LoadTargetScene();
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
            if (isInRange == false && Input.GetKeyDown(KeyCode.E)) // Добавлено, чтобы закрыть подсказку, если игрок пытается выйти, но уже ушел
            {
                // Необязательно, но может быть полезно для сброса состояния
            }
        }
    }

    // Загрузка целевой сцены (Лобби)
    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}