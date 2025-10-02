using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotL5 : MonoBehaviour, IDropHandler
{
    // Универсальный обработчик (GCHandlerL5, BiosensorHandlerL5 и т.д.)
    public MonoBehaviour handler;
    public Transform contentArea;

    public void OnDrop(PointerEventData eventData)
    {
        // Проверяем, что был перетаскиваемый элемент
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // 1. Перемещаем иконку в слот анализатора
            droppedItem.transform.SetParent(contentArea);
            droppedItem.transform.localPosition = Vector3.zero;

            // 2. Сообщаем InventoryManagerL5, какой продукт выбран
            if (InventoryManagerL5.Instance != null)
            {
                InventoryManagerL5.Instance.selectedProductForAnalysis = droppedItem.productName;
                Debug.Log($"L5: Selected product for analysis: {droppedItem.productName}");
            }

            // 3. Обновляем текст анализатора через handler
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
                // --- Для будущих анализаторов (если появятся) ---
                else
                {
                    Debug.Log("L5: Unknown handler assigned to DropSlotL5");
                }
            }
        }
    }
}
