using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL4 : MonoBehaviour
{
    public static InventoryManagerL4 Instance;

    // ��������� ������� ��� ������� (������ �������� ����)
    [HideInInspector] public string selectedProductForAnalysis = "";

    // �����: ������������� �������� ������ � ������� ����������� �������
    private readonly Dictionary<string, string> productFullNames = new Dictionary<string, string>()
    {
        {"WCream", "Whitening Cream"},
        {"Lipstick", "Lipstick"},
        {"FCream", "Face Cream"},
        {"HCream", "Hand Cream"}
    };

    // �����: ����� �������. ����: �������� ��� ��������. ��������: (������� ���������, ��������� ����������)
    private readonly Dictionary<string, (string component, string analyzer)> analysisMap =
        new Dictionary<string, (string component, string analyzer)>()
    {
        // ������� 1: Hydroquinone -> Biosensor
        {"WCream", ("Hydroquinone", "Biosensor")}, 
        
        // ������� 2: Lead -> ICP-MS/XRF
        {"Lipstick", ("Lead Acetate (Lead)", "ICPMS")}, 
        
        // ������� 3: Parabens -> ELISA
        {"FCream", ("Parabens", "ELISA")},
        
        // ������� 4: Safe -> NO (�� ������� ������������ �������)
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

    // ����� ����� ��� �������: ���������� (���������, ����������)
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
