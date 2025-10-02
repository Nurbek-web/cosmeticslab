using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL3 : MonoBehaviour
{
    public static InventoryManagerL3 Instance;

    // Выбранный продукт для анализатора ICP-MS (хранит короткий ключ)
    [HideInInspector] public string selectedProductForAnalysis = "";

    // Результаты анализа: Ключ = Короткое имя, Значение = Обнаруженный опасный компонент (или "Safe")
    private readonly Dictionary<string, string> analysisResults = new Dictionary<string, string>()
    {
        {"Eyeshadow", "Lead"},
        {"WCream", "Mercury"},
        {"FCream", "Safe"},
        {"FPowder", "Safe"}
    };

    // Новое: Сопоставление коротких ключей с полными английскими именами
    private readonly Dictionary<string, string> productFullNames = new Dictionary<string, string>()
    {
        {"Eyeshadow", "Eyeshadow"},
        {"WCream", "Whitening Cream"},
        {"FCream", "Face Cream"},
        {"FPowder", "Face Powder"}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Вызывается кнопками выбора продукта (или DropSlot)
    public void SelectProduct(string productKey)
    {
        selectedProductForAnalysis = productKey;
    }

    // Вызывается кнопкой "Detect" (ICPMSHandler)
    public string AnalyzeProduct()
    {
        if (string.IsNullOrEmpty(selectedProductForAnalysis))
        {
            return "None";
        }

        // Возвращает опасный компонент или "Safe"
        return analysisResults.ContainsKey(selectedProductForAnalysis)
            ? analysisResults[selectedProductForAnalysis]
            : "Unknown";
    }

    // Метод для получения полного имени
    public string GetProductFullName(string key)
    {
        return productFullNames.ContainsKey(key) ? productFullNames[key] : key;
    }
}