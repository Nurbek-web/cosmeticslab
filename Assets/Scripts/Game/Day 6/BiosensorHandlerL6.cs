using UnityEngine;
using UnityEngine.UI;

public class BiosensorHandlerL6 : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject ePromptUI;   // ��������� "E"
    public GameObject panelUI;     // ������ �����������
    public Text resultText;        // ����� ����������
    public Button detectButton;    // ������ "Detect"

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI) ePromptUI.SetActive(false);
        if (panelUI) panelUI.SetActive(false);

        if (detectButton != null)
            detectButton.onClick.AddListener(OnDetectClicked);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool open = panelUI.activeSelf;
            panelUI.SetActive(!open);
            ePromptUI.SetActive(open);
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL6.Instance == null) return;

        // ? ����������� ����� ���������
        string result = InventoryManagerL6.Instance.Analyze("Biosensor");

        if (result == "None")
        {
            resultText.text = "Please drag a product first!";
            return;
        }

        // ? ���������� ���������
        if (result == "Hydroquinone")
            resultText.text = "Hydroquinone detected!";
        else
            resultText.text = "No hazardous compound found.";

        // ? ����� ������� ���������� � ������������ InteractionTrigger
        if (InventoryManagerL6.Instance.IsBiosensorAnalyzed())
        {
            ProductManagerL6.Instance.isAnalysisComplete = true;
            Debug.Log("Biosensor analysis complete � interaction unlocked!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (ePromptUI) ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI) ePromptUI.SetActive(false);
            if (panelUI) panelUI.SetActive(false);
        }
    }
}
