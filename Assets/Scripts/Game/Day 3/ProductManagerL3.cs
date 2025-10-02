using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL3 : MonoBehaviour
{
    public static ProductManagerL3 Instance;

    // ОБЩИЕ ПЕРЕМЕННЫЕ
    [HideInInspector] public GameObject currentOpenPanel;

    // --- ПЕРЕМЕННЫЕ UI для Уровня 3 (Перетащить в Инспекторе) ---
    public GameObject mainInteractionPanelL3; // Главная панель 4 иконок
    public GameObject productAPanel;         // Панель Eyeshadow
    public GameObject productBPanel;         // Панель Whitening Cream
    public GameObject productCPanel;         // Панель Face Cream
    public GameObject productDPanel;         // Панель Face Powder

    // --- СОСТОЯНИЕ И ПРАВИЛА Уровня 3 ---
    [HideInInspector] public bool isAnalysisComplete = false; // Флаг для разблокировки InteractionHandlerL3

    private Dictionary<string, bool?> productDecisionsL3 = new Dictionary<string, bool?>()
    {
        {"Eyeshadow", null}, {"WCream", null}, {"FCream", null}, {"FPowder", null}
    };

    // Правила игры: Eyeshadow и Whitening Cream - Reject (Lead/Mercury); Face Cream/Powder - Approve (Safe)
    private readonly Dictionary<string, bool> correctDecisionsL3 = new Dictionary<string, bool>()
    {
        {"Eyeshadow", false}, // Reject (Lead)
        {"WCream", false},    // Reject (Mercury)
        {"FCream", true},     // Approve (Safe)
        {"FPowder", true}     // Approve (Safe)
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // --- МЕТОДЫ РЕШЕНИЯ Уровня 3 (Открытие UI и Решения) ---

    // Вызывается при нажатии иконки на главной панели L3
    public void OpenProductPanel(string productKey)
    {
        if (mainInteractionPanelL3 != null) mainInteractionPanelL3.SetActive(false);

        switch (productKey)
        {
            case "Eyeshadow": productAPanel.SetActive(true); currentOpenPanel = productAPanel; break;
            case "WCream": productBPanel.SetActive(true); currentOpenPanel = productBPanel; break;
            case "FCream": productCPanel.SetActive(true); currentOpenPanel = productCPanel; break;
            case "FPowder": productDPanel.SetActive(true); currentOpenPanel = productDPanel; break;
        }
    }

    // Внутренняя логика принятия решения
    private void ProcessDecision(string productKey, bool isApproved)
    {
        productDecisionsL3[productKey] = isApproved;

        // Закрываем текущую панель и открываем главную панель
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }
        if (mainInteractionPanelL3 != null)
        {
            mainInteractionPanelL3.SetActive(true);
        }
    }

    // Функции для кнопок Approve/Reject (настроить в Инспекторе!)
    public void ApproveProduct(string productKey) { ProcessDecision(productKey, true); }
    public void RejectProduct(string productKey) { ProcessDecision(productKey, false); }


    // Проверка всех решений (вызывается CheckDeskHandlerL3)
    public bool CheckAllDecisions()
    {
        foreach (var pair in correctDecisionsL3)
        {
            // Проверяем, что решение принято И оно правильное
            if (productDecisionsL3[pair.Key] == null || productDecisionsL3[pair.Key] != pair.Value)
            {
                return false;
            }
        }
        return true;
    }
}