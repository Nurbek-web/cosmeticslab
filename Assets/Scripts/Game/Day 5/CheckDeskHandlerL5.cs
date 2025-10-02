using UnityEngine;

public class CheckDeskHandlerL5 : MonoBehaviour
{
    public GameObject checkPromptUI;
    public GameObject checkPanelUI;
    public GameObject successImage;
    public GameObject failureImage;

    private bool isInRange = false;

    void Start()
    {
        if (checkPromptUI != null) checkPromptUI.SetActive(false);
        if (checkPanelUI != null) checkPanelUI.SetActive(false);
        if (successImage != null) successImage.SetActive(false);
        if (failureImage != null) failureImage.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (checkPanelUI.activeSelf)
            {
                // �������� ������
                checkPanelUI.SetActive(false);
                checkPromptUI.SetActive(true);
            }
            else
            {
                // ��������, ��� ��� 4 ������� �������
                // ��������: ��� �������� ������ ������ �������! 
                // ��� ����� �������� ProductManagerL5.productsDecided �� public.
                if (ProductManagerL5.Instance == null || ProductManagerL5.Instance.productsDecided < 4)
                {
                    Debug.Log("L5: Not all product decisions have been recorded yet.");
                    return; // �� ���������, ���� ������� �� �������
                }

                // �������� � ��������
                checkPanelUI.SetActive(true);
                checkPromptUI.SetActive(false);

                bool allCorrect = ProductManagerL5.Instance.CheckAllDecisions();

                // ����������� ����������
                successImage.SetActive(allCorrect);
                failureImage.SetActive(!allCorrect);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // ���������� E Prompt, ������ ���� ��� ������� �������
            // ��������: ��� �������� ������ ������ �������! 
            // ��� ����� �������� ProductManagerL5.productsDecided �� public.
            if (ProductManagerL5.Instance != null && ProductManagerL5.Instance.productsDecided >= 4)
            {
                if (checkPromptUI != null && !checkPanelUI.activeSelf)
                {
                    checkPromptUI.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (checkPromptUI != null) checkPromptUI.SetActive(false);
            if (checkPanelUI != null) checkPanelUI.SetActive(false);

            // �������� ��������� ��� ������
            if (successImage != null) successImage.SetActive(false);
            if (failureImage != null) failureImage.SetActive(false);
        }
    }
}
