using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // ��������� ������� ��� ����������
    [HideInInspector] public string selectedProductForAnalysis = "";

    // ���������� �������: true = �������� Hydroquinone (��� WC1, WC2)
    private readonly Dictionary<string, bool> analysisResults = new Dictionary<string, bool>()
    {
        {"WC1", true},
        {"WC2", true},
        {"WC3", false},
        {"WC4", false}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ���������� �������� ������ �������� � UI ����������
    public void SelectProduct(string productName)
    {
        selectedProductForAnalysis = productName;
        Debug.Log("Selected product for analysis: " + productName);
    }

    // ���������� ������� "Detect" � BiosensorHandler
    public bool AnalyzeProduct()
    {
        if (string.IsNullOrEmpty(selectedProductForAnalysis))
        {
            return false; // ������ �������������� � BiosensorHandler
        }

        // ���������� ��������� �������
        return analysisResults.ContainsKey(selectedProductForAnalysis) ? analysisResults[selectedProductForAnalysis] : false;
    }
    // InventoryManager.cs

    // ... ������������ ���������� ...

    // �����: ������������� �������� ������ � ������� ����������� �������
    private readonly Dictionary<string, string> productFullNames = new Dictionary<string, string>()
{
    {"WC1", "Whitening Cream N1"},
    {"WC2", "Whitening Cream N2"},
    {"WC3", "Whitening Cream N3"},
    {"WC4", "Whitening Cream N4"}
};

    // ... ������������ ������ ...

    // ����� ����� ��� ��������� ������� �����
    public string GetProductFullName(string key)
    {
        return productFullNames.ContainsKey(key) ? productFullNames[key] : key;
    }
}