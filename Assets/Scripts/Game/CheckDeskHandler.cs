using UnityEngine;

public class CheckDeskHandler : MonoBehaviour
{
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    // ���������� 2 � ���������� ��� ����� ��������
    public int currentLevel = 1;

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
            if (checkPanelUI == null || ProductManager.Instance == null)
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

                // �������� �������� ��� �������� ������ (2)
                bool allCorrect = ProductManager.Instance.CheckAllDecisions();

                checkPanelUI.SetActive(true);

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