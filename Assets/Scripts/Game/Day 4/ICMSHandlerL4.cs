using UnityEngine;
using UnityEngine.UI;

public class ICMSHandlerL4 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject panelUI;
    public Text analysisResultText;
    public Button detectButton;

    private bool isInRange = false;

    void Start()
    {
        // ������������� UI: �������� ��������� � ������
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (panelUI != null) panelUI.SetActive(false);
        // ��������� ���������� ������� �� ������ "Detect"
        if (detectButton != null)
        {
            detectButton.onClick.AddListener(OnDetectClicked);
        }
    }

    void Update()
    {
        // ������ ��������/�������� ������ �� 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isPanelOpen = panelUI.activeSelf;
            panelUI.SetActive(!isPanelOpen);
            // ���� ��������� ������, ���������� ��������� "E"
            ePromptUI.SetActive(isPanelOpen);

            // ����� ��������� ��� ��������
            if (isPanelOpen)
            {
                if (InventoryManagerL4.Instance != null) InventoryManagerL4.Instance.selectedProductForAnalysis = "";
                analysisResultText.text = "";
            }
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL4.Instance == null || ProductManagerL4.Instance == null) return;

        string productKey = InventoryManagerL4.Instance.selectedProductForAnalysis;

        if (string.IsNullOrEmpty(productKey))
        {
            analysisResultText.text = "Please drag a product to the slot first!";
            return;
        }

        var (component, analyzer) = InventoryManagerL4.Instance.GetAnalysisData(productKey);
        string fullName = InventoryManagerL4.Instance.GetProductFullName(productKey);

        // ��������: ����������� �� ���� ������� ��� ������?
        // ���������, ��� ���������� ���� "ICPMS", ���� "None" (���������� �������, �������� �� ����� ������������� ����������)
        if (analyzer != "ICPMS" && analyzer != "None")
        {
            // ���������, ��� ��������� ������, ����� ���������� �����
            analysisResultText.text = fullName + ": Incorrect machine! This product requires the " + analyzer + " analyzer";
            return;
        }

        // --- ������ ������� ---
        if (component.Contains("Lead"))
        {
            analysisResultText.text = fullName + ": Lead Acetate (Heavy Metal) detected!";
            ProductManagerL4.Instance.MarkProductAsAnalyzed(productKey);
        }
        else if (component == "Safe" || analyzer == "None")
        {
            // 'Hand Cream' ����� ��������� "Safe" � ���������� "None"
            analysisResultText.text = fullName + ": Safe. No heavy metals detected";
            ProductManagerL4.Instance.MarkProductAsAnalyzed(productKey);
        }
        else
        {
            // ���� ��� ������ �������, ������� �� ������ ����� ���� (��������, Paraben/Hydroquinone)
            analysisResultText.text = fullName + ": No heavy metal hazard found. This test is inconclusive. Use " + analyzer ;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ����� ����� � �������
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (ePromptUI != null) ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���� ����� ����� �� ��������
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);
            // ��������� ������, ���� ��� ���� �������
            if (panelUI != null && panelUI.activeSelf)
            {
                panelUI.SetActive(false);
            }
        }
    }
}
