using UnityEngine;
using UnityEngine.UI;

public class ICMSHandlerL4 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject panelUI;
    public Text analysisResultText;
    public Button detectButton;

    private bool isInRange = false;

    void Start()
    {
        // Инициализация UI: скрываем подсказку и панель
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (panelUI != null) panelUI.SetActive(false);
        // Добавляем обработчик нажатия на кнопку "Detect"
        if (detectButton != null)
        {
            detectButton.onClick.AddListener(OnDetectClicked);
        }
    }

    void Update()
    {
        // Логика открытия/закрытия панели по 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = panelUI.activeSelf;
            panelUI.SetActive(!isPanelOpen);
            // Если закрываем панель, показываем подсказку "E"
            ePromptUI.SetActive(isPanelOpen);

            // Сброс состояния при закрытии
            if (isPanelOpen)
            {
                if (InventoryManagerL4.Instance != null) InventoryManagerL4.Instance.selectedProductForAnalysis = "";
                analysisResultText.text = "";
            }
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL4.Instance == null || ProductManagerL4.Instance == null) return;

        string productKey = InventoryManagerL4.Instance.selectedProductForAnalysis;

        if (string.IsNullOrEmpty(productKey))
        {
            analysisResultText.text = "Please drag a product to the slot first!";
            return;
        }

        var (component, analyzer) = InventoryManagerL4.Instance.GetAnalysisData(productKey);
        string fullName = InventoryManagerL4.Instance.GetProductFullName(productKey);

        // ПРОВЕРКА: Анализирует ли этот продукт эта машина?
        // Проверяем, что анализатор либо "ICPMS", либо "None" (безопасный продукт, которому не нужен специфический анализатор)
        if (analyzer != "ICPMS" && analyzer != "None")
        {
            // Убедитесь, что сообщаете игроку, какой анализатор нужен
            analysisResultText.text = fullName + ": Incorrect machine! This product requires the " + analyzer + " analyzer";
            return;
        }

        // --- Логика анализа ---
        if (component.Contains("Lead"))
        {
            analysisResultText.text = fullName + ": Lead Acetate (Heavy Metal) detected!";
            ProductManagerL4.Instance.MarkProductAsAnalyzed(productKey);
        }
        else if (component == "Safe" || analyzer == "None")
        {
            // 'Hand Cream' имеет компонент "Safe" и анализатор "None"
            analysisResultText.text = fullName + ": Safe. No heavy metals detected";
            ProductManagerL4.Instance.MarkProductAsAnalyzed(productKey);
        }
        else
        {
            // Если это другой продукт, который не должен здесь быть (например, Paraben/Hydroquinone)
            analysisResultText.text = fullName + ": No heavy metal hazard found. This test is inconclusive. Use " + analyzer ;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если игрок вошел в триггер
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (ePromptUI != null) ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Если игрок вышел из триггера
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);
            // Закрываем панель, если она была открыта
            if (panelUI != null && panelUI.activeSelf)
            {
                panelUI.SetActive(false);
            }
        }
    }
}
