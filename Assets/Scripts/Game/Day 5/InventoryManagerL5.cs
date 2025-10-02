using UnityEngine;
using System.Collections.Generic;

public class InventoryManagerL5 : MonoBehaviour
{
    public static InventoryManagerL5 Instance;

    // ��������� ������� ��� ������� (������ �������� ����)
    [HideInInspector] public string selectedProductForAnalysis = "";

    // ������������� �������� ������ � ������� ����������� �������
    private readonly Dictionary<string, string> productFullNames = new Dictionary<string, string>()
    {
        {"NP", "Nail Polish"},
        {"FCream", "Face Cream"},
        {"Mascara", "Mascara"},
        {"HCream", "Hand Cream"}
    };

    // ����� �������. ����: �������� ��� ��������. ��������: (������� ���������, ��������� ����������)
    // NP -> Toluene -> GC
    // FCream -> Hydroquinone -> Biosensor
    // Mascara -> Safe -> None
    // HCream -> Safe -> None
    private readonly Dictionary<string, (string component, string analyzer)> analysisMap =
        new Dictionary<string, (string component, string analyzer)>()
    {
        // Nail Polish (Toluene) -> GC
        {"NP", ("Toluene", "GC")},          
        // Face Cream (Hydroquinone) -> Biosensor
        {"FCream", ("Hydroquinone", "Biosensor")}, 
        // Mascara (Safe) -> None
        {"Mascara", ("Safe", "None")},
        // Hand Cream (Safe) -> None
        {"HCream", ("Safe", "None")}
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
