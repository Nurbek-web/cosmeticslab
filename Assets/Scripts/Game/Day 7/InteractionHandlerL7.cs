using UnityEngine;

public class InteractionHandlerL7 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject mainPanel;

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI) ePromptUI.SetActive(false);
        if (mainPanel) mainPanel.SetActive(false);
    }

    void Update()
    {
        if (ProductManagerL7.Instance != null && ProductManagerL7.Instance.isAnalysisComplete)
        {
            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                bool open = mainPanel.activeSelf;
                mainPanel.SetActive(!open);
                ePromptUI.SetActive(open);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            if (ProductManagerL7.Instance.isAnalysisComplete && ePromptUI)
                ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI) ePromptUI.SetActive(false);
            if (mainPanel) mainPanel.SetActive(false);
        }
    }
}
