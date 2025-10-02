using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ICMSHandler : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject icpmsPanelUI;
    public Text analysisResultText;
    public Button detectButton;

    private bool isInRange = false;

    private Dictionary<string, bool> productsAnalyzed = new Dictionary<string, bool>()
    {
        {"Eyeshadow", false}, {"WCream", false}, {"FCream", false}, {"FPowder", false}
    };

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (icpmsPanelUI != null) icpmsPanelUI.SetActive(false);
        if (detectButton != null)
        {
            detectButton.onClick.AddListener(OnDetectClicked);
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = icpmsPanelUI.activeSelf;
            icpmsPanelUI.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen);

            if (isPanelOpen)
            {
                if (InventoryManagerL3.Instance != null) InventoryManagerL3.Instance.selectedProductForAnalysis = "";
                analysisResultText.text = "";
            }
        }

        // Активация доступа к InteractionHandlerL3
        int count = 0;
        foreach (var pair in productsAnalyzed) { if (pair.Value) count++; }

        if (count >= 4 && ProductManagerL3.Instance != null)
        {
            ProductManagerL3.Instance.isAnalysisComplete = true;
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL3.Instance == null || ProductManagerL3.Instance == null) return;

        // Берем короткий ключ продукта из InventoryManagerL3
        string productKey = InventoryManagerL3.Instance.selectedProductForAnalysis;

        if (string.IsNullOrEmpty(productKey))
        {
            analysisResultText.text = "Please drag a product to the slot first!";
            return;
        }

        // Получаем результат анализа (Hazardous Component или "Safe")
        string analysisResult = InventoryManagerL3.Instance.AnalyzeProduct();
        string fullName = InventoryManagerL3.Instance.GetProductFullName(productKey);

        if (analysisResult == "Safe")
        {
            analysisResultText.text = fullName + ": No heavy metals detected";
        }
        else if (analysisResult == "None")
        {
            analysisResultText.text = "Error: Please select a known product.";
        }
        else
        {
            // Обнаружен опасный металл (Lead или Mercury)
            analysisResultText.text = fullName + ": Hazardous heavy metal detected: " + analysisResult;
        }

        // Учет проанализированных продуктов
        if (productsAnalyzed.ContainsKey(productKey) && !productsAnalyzed[productKey])
        {
            productsAnalyzed[productKey] = true;
        }
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
            if (icpmsPanelUI != null && icpmsPanelUI.activeSelf)
            {
                icpmsPanelUI.SetActive(false);
            }
        }
    }
}