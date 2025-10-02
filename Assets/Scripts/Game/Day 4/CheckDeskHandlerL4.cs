using UnityEngine;

public class CheckDeskHandlerL4 : MonoBehaviour
{
    public GameObject checkPromptUI;     // UI с подсказкой "Нажмите E"
    public GameObject checkPanelUI;      // Панель, показывающая результат проверки
    public GameObject successImage;      // Изображение/текст при успешной проверке
    public GameObject failureImage;      // Изображение/текст при провальной проверке

    private bool isInRange = false;

    void Start()
    {
        // Инициализация UI: скрываем подсказку и панель
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
        // Скрываем результаты
        if (successImage != null) successImage.SetActive(false);
        if (failureImage != null) failureImage.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI == null || ProductManagerL4.Instance == null) return;

            if (checkPanelUI.activeSelf)
            {
                // Закрытие панели
                checkPanelUI.SetActive(false);
                // Скрываем результаты перед закрытием
                if (successImage != null) successImage.SetActive(false);
                if (failureImage != null) failureImage.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            else
            {
                // Открытие и Проверка Решений
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // Выполняем проверку принятых решений
                bool allCorrect = ProductManagerL4.Instance.CheckAllDecisions();

                checkPanelUI.SetActive(true);

                // Отображаем соответствующий результат
                if (successImage != null && failureImage != null)
                {
                    successImage.SetActive(allCorrect);
                    failureImage.SetActive(!allCorrect);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если игрок вошел в триггер
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (checkPromptUI != null) checkPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Если игрок вышел из триггера
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (checkPromptUI != null) checkPromptUI.SetActive(false);

            // Закрываем панель проверки, если она была открыта
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
        }
    }
}
