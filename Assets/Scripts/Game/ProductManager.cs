using UnityEngine;
using System.Collections.Generic;

public class ProductManager : MonoBehaviour
{
    // ����������� ��������� ��� ������� �� ������ ��������
    public static ProductManager Instance;

    // ������� ��� ������������ ������ ������
    // ����: ��� ��������, ��������: true (Approved) ��� false (Rejected)
    private Dictionary<string, bool?> productDecisions = new Dictionary<string, bool?>()
    {
        {"Shampoo", null},
        {"Sunscreen", null},
        {"FaceCream", null},
        {"Lipstick", null}
    };

    // ������� ����
    private readonly Dictionary<string, bool> correctDecisions = new Dictionary<string, bool>()
    {
        {"Shampoo", true},     // True = Approve
        {"Sunscreen", false},  // False = Reject
        {"FaceCream", true},
        {"Lipstick", false}
    };

    // UI-������ (���������� �� ����������)
    public GameObject mainInteractionPanel; // Interaction Panel UI
    public GameObject shampooPanel;
    public GameObject sunscreenPanel;
    public GameObject faceCreamPanel;
    public GameObject lipstickPanel;

    private GameObject currentOpenPanel; // ����� �����, ����� ������ �������

    void Awake()
    {
        // ������� Singleton ��� ������� �������
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ��� ������� ���������� ��� ������� ������ �� ������� ������
    public void OpenProductPanel(string productName)
    {
        // �������� �������� ������ ��������������
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

    // ��� ������� ���������� �������� Approve/Reject �� ������� ���������
    // (������� ������ ������� MakeDecision)

    // ����� �������: ��� ������ "Approve"
    public void ApproveProduct(string productName)
    {
        // �������� �������� ������, ��������� true (��������)
        ProcessDecision(productName, true);
    }

    // ����� �������: ��� ������ "Reject"
    public void RejectProduct(string productName)
    {
        // �������� �������� ������, ��������� false (���������)
        ProcessDecision(productName, false);
    }

    // �������� (private) �������, ���������� ��� ������ �������� �������
    private void ProcessDecision(string productName, bool isApproved)
    {
        // 1. ���������� ������� ������
        if (productDecisions.ContainsKey(productName))
        {
            productDecisions[productName] = isApproved;
        }
        else
        {
            Debug.LogError("ProductManager: ������� " + productName + " �� ������.");
            return;
        }

        // 2. ��������� ������� ������
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }

        // 3. ����� ���������� ������� ������ ��������������
        mainInteractionPanel.SetActive(true);
    }

    // �������� ���� ������� ����� �������
    public bool CheckAllDecisions()
    {
        bool allCorrect = true;

        foreach (var pair in correctDecisions)
        {
            string product = pair.Key;
            bool correctChoice = pair.Value;

            // ���� ����� ��� �� ������ ������� �� ��������, ���
            // ���� ��� ������� �� ��������� � ����������
            if (productDecisions[product] == null || productDecisions[product] != correctChoice)
            {
                allCorrect = false;
                break; // ���������� ����� ���� ������
            }
        }

        return allCorrect;
    }
}