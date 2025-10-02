using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL4 : MonoBehaviour
{
    public static ProductManagerL4 Instance;

    // ОБЩИЕ ПЕРЕМЕННЫЕ
    [HideInInspector] public GameObject currentOpenPanel;

    // --- ПЕРЕМЕННЫЕ UI для Уровня 4 (Перетащить в Инспекторе) ---
    public GameObject mainInteractionPanelL4;
    public GameObject productAPanel; // WCream
    public GameObject productBPanel; // Lipstick
    public GameObject productCPanel; // FCream
    public GameObject productDPanel; // Hand Cream

    // --- СОСТОЯНИЕ АНАЛИЗА (Ключевой момент) ---
    // True, если продукт был успешно проанализирован на правильной машине.
    private Dictionary<string, bool> productsAnalyzed = new Dictionary<string, bool>()
    {
        {"WCream", false}, {"Lipstick", false}, {"FCream", false}, {"HCream", false}
    };

    // --- СОСТОЯНИЕ РЕШЕНИЙ ---
    private Dictionary<string, bool?> productDecisionsL4 = new Dictionary<string, bool?>()
    {
        {"WCream", null}, {"Lipstick", null}, {"FCream", null}, {"HCream", null}
    };

    // Правила игры: WCream, Lipstick, FCream - Reject; HCream - Approve
    private readonly Dictionary<string, bool> correctDecisionsL4 = new Dictionary<string, bool>()
    {
        {"WCream", false},    // Reject (Hydroquinone)
        {"Lipstick", false},  // Reject (Lead)
        {"FCream", false},    // Reject (Parabens)
        {"HCream", true}      // Approve (Safe)
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Вызывается из *любого* обработчика (BiosensorHandlerL4, ICMSHandlerL4, ELISAHandler)
    public void MarkProductAsAnalyzed(string productKey)
    {
        if (productsAnalyzed.ContainsKey(productKey) && !productsAnalyzed[productKey])
        {
            productsAnalyzed[productKey] = true;
            Debug.Log("L4 Analysis Complete for: " + productKey);
        }
    }

    public bool IsAnalysisComplete()
    {
        int count = 0;
        foreach (var pair in productsAnalyzed) { if (pair.Value) count++; }
        return count >= 4; // Разблокировка, когда проанализированы все 4
    }

    public void OpenProductPanel(string productKey)
    {
        if (mainInteractionPanelL4 != null) mainInteractionPanelL4.SetActive(false);

        switch (productKey)
        {
            case "WCream": productAPanel.SetActive(true); currentOpenPanel = productAPanel; break;
            case "Lipstick": productBPanel.SetActive(true); currentOpenPanel = productBPanel; break;
            case "FCream": productCPanel.SetActive(true); currentOpenPanel = productCPanel; break;
            case "HCream": productDPanel.SetActive(true); currentOpenPanel = productDPanel; break;
        }
    }

    private void ProcessDecision(string productKey, bool isApproved)
    {
        productDecisionsL4[productKey] = isApproved;

        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }
        if (mainInteractionPanelL4 != null)
        {
            mainInteractionPanelL4.SetActive(true);
        }
    }

    public void ApproveProduct(string productKey) { ProcessDecision(productKey, true); }
    public void RejectProduct(string productKey) { ProcessDecision(productKey, false); }

    // Проверка всех решений (вызывается CheckDeskHandlerL4)
    public bool CheckAllDecisions()
    {
        foreach (var pair in correctDecisionsL4)
        {
            if (productDecisionsL4[pair.Key] == null || productDecisionsL4[pair.Key] != pair.Value)
            {
                return false;
            }
        }
        return true;
    }
}
