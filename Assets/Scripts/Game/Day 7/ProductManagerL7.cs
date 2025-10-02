using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL7 : MonoBehaviour
{
    public static ProductManagerL7 Instance;

    [HideInInspector] public bool isAnalysisComplete = false;
    [HideInInspector] public GameObject currentOpenPanel;

    public GameObject mainInteractionPanelL7;

    // Панели для 4 продуктов
    public GameObject foundationPanel;
    public GameObject lighteningPanel;
    public GameObject facePanel;
    public GameObject shampooPanel;

    private Dictionary<string, bool?> decisions = new Dictionary<string, bool?>()
    {
        {"Foundation", null},
        {"Lightening", null},
        {"Face", null},
        {"Shampoo", null}
    };

    private readonly Dictionary<string, bool> correct = new Dictionary<string, bool>()
    {
        {"Foundation", false}, // Reject
        {"Lightening", false}, // Reject
        {"Face", true},        // Approve
        {"Shampoo", true}      // Approve
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OpenProductPanel(string key)
    {
        mainInteractionPanelL7.SetActive(false);
        switch (key)
        {
            case "Foundation": foundationPanel.SetActive(true); currentOpenPanel = foundationPanel; break;
            case "Lightening": lighteningPanel.SetActive(true); currentOpenPanel = lighteningPanel; break;
            case "Face": facePanel.SetActive(true); currentOpenPanel = facePanel; break;
            case "Shampoo": shampooPanel.SetActive(true); currentOpenPanel = shampooPanel; break;
        }
    }

    private void ProcessDecision(string key, bool approve)
    {
        decisions[key] = approve;

        if (currentOpenPanel) currentOpenPanel.SetActive(false);
        currentOpenPanel = null;
        mainInteractionPanelL7.SetActive(true);
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
