using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlotL4 : MonoBehaviour, IDropHandler
{
    // Универсальный обработчик (ICMSHandlerL4, BiosensorHandlerL4, ELISAHandler)
    public MonoBehaviour handler;
    public Transform contentArea;

    public void OnDrop(PointerEventData eventData)
    {
        // ОБЪЯВЛЕНИЕ И ИНИЦИАЛИЗАЦИЯ droppedItem - ЭТО КРИТИЧЕСКИ ВАЖНО!
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            // 1. Устанавливаем иконку как дочерний объект DropSlot
            droppedItem.transform.SetParent(contentArea);
            droppedItem.transform.localPosition = Vector3.zero;

            // 2. ГЛАВНОЕ: Сообщаем InventoryManagerL4, какой продукт был выбран
            if (InventoryManagerL4.Instance != null)
            {
                InventoryManagerL4.Instance.selectedProductForAnalysis = droppedItem.productName;
            }

            // 3. Обновляем текст анализатора (через прикрепленный handler)
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
