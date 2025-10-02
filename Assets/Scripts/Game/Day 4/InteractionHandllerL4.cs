using UnityEngine;

public class InteractionHandlerL4 : MonoBehaviour
{
    public GameObject ePromptUI;            // UI с подсказкой "Нажмите E"
    public GameObject mainInteractionPanelL4; // Панель принятия решений (Approve/Reject)

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL4 != null) mainInteractionPanelL4.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Убеждаемся, что менеджер существует
            if (ProductManagerL4.Instance == null)
            {
                Debug.LogError("ProductManagerL4 not found!");
                return;
            }

            // Проверка, что ВСЕ 4 продукта проанализированы
            if (!ProductManagerL4.Instance.IsAnalysisComplete())
            {
                // Если анализ не завершен, панель не открываем.
                Debug.Log("L4: Not all products analyzed yet. Cannot proceed to decision desk.");
                return;
            }

            // Если анализ завершен, открываем/закрываем панель
            bool isPanelOpen = mainInteractionPanelL4.activeSelf;
            mainInteractionPanelL4.SetActive(!isPanelOpen);

            // Подсказка E должна быть видна, только когда панель закрыта
            ePromptUI.SetActive(isPanelOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что вошел игрок
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // Подсказка "E" появляется ТОЛЬКО, если анализ завершен
            if (ProductManagerL4.Instance != null && ProductManagerL4.Instance.IsAnalysisComplete())
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
            if (mainInteractionPanelL4 != null && mainInteractionPanelL4.activeSelf)
            {
                mainInteractionPanelL4.SetActive(false);
            }
        }
    }
}
