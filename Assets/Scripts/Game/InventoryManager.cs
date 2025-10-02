using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Выбранный продукт для биосенсора
    [HideInInspector] public string selectedProductForAnalysis = "";

    // Результаты анализа: true = содержит Hydroquinone (для WC1, WC2)
    private readonly Dictionary<string, bool> analysisResults = new Dictionary<string, bool>()
    {
        {"WC1", true},
        {"WC2", true},
        {"WC3", false},
        {"WC4", false}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Вызывается кнопками выбора продукта в UI биосенсора
    public void SelectProduct(string productName)
    {
        selectedProductForAnalysis = productName;
        Debug.Log("Selected product for analysis: " + productName);
    }

    // Вызывается кнопкой "Detect" в BiosensorHandler
    public bool AnalyzeProduct()
    {
        if (string.IsNullOrEmpty(selectedProductForAnalysis))
        {
            return false; // Должно обрабатываться в BiosensorHandler
        }

        // Возвращаем результат анализа
        return analysisResults.ContainsKey(selectedProductForAnalysis) ? analysisResults[selectedProductForAnalysis] : false;
    }
    // InventoryManager.cs

    // ... существующие переменные ...

    // Новое: Сопоставление коротких ключей с полными английскими именами
    private readonly Dictionary<string, string> productFullNames = new Dictionary<string, string>()
{
    {"WC1", "Whitening Cream N1"},
    {"WC2", "Whitening Cream N2"},
    {"WC3", "Whitening Cream N3"},
    {"WC4", "Whitening Cream N4"}
};

    // ... существующие методы ...

    // Новый метод для получения полного имени
    public string GetProductFullName(string key)
    {
        return productFullNames.ContainsKey(key) ? productFullNames[key] : key;
    }
}