using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotL6 : MonoBehaviour, IDropHandler
{
    [Header("Handler for this analyzer (Biosensor/ELISA/GC/ICPMS)")]
    public MonoBehaviour handler;     // ���� � ���������� ���������� ������ � ������ HandlerL6
    public Transform contentArea;     // ����, ���� ���������� ������ ��������

    public void OnDrop(PointerEventData eventData)
    {
        // ���������, ���� �� ������
        DraggableItem droppedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (droppedItem == null) return;

        // 1. �������� ������ ������ ��������
        droppedItem.transform.SetParent(contentArea);
        droppedItem.transform.localPosition = Vector3.zero;

        // 2. �������� InventoryManagerL6, ����� ������� ������
        if (InventoryManagerL6.Instance != null)
        {
            InventoryManagerL6.Instance.selectedProduct = droppedItem.productName;
        }

        // 3. ��������� ����� ����������� ����� handler
        if (handler != null)
        {
            string readyMsg = $"Product '{droppedItem.productName}' ready. Press Detect.";

            if (handler is BiosensorHandlerL6 biosensor)
            {
                biosensor.resultText.text = readyMsg;
            }
            else if (handler is ELISAHandlerL6 elisa)
            {
                elisa.resultText.text = readyMsg;
            }
            else if (handler is GCHandlerL6 gc)
            {
                gc.resultText.text = readyMsg;
            }
            else if (handler is ICPMSHandlerL6 xrf)
            {
                xrf.resultText.text = readyMsg;
            }
        }
    }
}
