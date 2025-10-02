using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    // ������ �� ��� ������ BiosensorHandler ��� ���������� UI
    public BiosensorHandler handler;

    // ������ �� �������� ������ Image, ������� ���������� ��������� �������
    public Transform contentArea;

    // ����������, ����� DraggableItem ������ � ��� �������
    public void OnDrop(PointerEventData eventData)
    {
        // ���������, ��� ��� DraggableItem
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // 1. ������������� ������ ��� �������� ������ DropSlot
            droppedItem.transform.SetParent(contentArea);

            // 2. ���������� ������� (����� �� ����� � ����� DropSlot)
            droppedItem.transform.localPosition = Vector3.zero;

            // 3. �������� InventoryManager, ����� ������� ��� ������
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.selectedProductForAnalysis = droppedItem.productName;
            }

            // 4. ������� ������ ��������� (���� ����)
            if (handler != null)
            {
                handler.analysisResultText.text = "Product ready. Press Detect to analyze...";
            }
        }
    }
}