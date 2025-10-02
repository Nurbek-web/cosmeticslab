using UnityEngine;

public class InteractionHandlerL5 : MonoBehaviour
{
    public GameObject ePromptUI;              // UI с подсказкой "Нажмите E"
    public GameObject mainInteractionPanelL5; // Панель принятия решений (Approve/Reject)

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL5 != null) mainInteractionPanelL5.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Проверяем наличие менеджера
            if (ProductManagerL5.Instance == null)
            {
                Debug.LogError("ProductManagerL5 not found!");
                return;
            }

            // Проверяем, что ВСЕ продукты проанализированы
            if (!ProductManagerL5.Instance.IsAnalysisComplete())
            {
                Debug.Log("L5: Not all products analyzed yet. Cannot open decision panel.");
                return;
            }

            // Если анализ завершён — открываем/закрываем панель
            bool isPanelOpen = mainInteractionPanelL5.activeSelf;
            mainInteractionPanelL5.SetActive(!isPanelOpen);

            // Подсказка "E" отображается только, если панель закрыта
            ePromptUI.SetActive(isPanelOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что вошёл игрок
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // Подсказка "E" появляется, только если анализ завершён
            if (ProductManagerL5.Instance != null && ProductManagerL5.Instance.IsAnalysisComplete())
            {
                if (ePromptUI != null) ePromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверяем, что вышел игрок
        if (other.CompareTag("Player"))
        {
            isInRange = false;

            if (ePromptUI != null) ePromptUI.SetActive(false);

            // Закрываем панель, если она была открыта
            if (mainInteractionPanelL5 != null && mainInteractionPanelL5.activeSelf)
            {
                mainInteractionPanelL5.SetActive(false);
            }
        }
    }
}
