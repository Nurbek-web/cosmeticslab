using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotL5 : MonoBehaviour, IDropHandler
{
    // ������������� ���������� (GCHandlerL5, BiosensorHandlerL5 � �.�.)
    public MonoBehaviour handler;
    public Transform contentArea;

    public void OnDrop(PointerEventData eventData)
    {
        // ���������, ��� ��� ��������������� �������
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // 1. ���������� ������ � ���� �����������
            droppedItem.transform.SetParent(contentArea);
            droppedItem.transform.localPosition = Vector3.zero;

            // 2. �������� InventoryManagerL5, ����� ������� ������
            if (InventoryManagerL5.Instance != null)
            {
                InventoryManagerL5.Instance.selectedProductForAnalysis = droppedItem.productName;
                Debug.Log($"L5: Selected product for analysis: {droppedItem.productName}");
            }

            // 3. ��������� ����� ����������� ����� handler
            if (handler != null)
            {
                // --- GC ---
                if (handler is GCHandler gc)
                {
                    gc.analysisResultText.text = "Product ready for GC. Press Detect.";
                }
                // --- Biosensor ---
                else if (handler is BiosensorHandlerL5 bio)
                {
                    bio.analysisResultText.text = "Product ready for Biosensor. Press Detect.";
                }
                // --- ��� ������� ������������ (���� ��������) ---
                else
                {
                    Debug.Log("L5: Unknown handler assigned to DropSlotL5");
                }
            }
        }
    }
}
