using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL2 : MonoBehaviour
{
    // Singleton для доступа из других скриптов
    public static ProductManagerL2 Instance;

    // ОБЩИЕ ПЕРЕМЕННЫЕ
    [HideInInspector] public GameObject currentOpenPanel; // Сюда ничего не перетаскивать

    // --- ПЕРЕМЕННЫЕ UI для Уровня 2 (Перетащить в Инспекторе) ---
    public GameObject mainInteractionPanelL2; // Главная панель 4 иконок (5 png, где 4 - продукты, 1 - фон)
    public GameObject wc1Panel;              // Панель Whitening Cream #1
    public GameObject wc2Panel;              // Панель Whitening Cream #2
    public GameObject wc3Panel;              // Панель Whitening Cream #3
    public GameObject wc4Panel;              // Панель Whitening Cream #4

    // --- СОСТОЯНИЕ И ПРАВИЛА Уровня 2 ---
    [HideInInspector] public bool isAnalysisComplete = false; // Разблокировка InteractionHandlerL2
    [HideInInspector] public int analyzedProductCount = 0;    // Счетчик для биосенсора (для отображения, не для уникальности)

    private Dictionary<string, bool?> productDecisionsL2 = new Dictionary<string, bool?>()
    {
        {"WC1", null}, {"WC2", null}, {"WC3", null}, {"WC4", null}
    };

    // Правила игры: WC1 и WC2 - Reject (Hydroquinone); WC3 и WC4 - Approve
    private readonly Dictionary<string, bool> correctDecisionsL2 = new Dictionary<string, bool>()
    {
        {"WC1", false},
        {"WC2", false},
        {"WC3", true},
        {"WC4", true}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // --- МЕТОДЫ РЕШЕНИЯ Уровня 2 ---

    // 1. Открытие панели продукта
    public void OpenProductPanel(string productName)
    {
        if (mainInteractionPanelL2 != null) mainInteractionPanelL2.SetActive(false);

        switch (productName)
        {
            case "WC1": wc1Panel.SetActive(true); currentOpenPanel = wc1Panel; break;
            case "WC2": wc2Panel.SetActive(true); currentOpenPanel = wc2Panel; break;
            case "WC3": wc3Panel.SetActive(true); currentOpenPanel = wc3Panel; break;
            case "WC4": wc4Panel.SetActive(true); currentOpenPanel = wc4Panel; break;
        }
    }

    // 2. Внутренняя логика принятия решения (вызывается кнопками)
    private void ProcessDecision(string productName, bool isApproved)
    {
        productDecisionsL2[productName] = isApproved;

        // Закрываем текущую панель и открываем главную панель
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }
        if (mainInteractionPanelL2 != null)
        {
            mainInteractionPanelL2.SetActive(true);
        }
    }

    // 3. Функции для кнопок Approve/Reject (настроить в Инспекторе!)
    public void ApproveProduct(string productName) { ProcessDecision(productName, true); }
    public void RejectProduct(string productName) { ProcessDecision(productName, false); }


    // 4. Проверка всех решений (вызывается CheckDeskHandlerL2)
    public bool CheckAllDecisions()
    {
        foreach (var pair in correctDecisionsL2)
        {
            // Проверяем, что решение принято И оно правильное
            if (productDecisionsL2[pair.Key] == null || productDecisionsL2[pair.Key] != pair.Value)
            {
                return false;
            }
        }
        return true;
    }
}