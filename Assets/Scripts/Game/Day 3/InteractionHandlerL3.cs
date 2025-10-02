using UnityEngine;

public class InteractionHandlerL3 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject mainInteractionPanelL3;

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL3 != null) mainInteractionPanelL3.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Проверка, что анализ завершен
            if (ProductManagerL3.Instance == null || !ProductManagerL3.Instance.isAnalysisComplete)
            {
                return;
            }

            bool isPanelOpen = mainInteractionPanelL3.activeSelf;
            mainInteractionPanelL3.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            if (ProductManagerL3.Instance != null && ProductManagerL3.Instance.isAnalysisComplete)
            {
                if (ePromptUI != null) ePromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);

            if (mainInteractionPanelL3 != null && mainInteractionPanelL3.activeSelf)
            {
                mainInteractionPanelL3.SetActive(false);
            }
        }
    }
}