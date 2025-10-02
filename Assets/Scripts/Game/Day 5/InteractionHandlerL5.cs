using UnityEngine;

public class InteractionHandlerL5 : MonoBehaviour
{
    public GameObject ePromptUI;              // UI � ���������� "������� E"
    public GameObject mainInteractionPanelL5; // ������ �������� ������� (Approve/Reject)

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL5 != null) mainInteractionPanelL5.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // ��������� ������� ���������
            if (ProductManagerL5.Instance == null)
            {
                Debug.LogError("ProductManagerL5 not found!");
                return;
            }

            // ���������, ��� ��� �������� ����������������
            if (!ProductManagerL5.Instance.IsAnalysisComplete())
            {
                Debug.Log("L5: Not all products analyzed yet. Cannot open decision panel.");
                return;
            }

            // ���� ������ �������� � ���������/��������� ������
            bool isPanelOpen = mainInteractionPanelL5.activeSelf;
            mainInteractionPanelL5.SetActive(!isPanelOpen);

            // ��������� "E" ������������ ������, ���� ������ �������
            ePromptUI.SetActive(isPanelOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� ����� �����
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // ��������� "E" ����������, ������ ���� ������ ��������
            if (ProductManagerL5.Instance != null && ProductManagerL5.Instance.IsAnalysisComplete())
            {
                if (ePromptUI != null) ePromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���������, ��� ����� �����
        if (other.CompareTag("Player"))
        {
            isInRange = false;

            if (ePromptUI != null) ePromptUI.SetActive(false);

            // ��������� ������, ���� ��� ���� �������
            if (mainInteractionPanelL5 != null && mainInteractionPanelL5.activeSelf)
            {
                mainInteractionPanelL5.SetActive(false);
            }
        }
    }
}
