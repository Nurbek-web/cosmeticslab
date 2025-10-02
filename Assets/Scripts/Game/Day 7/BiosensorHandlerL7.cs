using UnityEngine;
using UnityEngine.UI;

public class BiosensorHandlerL7 : MonoBehaviour
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
    }

    public void OnDetectClicked()
    {
        if (InventoryManagerL7.Instance == null) return;

        string result = InventoryManagerL7.Instance.Analyze("Biosensor");

        if (result == "None")
            resultText.text = "Please drag a product first!";
        else if (result == "Hydroquinone")
            resultText.text = "Hydroquinone detected!.";
        else
            resultText.text = "No hazardous compounds found.";

        if (InventoryManagerL7.Instance.RequiredAnalyzed())
        {
            ProductManagerL7.Instance.isAnalysisComplete = true;
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
