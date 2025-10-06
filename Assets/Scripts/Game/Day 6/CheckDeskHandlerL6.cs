using UnityEngine;

public class CheckDeskHandlerL6 : MonoBehaviour
{
    // ... (��������� ���� ��� ���������)
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    // !!! �����: ���������� ����� �������� ��� (1 ��� Day1, 2 ��� Day2 � �.�.)
    public int currentLevel = 6;

    private bool isInRange = false;

    void Start()
    {
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI == null || ProductManagerL6.Instance == null)
            {
                Debug.LogError("������: �� ����������� UI-������ �������� ��� ProductManager.");
                return;
            }

            if (checkPanelUI.activeSelf)
            {
                // ��������
                checkPanelUI.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            else
            {
                // �������� � ��������
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // �������� ��������
                bool allCorrect = ProductManagerL6.Instance.CheckAllDecisions();

                checkPanelUI.SetActive(true);

                if (successImage != null && failureImage != null)
                {
                    successImage.SetActive(allCorrect);
                    failureImage.SetActive(!allCorrect);
                }

                // =======================================================
                // !!! ����� ������: ���������� ��������� ��� ������ !!!
                // =======================================================
                if (allCorrect)
                {
                    // �������� ����������� ����� ��� ���������� ���������. 
                    // ��� ��������� ��������� ���� (currentLevel + 1)
                    ProgressManager.CompleteDay(currentLevel);
                }
                // =======================================================
            }
        }
    }

    // ... (������ OnTriggerEnter2D � OnTriggerExit2D ��� ���������)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (checkPromptUI != null) checkPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (checkPromptUI != null) checkPromptUI.SetActive(false);
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
        }
    }
}