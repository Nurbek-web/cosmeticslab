using UnityEngine;

public class InteractionHandlerL2 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject mainInteractionPanelL2;

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL2 != null) mainInteractionPanelL2.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // ����������: ���� ������ �� ��������, �����
            if (ProductManagerL2.Instance == null || !ProductManagerL2.Instance.isAnalysisComplete)
            {
                return;
            }

            // ������������ UI
            bool isPanelOpen = mainInteractionPanelL2.activeSelf;
            mainInteractionPanelL2.SetActive(!isPanelOpen);
            ePromptUI.SetActive(isPanelOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // ���������� ������ 'E' ������ ���� ������ �������� (isAnalysisComplete = true)
            if (ProductManagerL2.Instance != null && ProductManagerL2.Instance.isAnalysisComplete)
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

            // ��������� ������� ������ L2, ���� �������� ����
            if (mainInteractionPanelL2 != null && mainInteractionPanelL2.activeSelf)
            {
                mainInteractionPanelL2.SetActive(false);
            }
        }
    }
}