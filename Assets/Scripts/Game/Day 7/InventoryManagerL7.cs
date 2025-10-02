using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL7 : MonoBehaviour
{
    public static InventoryManagerL7 Instance;

    [HideInInspector] public string selectedProduct = "";

    private readonly Dictionary<string, string> analysisResults = new Dictionary<string, string>()
    {
        {"Foundation_ELISA", "Propylparaben"},
        {"Lightening_Biosensor", "Hydroquinone"},
        {"Face_Safe", "Safe"},
        {"Shampoo_Safe", "Safe"}
    };

    private Dictionary<string, bool> done = new Dictionary<string, bool>()
    {
        {"Foundation_ELISA", false},
        {"Lightening_Biosensor", false},
        {"Face_Safe", false},
        {"Shampoo_Safe", false}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectProduct(string key) => selectedProduct = key;

    public string Analyze(string analyzer)
    {
        if (string.IsNullOrEmpty(selectedProduct)) return "None";

        string key = selectedProduct + "_" + analyzer;
        if (analysisResults.ContainsKey(key))
        {
            done[key] = true;
            return analysisResults[key];
        }
        return "Safe";
    }

    public bool IsBiosensorAnalyzed()
    {
        return done["Lightening_Biosensor"];
    }

    public bool IsELISAAnalyzed()
    {
        return done["Foundation_ELISA"];
    }

    public bool RequiredAnalyzed()
    {
        // Оба ключевых анализа
        return IsBiosensorAnalyzed() && IsELISAAnalyzed();
    }
}
