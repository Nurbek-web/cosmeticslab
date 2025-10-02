using UnityEngine;
using UnityEngine.UI;

public class BiosensorHandlerL4 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject panelUI;
    public Text analysisResultText;
    public Button detectButton;

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (panelUI != null) panelUI.SetActive(false);
        if (detectButton != null)
        {
            detectButton.onClick.AddListener(OnDetectClicked);
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = panelUI.activeSelf;
            panelUI.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen); // Показываем E, когда закрываем панель

            // Сбрасываем выбранный продукт и текст при закрытии
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
        if (analyzer != "Biosensor" && component != "Safe")
        {
            analysisResultText.text = fullName + ": Incorrect machine! This product requires the " + analyzer + " analyzer";
            return;
        }

        // --- Логика анализа ---
        if (component == "Hydroquinone")
        {
            analysisResultText.text = fullName + ": Hydroquinone detected! ";
            ProductManagerL4.Instance.MarkProductAsAnalyzed(productKey);
        }
        else if (component == "Safe")
        {
            analysisResultText.text = fullName + ": Safe. No hazardous components detected";
            ProductManagerL4.Instance.MarkProductAsAnalyzed(productKey);
        }
        else
        {
            analysisResultText.text = fullName + ": No Biosensor-specific hazard found. This test is inconclusive. Use " + analyzer ;
        }
    }

    // --- ИСПРАВЛЕННАЯ ЛОГИКА ТРИГГЕРА ---
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
            if (panelUI != null && panelUI.activeSelf)
            {
                panelUI.SetActive(false);
            }
        }
    }
}
