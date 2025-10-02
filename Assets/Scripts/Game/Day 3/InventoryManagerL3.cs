using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL3 : MonoBehaviour
{
    public static InventoryManagerL3 Instance;

    // ��������� ������� ��� ����������� ICP-MS (������ �������� ����)
    [HideInInspector] public string selectedProductForAnalysis = "";

    // ���������� �������: ���� = �������� ���, �������� = ������������ ������� ��������� (��� "Safe")
    private readonly Dictionary<string, string> analysisResults = new Dictionary<string, string>()
    {
        {"Eyeshadow", "Lead"},
        {"WCream", "Mercury"},
        {"FCream", "Safe"},
        {"FPowder", "Safe"}
    };

    // �����: ������������� �������� ������ � ������� ����������� �������
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

    // ���������� �������� ������ �������� (��� DropSlot)
    public void SelectProduct(string productKey)
    {
        selectedProductForAnalysis = productKey;
    }

    // ���������� ������� "Detect" (ICPMSHandler)
    public string AnalyzeProduct()
    {
        if (string.IsNullOrEmpty(selectedProductForAnalysis))
        {
            return "None";
        }

        // ���������� ������� ��������� ��� "Safe"
        return analysisResults.ContainsKey(selectedProductForAnalysis)
            ? analysisResults[selectedProductForAnalysis]
            : "Unknown";
    }

    // ����� ��� ��������� ������� �����
    public string GetProductFullName(string key)
    {
        return productFullNames.ContainsKey(key) ? productFullNames[key] : key;
    }
}