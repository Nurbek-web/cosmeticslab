using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotL4 : MonoBehaviour, IDropHandler
{
    // ������������� ���������� (ICMSHandlerL4, BiosensorHandlerL4, ELISAHandler)
    public MonoBehaviour handler;
    public Transform contentArea;

    public void OnDrop(PointerEventData eventData)
    {
        // ���������� � ������������� droppedItem - ��� ���������� �����!
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // 1. ������������� ������ ��� �������� ������ DropSlot
            droppedItem.transform.SetParent(contentArea);
            droppedItem.transform.localPosition = Vector3.zero;

            // 2. �������: �������� InventoryManagerL4, ����� ������� ��� ������
            if (InventoryManagerL4.Instance != null)
            {
                InventoryManagerL4.Instance.selectedProductForAnalysis = droppedItem.productName;
            }

            // 3. ��������� ����� ����������� (����� ������������� handler)
            if (handler != null)
            {
                if (handler is ICMSHandlerL4 icms)
                {
                    icms.analysisResultText.text = "Product ready for ICP-MS. Press Detect.";
                }
                else if (handler is BiosensorHandlerL4 bio)
                {
                    bio.analysisResultText.text = "Product ready for Biosensor. Press Detect.";
                }
                else if (handler is ELISAHandler elisa)
                {
                    elisa.analysisResultText.text = "Product ready for ELISA. Press Detect.";
                }
            }
        }
    }
}
