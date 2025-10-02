using UnityEngine;

public class CheckDeskHandlerL5 : MonoBehaviour
{
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    private bool isInRange = false;

    void Start()
    {
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (successImage != null) successImage.SetActive(false);
        if (failureImage != null) failureImage.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI.activeSelf)
            {
                // Закрытие панели
                checkPanelUI.SetActive(false);
                checkPromptUI.SetActive(true);
            }
            else
            {
                // Проверка, что ВСЕ 4 решения приняты
                // ВНИМАНИЕ: ЭТО ВЫЗЫВАЕТ ОШИБКУ УРОВНЯ ДОСТУПА! 
                // Вам нужно изменить ProductManagerL5.productsDecided на public.
                if (ProductManagerL5.Instance == null || ProductManagerL5.Instance.productsDecided < 4)
                {
                    Debug.Log("L5: Not all product decisions have been recorded yet.");
                    return; // Не открываем, если решения не приняты
                }

                // Открытие и Проверка
                checkPanelUI.SetActive(true);
                checkPromptUI.SetActive(false);

                bool allCorrect = ProductManagerL5.Instance.CheckAllDecisions();

                // Отображение результата
                successImage.SetActive(allCorrect);
                failureImage.SetActive(!allCorrect);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Показываем E Prompt, только если все решения приняты
            // ВНИМАНИЕ: ЭТО ВЫЗЫВАЕТ ОШИБКУ УРОВНЯ ДОСТУПА! 
            // Вам нужно изменить ProductManagerL5.productsDecided на public.
            if (ProductManagerL5.Instance != null && ProductManagerL5.Instance.productsDecided >= 4)
            {
                if (checkPromptUI != null && !checkPanelUI.activeSelf)
                {
                    checkPromptUI.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (checkPromptUI != null) checkPromptUI.SetActive(false);
            if (checkPanelUI != null) checkPanelUI.SetActive(false);

            // Скрываем результат при выходе
            if (successImage != null) successImage.SetActive(false);
            if (failureImage != null) failureImage.SetActive(false);
        }
    }
}
