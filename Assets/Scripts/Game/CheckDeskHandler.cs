using UnityEngine;

public class CheckDeskHandler : MonoBehaviour
{
    // ... (старые публичные поля)
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    // !!! НОВОЕ ПОЛЕ: ПАНЕЛЬ, КОТОРАЯ ПОЯВИТСЯ ПОСЛЕ УСПЕХА !!!
    [Header("Post-Success UI")]
    public GameObject postSuccessDialoguePanel;

    // !!! ВАЖНО: Установите номер текущего дня (1 для Day1, 2 для Day2 и т.д.)
    public int currentLevel = 1;

    private bool isInRange = false;

    void Start()
    {
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);

        // НОВОЕ: Скрываем диалоговую панель при старте
        if (postSuccessDialoguePanel != null) postSuccessDialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Проверка на null (без изменений)
            if (checkPanelUI == null || ProductManager.Instance == null)
            {
                Debug.LogError("Ошибка: Не установлены UI-панель проверки или ProductManager.");
                return;
            }

            // Логика закрытия панели проверки 'checkPanelUI'
            if (checkPanelUI.activeSelf)
            {
                // Закрытие панели проверки (если не было успеха)
                checkPanelUI.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            // Логика закрытия новой диалоговой панели 'postSuccessDialoguePanel'
            else if (postSuccessDialoguePanel != null && postSuccessDialoguePanel.activeSelf)
            {
                // Если игрок нажимает 'E', когда активна диалоговая панель, 
                // мы предполагаем, что он хочет ее закрыть.
                postSuccessDialoguePanel.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            // Логика открытия и Проверки
            else
            {
                // Скрываем подсказку
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // Вызываем проверку
                bool allCorrect = ProductManager.Instance.CheckAllDecisions();

                // 1. Показываем результат (Success/Failure)
                checkPanelUI.SetActive(true);
                if (successImage != null && failureImage != null)
                {
                    successImage.SetActive(allCorrect);
                    failureImage.SetActive(!allCorrect);
                }

                if (allCorrect)
                {
                    // 2. Сохраняем прогресс
                    ProgressManager.CompleteDay(currentLevel);

                    // =======================================================
                    // !!! НОВАЯ ЛОГИКА: Активация Диалоговой Панели !!!
                    // =======================================================
                    if (postSuccessDialoguePanel != null)
                    {
                        // Скрываем панель проверки (которая показала успех)
                        checkPanelUI.SetActive(false);
                        // Показываем панель с диалогом
                        postSuccessDialoguePanel.SetActive(true);
                    }
                    // =======================================================
                }
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

            // Закрываем обе панели при выходе из зоны триггера
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
            if (postSuccessDialoguePanel != null && postSuccessDialoguePanel.activeSelf)
            {
                postSuccessDialoguePanel.SetActive(false);
            }
        }
    }
}