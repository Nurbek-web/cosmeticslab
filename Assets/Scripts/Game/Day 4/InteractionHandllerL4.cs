using UnityEngine;

public class InteractionHandlerL4 : MonoBehaviour
{
    public GameObject ePromptUI;            // UI � ���������� "������� E"
    public GameObject mainInteractionPanelL4; // ������ �������� ������� (Approve/Reject)

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL4 != null) mainInteractionPanelL4.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // ����������, ��� �������� ����������
            if (ProductManagerL4.Instance == null)
            {
                Debug.LogError("ProductManagerL4 not found!");
                return;
            }

            // ��������, ��� ��� 4 �������� ����������������
            if (!ProductManagerL4.Instance.IsAnalysisComplete())
            {
                // ���� ������ �� ��������, ������ �� ���������.
                Debug.Log("L4: Not all products analyzed yet. Cannot proceed to decision desk.");
                return;
            }

            // ���� ������ ��������, ���������/��������� ������
            bool isPanelOpen = mainInteractionPanelL4.activeSelf;
            mainInteractionPanelL4.SetActive(!isPanelOpen);

            // ��������� E ������ ���� �����, ������ ����� ������ �������
            ePromptUI.SetActive(isPanelOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� ����� �����
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // ��������� "E" ���������� ������, ���� ������ ��������
            if (ProductManagerL4.Instance != null && ProductManagerL4.Instance.IsAnalysisComplete())
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
            if (mainInteractionPanelL4 != null && mainInteractionPanelL4.activeSelf)
            {
                mainInteractionPanelL4.SetActive(false);
            }
        }
    }
}
