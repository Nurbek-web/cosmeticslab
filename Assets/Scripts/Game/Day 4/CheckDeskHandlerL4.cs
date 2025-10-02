using UnityEngine;

public class CheckDeskHandlerL4 : MonoBehaviour
{
    public GameObject checkPromptUI;     // UI � ���������� "������� E"
    public GameObject checkPanelUI;      // ������, ������������ ��������� ��������
    public GameObject successImage;      // �����������/����� ��� �������� ��������
    public GameObject failureImage;      // �����������/����� ��� ���������� ��������

    private bool isInRange = false;

    void Start()
    {
        // ������������� UI: �������� ��������� � ������
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
        // �������� ����������
        if (successImage != null) successImage.SetActive(false);
        if (failureImage != null) failureImage.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI == null || ProductManagerL4.Instance == null) return;

            if (checkPanelUI.activeSelf)
            {
                // �������� ������
                checkPanelUI.SetActive(false);
                // �������� ���������� ����� ���������
                if (successImage != null) successImage.SetActive(false);
                if (failureImage != null) failureImage.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            else
            {
                // �������� � �������� �������
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // ��������� �������� �������� �������
                bool allCorrect = ProductManagerL4.Instance.CheckAllDecisions();

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
        // ���� ����� ����� � �������
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (checkPromptUI != null) checkPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���� ����� ����� �� ��������
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (checkPromptUI != null) checkPromptUI.SetActive(false);

            // ��������� ������ ��������, ���� ��� ���� �������
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
        }
    }
}
