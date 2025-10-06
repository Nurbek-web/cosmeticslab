using UnityEngine;

public class CheckDeskHandlerL6 : MonoBehaviour
{
    // ... (публичные поля без изменений)
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    // !!! ВАЖНО: Установите номер текущего дня (1 для Day1, 2 для Day2 и т.д.)
    public int currentLevel = 6;

    private bool isInRange = false;

    void Start()
    {
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI == null || ProductManagerL6.Instance == null)
            {
                Debug.LogError("Ошибка: Не установлены UI-панель проверки или ProductManager.");
                return;
            }

            if (checkPanelUI.activeSelf)
            {
                // Закрытие
                checkPanelUI.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            else
            {
                // Открытие и Проверка
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // Вызываем проверку
                bool allCorrect = ProductManagerL6.Instance.CheckAllDecisions();

                checkPanelUI.SetActive(true);

                if (successImage != null && failureImage != null)
                {
                    successImage.SetActive(allCorrect);
                    failureImage.SetActive(!allCorrect);
                }

                // =======================================================
                // !!! НОВАЯ ЛОГИКА: СОХРАНЕНИЕ ПРОГРЕССА ПРИ УСПЕХЕ !!!
                // =======================================================
                if (allCorrect)
                {
                    // Вызываем статический метод для сохранения прогресса. 
                    // Это открывает следующий день (currentLevel + 1)
                    ProgressManager.CompleteDay(currentLevel);
                }
                // =======================================================
            }
        }
    }

    // ... (методы OnTriggerEnter2D и OnTriggerExit2D без изменений)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (checkPromptUI != null) checkPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (checkPromptUI != null) checkPromptUI.SetActive(false);
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
        }
    }
}