using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL6 : MonoBehaviour
{
    public static ProductManagerL6 Instance;

    [HideInInspector] public bool isAnalysisComplete = false;
    [HideInInspector] public GameObject currentOpenPanel;

    public GameObject mainInteractionPanelL6;

    // Панели для 4 продуктов
    public GameObject wCreamPanel;
    public GameObject nailPolishPanel;
    public GameObject lipstickPanel;
    public GameObject hCreamPanel;

    private Dictionary<string, bool?> decisions = new Dictionary<string, bool?>()
    {
        {"WCream", null}, {"NPolish", null}, {"Lipstick", null}, {"HCream", null}
    };

    private readonly Dictionary<string, bool> correct = new Dictionary<string, bool>()
    {
        {"WCream", false},   // Reject (Hydroquinone + Methylparaben)
        {"NPolish", false},  // Reject (Toluene)
        {"Lipstick", false}, // Reject (Lead acetate)
        {"HCream", true}     // Approve (Safe)
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OpenProductPanel(string key)
    {
        mainInteractionPanelL6.SetActive(false);
        switch (key)
        {
            case "WCream": wCreamPanel.SetActive(true); currentOpenPanel = wCreamPanel; break;
            case "NPolish": nailPolishPanel.SetActive(true); currentOpenPanel = nailPolishPanel; break;
            case "Lipstick": lipstickPanel.SetActive(true); currentOpenPanel = lipstickPanel; break;
            case "HCream": hCreamPanel.SetActive(true); currentOpenPanel = hCreamPanel; break;
        }
    }

    private void ProcessDecision(string key, bool approve)
    {
        decisions[key] = approve;

        if (currentOpenPanel) currentOpenPanel.SetActive(false);
        currentOpenPanel = null;
        mainInteractionPanelL6.SetActive(true);
    }

    public void ApproveProduct(string key) => ProcessDecision(key, true);
    public void RejectProduct(string key) => ProcessDecision(key, false);

    public bool CheckAllDecisions()
    {
        foreach (var p in correct)
        {
            if (decisions[p.Key] == null || decisions[p.Key] != p.Value)
                return false;
        }
        return true;
    }
}
