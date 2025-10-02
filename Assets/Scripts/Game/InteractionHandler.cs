using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    // ���������� ���� �� �������� (Hierarchy)
    public GameObject ePromptUI;            // ����������� ������ 'E'
    public GameObject mainInteractionPanel; // ������� ������ (Interaction Panel UI)

    private bool isInRange = false;         // ����, ��� �������� � �������

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanel != null) mainInteractionPanel.SetActive(false);
    }

    void Update()
    {
        // ���������, ��������� �� �������� � ������� � ���� �� ������ ������� 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // ����������� ��������� ������� ������
            bool isPanelOpen = mainInteractionPanel.activeSelf;
            mainInteractionPanel.SetActive(!isPanelOpen);

            // �������� ������ 'E', ���� ������� ������
            ePromptUI.SetActive(isPanelOpen); // ���� ������ �����������, ������ �� ���������, � ������ E ����������

            // ���� ������ �����������, ���������, ��� ��� �������� ������ ��������� ���� �������.
            if (isPanelOpen)
            {
                // (�����������) ������� ��� �������� ������, ���� ��� ���� �������, 
                // ����� �������� ������ ��� ��������� ��������. 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (ePromptUI != null) ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);

            // ��������� ������� ������, ���� �������� ����
            if (mainInteractionPanel.activeSelf)
            {
                mainInteractionPanel.SetActive(false);
            }
        }
    }
}