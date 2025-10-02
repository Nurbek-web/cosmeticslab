using UnityEngine;

public class InteractionHandlerL6 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject mainInteractionPanelL6;

    private bool isInRange = false;

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanelL6 != null) mainInteractionPanelL6.SetActive(false);
    }

    void Update()
    {
        // ? ��������� ������ ���������
        if (InventoryManagerL6.Instance != null && InventoryManagerL6.Instance.IsBiosensorAnalyzed())
        {
            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                bool isOpen = mainInteractionPanelL6.activeSelf;
                mainInteractionPanelL6.SetActive(!isOpen);
                ePromptUI.SetActive(isOpen);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // ? ��������� ���������� ������ ����� ������� ����������
            if (InventoryManagerL6.Instance != null && InventoryManagerL6.Instance.IsBiosensorAnalyzed())
            {
                if (ePromptUI != null) ePromptUI.SetActive(true);
                Debug.Log("Player ����� � InteractionTrigger � ������ ������ ����� ����������");
            }
            else
            {
                Debug.Log("Interaction ���������� � ����� ������ ������ ����������");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);
            if (mainInteractionPanelL6 != null && mainInteractionPanelL6.activeSelf)
                mainInteractionPanelL6.SetActive(false);
        }
    }
}
