using UnityEngine;
using UnityEngine.UI;

public class ICPMSHandlerL6 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject panelUI;
    public Text resultText;
    public Button detectButton;

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

        if (InventoryManagerL6.Instance != null &&
            InventoryManagerL6.Instance.AllAnalyzed())
        {
            ProductManagerL6.Instance.isAnalysisComplete = true;
        }
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL6.Instance == null) return;

        string result = InventoryManagerL6.Instance.Analyze("XRF");
        if (result == "None")
            resultText.text = "Please drag a product first!";
        else if (result == "Lead")
            resultText.text = "Lead detected!";
        else
            resultText.text = "No hazardous compound found";
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

