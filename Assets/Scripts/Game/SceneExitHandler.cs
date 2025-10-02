using UnityEngine;
using UnityEngine.SceneManagement; // Обязательно для работы сценами!

public class SceneExitHandler : MonoBehaviour
{
    // Перетащите сюда из Инспектора
    public GameObject exitPromptUI; // Ваш PNG "Exit"

    // Индекс сцены, на которую нужно переключиться (например, 0 - сцена Меню)
    public int menuSceneIndex = 0;

    private bool isInRange = false; // Флаг, находится ли персонаж в области двери

    void Start()
    {
        // Убедимся, что подсказка невидима в начале
        if (exitPromptUI != null)
        {
            exitPromptUI.SetActive(false);
        }
    }

    void Update()
    {
        // Проверяем, находится ли персонаж в области и нажата ли клавиша 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Вызываем функцию смены сцены
            LoadMenuScene();
        }
    }

    // Эта функция вызывается, когда другой коллайдер входит в Триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что вошел именно персонаж по тегу "Player"
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Показываем PNG "Exit"
            if (exitPromptUI != null)
            {
                exitPromptUI.SetActive(true);
            }
        }
    }

    // Эта функция вызывается, когда другой коллайдер выходит из Триггера
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // Скрываем PNG "Exit"
            if (exitPromptUI != null)
            {
                exitPromptUI.SetActive(false);
            }
        }
    }

    // Функция для загрузки сцены
    private void LoadMenuScene()
    {
        // SceneManager.LoadScene загружает сцену по индексу
        SceneManager.LoadScene(menuSceneIndex);
    }
}