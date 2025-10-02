using UnityEngine;
using System.Collections.Generic;

public class ProductManagerL2 : MonoBehaviour
{
    // Singleton ��� ������� �� ������ ��������
    public static ProductManagerL2 Instance;

    // ����� ����������
    [HideInInspector] public GameObject currentOpenPanel; // ���� ������ �� �������������

    // --- ���������� UI ��� ������ 2 (���������� � ����������) ---
    public GameObject mainInteractionPanelL2; // ������� ������ 4 ������ (5 png, ��� 4 - ��������, 1 - ���)
    public GameObject wc1Panel;              // ������ Whitening Cream #1
    public GameObject wc2Panel;              // ������ Whitening Cream #2
    public GameObject wc3Panel;              // ������ Whitening Cream #3
    public GameObject wc4Panel;              // ������ Whitening Cream #4

    // --- ��������� � ������� ������ 2 ---
    [HideInInspector] public bool isAnalysisComplete = false; // ������������� InteractionHandlerL2
    [HideInInspector] public int analyzedProductCount = 0;    // ������� ��� ���������� (��� �����������, �� ��� ������������)

    private Dictionary<string, bool?> productDecisionsL2 = new Dictionary<string, bool?>()
    {
        {"WC1", null}, {"WC2", null}, {"WC3", null}, {"WC4", null}
    };

    // ������� ����: WC1 � WC2 - Reject (Hydroquinone); WC3 � WC4 - Approve
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

    // --- ������ ������� ������ 2 ---

    // 1. �������� ������ ��������
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

    // 2. ���������� ������ �������� ������� (���������� ��������)
    private void ProcessDecision(string productName, bool isApproved)
    {
        productDecisionsL2[productName] = isApproved;

        // ��������� ������� ������ � ��������� ������� ������
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

    // 3. ������� ��� ������ Approve/Reject (��������� � ����������!)
    public void ApproveProduct(string productName) { ProcessDecision(productName, true); }
    public void RejectProduct(string productName) { ProcessDecision(productName, false); }


    // 4. �������� ���� ������� (���������� CheckDeskHandlerL2)
    public bool CheckAllDecisions()
    {
        foreach (var pair in correctDecisionsL2)
        {
            // ���������, ��� ������� ������� � ��� ����������
            if (productDecisionsL2[pair.Key] == null || productDecisionsL2[pair.Key] != pair.Value)
            {
                return false;
            }
        }
        return true;
    }
}