using UnityEngine;

public class CheckDeskHandler : MonoBehaviour
{
    // ... (������ ��������� ����)
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    // !!! ����� ����: ������, ������� �������� ����� ������ !!!
    [Header("Post-Success UI")]
    public GameObject postSuccessDialoguePanel;

    // !!! �����: ���������� ����� �������� ��� (1 ��� Day1, 2 ��� Day2 � �.�.)
    public int currentLevel = 1;

    private bool isInRange = false;

    void Start()
    {
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (checkPromptUI != null) checkPromptUI.SetActive(false);

        // �����: �������� ���������� ������ ��� ������
        if (postSuccessDialoguePanel != null) postSuccessDialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // �������� �� null (��� ���������)
            if (checkPanelUI == null || ProductManager.Instance == null)
            {
                Debug.LogError("������: �� ����������� UI-������ �������� ��� ProductManager.");
                return;
            }

            // ������ �������� ������ �������� 'checkPanelUI'
            if (checkPanelUI.activeSelf)
            {
                // �������� ������ �������� (���� �� ���� ������)
                checkPanelUI.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            // ������ �������� ����� ���������� ������ 'postSuccessDialoguePanel'
            else if (postSuccessDialoguePanel != null && postSuccessDialoguePanel.activeSelf)
            {
                // ���� ����� �������� 'E', ����� ������� ���������� ������, 
                // �� ������������, ��� �� ����� �� �������.
                postSuccessDialoguePanel.SetActive(false);
                if (checkPromptUI != null) checkPromptUI.SetActive(true);
            }
            // ������ �������� � ��������
            else
            {
                // �������� ���������
                if (checkPromptUI != null) checkPromptUI.SetActive(false);

                // �������� ��������
                bool allCorrect = ProductManager.Instance.CheckAllDecisions();

                // 1. ���������� ��������� (Success/Failure)
                checkPanelUI.SetActive(true);
                if (successImage != null && failureImage != null)
                {
                    successImage.SetActive(allCorrect);
                    failureImage.SetActive(!allCorrect);
                }

                if (allCorrect)
                {
                    // 2. ��������� ��������
                    ProgressManager.CompleteDay(currentLevel);

                    // =======================================================
                    // !!! ����� ������: ��������� ���������� ������ !!!
                    // =======================================================
                    if (postSuccessDialoguePanel != null)
                    {
                        // �������� ������ �������� (������� �������� �����)
                        checkPanelUI.SetActive(false);
                        // ���������� ������ � ��������
                        postSuccessDialoguePanel.SetActive(true);
                    }
                    // =======================================================
                }
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

            // ��������� ��� ������ ��� ������ �� ���� ��������
            if (checkPanelUI != null && checkPanelUI.activeSelf)
            {
                checkPanelUI.SetActive(false);
            }
            if (postSuccessDialoguePanel != null && postSuccessDialoguePanel.activeSelf)
            {
                postSuccessDialoguePanel.SetActive(false);
            }
        }
    }
}