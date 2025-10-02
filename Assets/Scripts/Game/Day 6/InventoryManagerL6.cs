using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL6 : MonoBehaviour
{
    public static InventoryManagerL6 Instance;

    [HideInInspector] public string selectedProduct = "";

    private readonly Dictionary<string, string> analysisResults = new Dictionary<string, string>()
    {
        {"WCream_Biosensor", "Hydroquinone"},
        {"WCream_ELISA", "Methylparaben"},
        {"NPolish_GC", "Toluene"},
        {"Lipstick_XRF", "Lead"},
        {"HCream_Safe", "Safe"}
    };

    private Dictionary<string, bool> done = new Dictionary<string, bool>()
    {
        {"WCream_Biosensor", false},
        {"WCream_ELISA", false},
        {"NPolish_GC", false},
        {"Lipstick_XRF", false},
        {"HCream_Safe", false}
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

    public bool AllAnalyzed()
    {
        foreach (var p in done)
            if (!p.Value) return false;
        return true;
    }

    // ? Новый метод: проверка только Биосенсора
    public bool IsBiosensorAnalyzed()
    {
        return done.ContainsKey("WCream_Biosensor") && done["WCream_Biosensor"];
    }
}
