using UnityEngine;

public class CheckDeskHandlerL3 : MonoBehaviour
{
    // UI ��������, ������� ����� ���������� � ����������
    // ���������� ���� ������ � �������-�������� (��������, CheckDeskTriggerL3)
    public GameObject checkPromptUI;     // UI � ���������� "������� E ��� ��������"
    public GameObject checkPanelUI;      // ������� ������ ��� ����������� ���������� �������� (��������� ����)
    public GameObject successImage;      // �����������/�����, ������������ �������� �����������
    public GameObject failureImage;      // �����������/�����, ������������ ������

    private bool isInRange = false;

    void Start()
    {
        // ���������� �������� ��� UI
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
    }

    void Update()
    {
        // ��������� ������� ������� 'E' ��� ��������/�������� ������ ��������
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI == null || ProductManagerL3.Instance == null)
            {
                Debug.LogError("Error: Check UI Panel or ProductManagerL3 is not set correctly in CheckDeskHandlerL3.");
                return;
            }

            if (checkPanelUI.activeSelf)
            {
                // �������� ������
                checkPanelUI.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            else
            {
                // �������� � �������� �������
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // �������� �������� ���� ������� �� ProductManagerL3
                bool allCorrect = ProductManagerL3.Instance.CheckAllDecisions();

                checkPanelUI.SetActive(true);

                // ���������� ��������������� ���������
                if (successImage != null && failureImage != null)
                {
                    successImage.SetActive(allCorrect);
                    failureImage.SetActive(!allCorrect);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // ���������� ���������, ����� ����� ������ � �������
            if (checkPromptUI != null) checkPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            // �������� ���������
            if (checkPromptUI != null) checkPromptUI.SetActive(false);

            // ���� ������ �������, ��������� � ��� ������
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
        }
    }
}
