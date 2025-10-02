using UnityEngine;

public class CheckDeskHandlerL3 : MonoBehaviour
{
    // UI элементы, которые нужно перетащить в Инспекторе
    // Прикрепите этот скрипт к объекту-триггеру (например, CheckDeskTriggerL3)
    public GameObject checkPromptUI;     // UI с подсказкой "Нажмите E для проверки"
    public GameObject checkPanelUI;      // Главная панель для отображения результата проверки (Модальное окно)
    public GameObject successImage;      // Изображение/текст, показывающий успешное прохождение
    public GameObject failureImage;      // Изображение/текст, показывающий провал

    private bool isInRange = false;

    void Start()
    {
        // Изначально скрываем все UI
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
    }

    void Update()
    {
        // Обработка нажатия клавиши 'E' для открытия/закрытия панели проверки
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI == null || ProductManagerL3.Instance == null)
            {
                Debug.LogError("Error: Check UI Panel or ProductManagerL3 is not set correctly in CheckDeskHandlerL3.");
                return;
            }

            if (checkPanelUI.activeSelf)
            {
                // Закрытие панели
                checkPanelUI.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            else
            {
                // Открытие и Проверка Решений
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // Вызываем проверку всех решений из ProductManagerL3
                bool allCorrect = ProductManagerL3.Instance.CheckAllDecisions();

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
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Показываем подсказку, когда игрок входит в триггер
            if (checkPromptUI != null) checkPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // Скрываем подсказку
            if (checkPromptUI != null) checkPromptUI.SetActive(false);

            // Если панель открыта, закрываем её при выходе
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
        }
    }
}
