using UnityEngine;
using UnityEngine.UI;

public class GCHandler : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject panelUI;
    public Text analysisResultText;
    public Button detectButton;
    public Transform contentArea; // �������� ��� DropSlot

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (panelUI != null) panelUI.SetActive(false);

        // ��������� ��������� ��� ������ Detect
        if (detectButton != null)
        {
            detectButton.onClick.RemoveAllListeners();
            detectButton.onClick.AddListener(OnDetectClicked);
        }

        // ��������, ��� ������-����� ���� ��� ������
        if (analysisResultText != null)
        {
            analysisResultText.text = "Drag a product here to begin GC analysis.";
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = panelUI.activeSelf;
            panelUI.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen); // ���������� E Prompt, ����� ��������� ������

            // ���� ������ �����������, ���������� ��������� ������� � �����
            if (isPanelOpen)
            {
                if (InventoryManagerL5.Instance != null)
                {
                    InventoryManagerL5.Instance.selectedProductForAnalysis = "";
                }
            }
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL5.Instance == null || ProductManagerL5.Instance == null) return;

        string productKey = InventoryManagerL5.Instance.selectedProductForAnalysis;

        if (string.IsNullOrEmpty(productKey))
        {
            analysisResultText.text = "Please drag a product to the slot first!";
            return;
        }

        var (component, analyzer) = InventoryManagerL5.Instance.GetAnalysisData(productKey);
        string fullName = InventoryManagerL5.Instance.GetProductFullName(productKey);

        // ��������: ����������� �� ���� ������� ��� ������?
        // ������� NP (Nail Polish) ������ ���� ��������������� GC. ���������� �������� (Mascara, Hand Cream) ����� analyzer == "None" 
        // � component == "Safe", � ����� ����� ���� ���������.
        if (analyzer != "GC" && component != "Safe")
        {
            analysisResultText.text = fullName + " Incorrect machine! This product requires the " + analyzer + " analyzer.";
            return;
        }

        // --- ������ ������� ---
        if (component == "Toluene")
        {
            analysisResultText.text = fullName + ": HAZARD DETECTED! Toluene (Toxic Solvent) found!";
            ProductManagerL5.Instance.MarkProductAsAnalyzed(productKey);
        }
        else if (component == "Safe")
        {
            analysisResultText.text = fullName + ": Safe. No volatile organic compounds detected.";
            ProductManagerL5.Instance.MarkProductAsAnalyzed(productKey);
        }
        else
        {
            analysisResultText.text = fullName + ": Test inconclusive for volatile solvents. This machine might not be the correct tool or the component is missing.";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // ��������, ��� ������ �� �������, ����� �� ����������� ���������
            if (ePromptUI != null && !panelUI.activeSelf)
            {
                ePromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);
            if (panelUI != null) panelUI.SetActive(false); // ��������� ������ ��� ������

            // ���������� ��������� ������� � ����� ��� ��������
            if (InventoryManagerL5.Instance != null)
            {
                InventoryManagerL5.Instance.selectedProductForAnalysis = "";
            }
        }
    }
}
