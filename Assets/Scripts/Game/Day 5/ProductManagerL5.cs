using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL5 : MonoBehaviour
{
    public static ProductManagerL5 Instance;

    // ОБЩИЕ ПЕРЕМЕННЫЕ
    [HideInInspector] public GameObject currentOpenPanel;

    // --- UI-ПАНЕЛИ для Уровня 5 (перетащить в Инспекторе) ---
    public GameObject mainInteractionPanelL5;
    public GameObject productAPanel; // Nail Polish (NP)
    public GameObject productBPanel; // Face Cream (FCream)
    public GameObject productCPanel; // Mascara
    public GameObject productDPanel; // Hand Cream (HCream)

    // --- СЧЁТЧИК принятых решений (для совместимости с CheckDeskHandlerL5) ---
    public int productsDecided = 0;

    // --- СОСТОЯНИЕ АНАЛИЗА ---
    private Dictionary<string, bool> productsAnalyzed = new Dictionary<string, bool>()
    {
        {"NP", false},
        {"FCream", false},
        {"Mascara", false},
        {"HCream", false}
    };

    // --- СОСТОЯНИЕ РЕШЕНИЙ ---
    private Dictionary<string, bool?> productDecisionsL5 = new Dictionary<string, bool?>()
    {
        {"NP", null},
        {"FCream", null},
        {"Mascara", null},
        {"HCream", null}
    };

    // --- ПРАВИЛЬНЫЕ РЕШЕНИЯ ---
    private readonly Dictionary<string, bool> correctDecisionsL5 = new Dictionary<string, bool>()
    {
        {"NP", false},      // Reject (Toluene)
        {"FCream", false},  // Reject (Hydroquinone)
        {"Mascara", true},  // Approve (Safe)
        {"HCream", true}    // Approve (Safe)
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ======================================================
    // АНАЛИЗ ПРОДУКТОВ
    // ======================================================
    public void MarkProductAsAnalyzed(string productKey)
    {
        if (productsAnalyzed.ContainsKey(productKey) && !productsAnalyzed[productKey])
        {
            productsAnalyzed[productKey] = true;
            Debug.Log("L5 Analysis Complete for: " + productKey);
        }
    }

    public bool IsAnalysisComplete()
    {
        foreach (var pair in productsAnalyzed)
        {
            if (!pair.Value) return false;
        }
        return true;
    }

    // ======================================================
    // UI: Открытие и закрытие панелей
    // ======================================================
    public void OpenProductPanel(string productKey)
    {
        if (mainInteractionPanelL5 != null)
            mainInteractionPanelL5.SetActive(false);

        switch (productKey)
        {
            case "NP":
                productAPanel.SetActive(true);
                currentOpenPanel = productAPanel;
                break;
            case "FCream":
                productBPanel.SetActive(true);
                currentOpenPanel = productBPanel;
                break;
            case "Mascara":
                productCPanel.SetActive(true);
                currentOpenPanel = productCPanel;
                break;
            case "HCream":
                productDPanel.SetActive(true);
                currentOpenPanel = productDPanel;
                break;
        }
    }

    private void ProcessDecision(string productKey, bool isApproved)
    {
        bool wasDecided = productDecisionsL5[productKey].HasValue;

        productDecisionsL5[productKey] = isApproved;

        // Увеличиваем счётчик только при первом решении
        if (!wasDecided)
            productsDecided++;

        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }

        if (mainInteractionPanelL5 != null)
            mainInteractionPanelL5.SetActive(true);

        Debug.Log($"L5 Decision recorded for {productKey}: {(isApproved ? "APPROVE" : "REJECT")}. Total decided: {productsDecided}");
    }

    public void ApproveProduct(string productKey) => ProcessDecision(productKey, true);
    public void RejectProduct(string productKey) => ProcessDecision(productKey, false);

    // ======================================================
    // ПРОВЕРКА РЕШЕНИЙ
    // ======================================================
    public bool CheckAllDecisions()
    {
        if (productsDecided < productDecisionsL5.Count)
        {
            Debug.LogWarning("L5: Not all products have decisions yet!");
            return false;
        }

        foreach (var pair in correctDecisionsL5)
        {
            if (productDecisionsL5[pair.Key] == null || productDecisionsL5[pair.Key] != pair.Value)
            {
                Debug.LogWarning($"L5: Incorrect decision for {pair.Key}");
                return false;
            }
        }
        return true;
    }

    public bool AreAllDecisionsMade()
    {
        return productsDecided >= productDecisionsL5.Count;
    }

    public bool GetDecision(string productKey)
    {
        return productDecisionsL5.ContainsKey(productKey) && productDecisionsL5[productKey].GetValueOrDefault(false);
    }
}
