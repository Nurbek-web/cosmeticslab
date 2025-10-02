using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL4 : MonoBehaviour
{
    public static InventoryManagerL4 Instance;

    // Выбранный продукт для анализа (хранит короткий ключ)
    [HideInInspector] public string selectedProductForAnalysis = "";

    // Новое: Сопоставление коротких ключей с полными английскими именами
    private readonly Dictionary<string, string> productFullNames = new Dictionary<string, string>()
    {
        {"WCream", "Whitening Cream"},
        {"Lipstick", "Lipstick"},
        {"FCream", "Face Cream"},
        {"HCream", "Hand Cream"}
    };

    // Новое: Карта анализа. Ключ: короткое имя продукта. Значение: (Опасный компонент, Требуемый Анализатор)
    private readonly Dictionary<string, (string component, string analyzer)> analysisMap =
        new Dictionary<string, (string component, string analyzer)>()
    {
        // Продукт 1: Hydroquinone -> Biosensor
        {"WCream", ("Hydroquinone", "Biosensor")}, 
        
        // Продукт 2: Lead -> ICP-MS/XRF
        {"Lipstick", ("Lead Acetate (Lead)", "ICPMS")}, 
        
        // Продукт 3: Parabens -> ELISA
        {"FCream", ("Parabens", "ELISA")},
        
        // Продукт 4: Safe -> NO (не требует специального анализа)
        {"HCream", ("Safe", "None")}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectProduct(string productKey)
    {
        selectedProductForAnalysis = productKey;
    }

    // Общий метод для анализа: возвращает (компонент, анализатор)
    public (string component, string analyzer) GetAnalysisData(string productKey)
    {
        if (analysisMap.ContainsKey(productKey))
        {
            return analysisMap[productKey];
        }
        return ("Unknown", "None");
    }

    public string GetProductFullName(string key)
    {
        return productFullNames.ContainsKey(key) ? productFullNames[key] : key;
    }
}
