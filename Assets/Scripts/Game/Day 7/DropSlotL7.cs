using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotL7 : MonoBehaviour, IDropHandler
{
    [Header("Handler for this analyzer (Biosensor / ELISA)")]
    public MonoBehaviour handler;     // ?? ���� � ���������� ���������� ������ � ������ HandlerL7
    public Transform contentArea;     // ?? ����, ���� ����� �������� ������ ��������

    public void OnDrop(PointerEventData eventData)
    {
        // ���������, ��� ��������������� ������
        DraggableItem droppedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (droppedItem == null) return;

        // 1. �������� ������ ������ �����
        droppedItem.transform.SetParent(contentArea);
        droppedItem.transform.localPosition = Vector3.zero;
        droppedItem.transform.localScale = Vector3.one;

        // 2. �������� InventoryManagerL7, ����� ������� ������
        if (InventoryManagerL7.Instance != null)
        {
            InventoryManagerL7.Instance.selectedProduct = droppedItem.productName;
            Debug.Log($"[DropSlotL7] Selected product: {droppedItem.productName}");
        }

        // 3. ��������� UI ����� ����� handler
        if (handler != null)
        {
            string readyMsg = $"Product '{droppedItem.productName}' ready. Press Detect.";

            if (handler is BiosensorHandlerL7 biosensor)
            {
                biosensor.resultText.text = readyMsg;
            }
            else if (handler is ELISAHandlerL7 elisa)
            {
                elisa.resultText.text = readyMsg;
            }
        }
    }
}
