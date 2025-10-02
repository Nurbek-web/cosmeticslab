using UnityEngine;
using UnityEngine.UI;

public class GCHandler : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject panelUI;
    public Text analysisResultText;
    public Button detectButton;
    public Transform contentArea; // Добавлен для DropSlot

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (panelUI != null) panelUI.SetActive(false);

        // Добавляем слушатель для кнопки Detect
        if (detectButton != null)
        {
            detectButton.onClick.RemoveAllListeners();
            detectButton.onClick.AddListener(OnDetectClicked);
        }

        // Убедимся, что анализ-текст чист при старте
        if (analysisResultText != null)
        {
            analysisResultText.text = "Drag a product here to begin GC analysis.";
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = panelUI.activeSelf;
            panelUI.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen); // Показываем E Prompt, когда закрываем панель

            // Если панель закрывается, сбрасываем выбранный продукт в слоте
            if (isPanelOpen)
            {
                if (InventoryManagerL5.Instance != null)
                {
                    InventoryManagerL5.Instance.selectedProductForAnalysis = "";
                }
            }
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL5.Instance == null || ProductManagerL5.Instance == null) return;

        string productKey = InventoryManagerL5.Instance.selectedProductForAnalysis;

        if (string.IsNullOrEmpty(productKey))
        {
            analysisResultText.text = "Please drag a product to the slot first!";
            return;
        }

        var (component, analyzer) = InventoryManagerL5.Instance.GetAnalysisData(productKey);
        string fullName = InventoryManagerL5.Instance.GetProductFullName(productKey);

        // ПРОВЕРКА: Анализирует ли этот продукт эта машина?
        // Продукт NP (Nail Polish) должен быть проанализирован GC. Безопасные продукты (Mascara, Hand Cream) имеют analyzer == "None" 
        // и component == "Safe", и также могут быть проверены.
        if (analyzer != "GC" && component != "Safe")
        {
            analysisResultText.text = fullName + " Incorrect machine! This product requires the " + analyzer + " analyzer.";
            return;
        }

        // --- Логика анализа ---
        if (component == "Toluene")
        {
            analysisResultText.text = fullName + ": HAZARD DETECTED! Toluene (Toxic Solvent) found!";
            ProductManagerL5.Instance.MarkProductAsAnalyzed(productKey);
        }
        else if (component == "Safe")
        {
            analysisResultText.text = fullName + ": Safe. No volatile organic compounds detected.";
            ProductManagerL5.Instance.MarkProductAsAnalyzed(productKey);
        }
        else
        {
            analysisResultText.text = fullName + ": Test inconclusive for volatile solvents. This machine might not be the correct tool or the component is missing.";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Проверка, что панель не открыта, чтобы не перекрывать подсказку
            if (ePromptUI != null && !panelUI.activeSelf)
            {
                ePromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);
            if (panelUI != null) panelUI.SetActive(false); // Закрываем панель при выходе

            // Сбрасываем выбранный продукт в слоте при закрытии
            if (InventoryManagerL5.Instance != null)
            {
                InventoryManagerL5.Instance.selectedProductForAnalysis = "";
            }
        }
    }
}
