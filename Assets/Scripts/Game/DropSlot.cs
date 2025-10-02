using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    // Ссылка на сам объект BiosensorHandler для обновления UI
    public BiosensorHandler handler;

    // Ссылка на дочерний объект Image, который отображает брошенный продукт
    public Transform contentArea;

    // Вызывается, когда DraggableItem брошен в эту область
    public void OnDrop(PointerEventData eventData)
    {
        // Проверяем, что это DraggableItem
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // 1. Устанавливаем иконку как дочерний объект DropSlot
            droppedItem.transform.SetParent(contentArea);

            // 2. Сбрасываем позицию (чтобы он встал в центр DropSlot)
            droppedItem.transform.localPosition = Vector3.zero;

            // 3. Сообщаем InventoryManager, какой продукт был выбран
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.selectedProductForAnalysis = droppedItem.productName;
            }

            // 4. Очищаем старый результат (если есть)
            if (handler != null)
            {
                handler.analysisResultText.text = "Product ready. Press Detect to analyze...";
            }
        }
    }
}