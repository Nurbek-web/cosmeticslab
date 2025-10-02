using UnityEngine;
using System.Collections.Generic;

public class ProductManager : MonoBehaviour
{
    // Статический экземпляр для доступа из других скриптов
    public static ProductManager Instance;

    // Словарь для отслеживания выбора игрока
    // Ключ: имя продукта, Значение: true (Approved) или false (Rejected)
    private Dictionary<string, bool?> productDecisions = new Dictionary<string, bool?>()
    {
        {"Shampoo", null},
        {"Sunscreen", null},
        {"FaceCream", null},
        {"Lipstick", null}
    };

    // Правила игры
    private readonly Dictionary<string, bool> correctDecisions = new Dictionary<string, bool>()
    {
        {"Shampoo", true},     // True = Approve
        {"Sunscreen", false},  // False = Reject
        {"FaceCream", true},
        {"Lipstick", false}
    };

    // UI-панели (перетащите из Инспектора)
    public GameObject mainInteractionPanel; // Interaction Panel UI
    public GameObject shampooPanel;
    public GameObject sunscreenPanel;
    public GameObject faceCreamPanel;
    public GameObject lipstickPanel;

    private GameObject currentOpenPanel; // Чтобы знать, какую панель закрыть

    void Awake()
    {
        // Паттерн Singleton для легкого доступа
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Эта функция вызывается при нажатии кнопки на главной панели
    public void OpenProductPanel(string productName)
    {
        // Скрываем основную панель взаимодействия
        mainInteractionPanel.SetActive(false);

        switch (productName)
        {
            case "Shampoo":
                shampooPanel.SetActive(true);
                currentOpenPanel = shampooPanel;
                break;
            case "Sunscreen":
                sunscreenPanel.SetActive(true);
                currentOpenPanel = sunscreenPanel;
                break;
            case "FaceCream":
                faceCreamPanel.SetActive(true);
                currentOpenPanel = faceCreamPanel;
                break;
            case "Lipstick":
                lipstickPanel.SetActive(true);
                currentOpenPanel = lipstickPanel;
                break;
        }
    }

    // Эта функция вызывается кнопками Approve/Reject на панелях косметики
    // (Удаляем старую функцию MakeDecision)

    // НОВАЯ ФУНКЦИЯ: для кнопки "Approve"
    public void ApproveProduct(string productName)
    {
        // Вызываем основную логику, передавая true (Одобрено)
        ProcessDecision(productName, true);
    }

    // НОВАЯ ФУНКЦИЯ: для кнопки "Reject"
    public void RejectProduct(string productName)
    {
        // Вызываем основную логику, передавая false (Отклонено)
        ProcessDecision(productName, false);
    }

    // Закрытая (private) функция, содержащая всю логику принятия решения
    private void ProcessDecision(string productName, bool isApproved)
    {
        // 1. Записываем решение игрока
        if (productDecisions.ContainsKey(productName))
        {
            productDecisions[productName] = isApproved;
        }
        else
        {
            Debug.LogError("ProductManager: Продукт " + productName + " не найден.");
            return;
        }

        // 2. Закрываем текущую панель
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }

        // 3. Снова показываем главную панель взаимодействия
        mainInteractionPanel.SetActive(true);
    }

    // Проверка всех решений перед выходом
    public bool CheckAllDecisions()
    {
        bool allCorrect = true;

        foreach (var pair in correctDecisions)
        {
            string product = pair.Key;
            bool correctChoice = pair.Value;

            // Если игрок еще не принял решение по продукту, или
            // Если его решение не совпадает с правильным
            if (productDecisions[product] == null || productDecisions[product] != correctChoice)
            {
                allCorrect = false;
                break; // Достаточно найти одну ошибку
            }
        }

        return allCorrect;
    }
}