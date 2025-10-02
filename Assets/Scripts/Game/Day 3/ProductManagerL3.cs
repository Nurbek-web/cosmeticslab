using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL3 : MonoBehaviour
{
    public static ProductManagerL3 Instance;

    // ����� ����������
    [HideInInspector] public GameObject currentOpenPanel;

    // --- ���������� UI ��� ������ 3 (���������� � ����������) ---
    public GameObject mainInteractionPanelL3; // ������� ������ 4 ������
    public GameObject productAPanel;         // ������ Eyeshadow
    public GameObject productBPanel;         // ������ Whitening Cream
    public GameObject productCPanel;         // ������ Face Cream
    public GameObject productDPanel;         // ������ Face Powder

    // --- ��������� � ������� ������ 3 ---
    [HideInInspector] public bool isAnalysisComplete = false; // ���� ��� ������������� InteractionHandlerL3

    private Dictionary<string, bool?> productDecisionsL3 = new Dictionary<string, bool?>()
    {
        {"Eyeshadow", null}, {"WCream", null}, {"FCream", null}, {"FPowder", null}
    };

    // ������� ����: Eyeshadow � Whitening Cream - Reject (Lead/Mercury); Face Cream/Powder - Approve (Safe)
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

    // --- ������ ������� ������ 3 (�������� UI � �������) ---

    // ���������� ��� ������� ������ �� ������� ������ L3
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

    // ���������� ������ �������� �������
    private void ProcessDecision(string productKey, bool isApproved)
    {
        productDecisionsL3[productKey] = isApproved;

        // ��������� ������� ������ � ��������� ������� ������
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

    // ������� ��� ������ Approve/Reject (��������� � ����������!)
    public void ApproveProduct(string productKey) { ProcessDecision(productKey, true); }
    public void RejectProduct(string productKey) { ProcessDecision(productKey, false); }


    // �������� ���� ������� (���������� CheckDeskHandlerL3)
    public bool CheckAllDecisions()
    {
        foreach (var pair in correctDecisionsL3)
        {
            // ���������, ��� ������� ������� � ��� ����������
            if (productDecisionsL3[pair.Key] == null || productDecisionsL3[pair.Key] != pair.Value)
            {
                return false;
            }
        }
        return true;
    }
}