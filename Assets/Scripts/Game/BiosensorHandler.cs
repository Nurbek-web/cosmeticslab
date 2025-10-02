using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BiosensorHandler : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject biosensorPanelUI;
    public Text analysisResultText;
    public Button detectButton;

    private bool isInRange = false;

    // Отслеживание, какие продукты УЖЕ проанализированы (для уникальности)
    private Dictionary<string, bool> productsAnalyzed = new Dictionary<string, bool>()
    {
        {"WC1", false}, {"WC2", false}, {"WC3", false}, {"WC4", false}
    };

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (biosensorPanelUI != null) biosensorPanelUI.SetActive(false);
        if (detectButton != null)
        {
            detectButton.onClick.AddListener(OnDetectClicked);
        }
    }

    void Update()
    {
        // 1. Управление открытием/закрытием панели по клавише 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = biosensorPanelUI.activeSelf;
            biosensorPanelUI.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen);

            if (isPanelOpen) // Если закрываем, сбрасываем состояние
            {
                // Note: We don't need to clear selectedProductForAnalysis here,
                // as DraggableItem/DropSlot handle that during dragging/dropping.
                analysisResultText.text = "";
            }
        }

        // 2. Активация доступа к InteractionTrigger (Проверяем через ProductManagerL2)
        int count = 0;
        foreach (var pair in productsAnalyzed) { if (pair.Value) count++; }

        if (count >= 4 && ProductManagerL2.Instance != null)
        {
            ProductManagerL2.Instance.isAnalysisComplete = true;
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManager.Instance == null || ProductManagerL2.Instance == null) return;

        // Получаем короткий ключ (e.g., "WC1")
        string productKey = InventoryManager.Instance.selectedProductForAnalysis;

        if (string.IsNullOrEmpty(productKey))
        {
            // Text in English
            analysisResultText.text = "Please drag a product to the slot first!";
            return;
        }

        // Get the full product name from InventoryManager
        string fullName = InventoryManager.Instance.GetProductFullName(productKey);

        // --- Analysis Logic ---
        bool containsHydroquinone = InventoryManager.Instance.AnalyzeProduct();

        // Set results using full name and correct English verdict
        if (containsHydroquinone)
        {
            // RED verdict for dangerous products
            analysisResultText.text = fullName + ": Hydroquinone detected!";
        }
        else
        {
            // GREEN verdict for safe products
            analysisResultText.text = fullName + ": Safe. No Hydroquinone detected.";
        }

        // Mark product as analyzed (only if not already marked)
        if (productsAnalyzed.ContainsKey(productKey) && !productsAnalyzed[productKey])
        {
            productsAnalyzed[productKey] = true;
            // Optionally update a counter here if needed, but the dictionary check is enough for the loop above.
        }

        // We do NOT clear selectedProductForAnalysis here, as it's needed to hold the product in the slot
        // until a new one is dragged in.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (ePromptUI != null) ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);
            if (biosensorPanelUI != null && biosensorPanelUI.activeSelf)
            {
                biosensorPanelUI.SetActive(false);
            }
        }
    }
}