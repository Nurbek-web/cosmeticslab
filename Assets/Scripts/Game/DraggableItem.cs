using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic; // Добавьте, если используете словари

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // (Настроить в Инспекторе) Имя продукта: "WC1", "WC2", "WC3" или "WC4"
    public string productName;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // ВАЖНО: Хранит исходную ЛОКАЛЬНУЮ позицию.
    private Vector3 originalLocalPosition;

    // Хранит ссылку на исходного родителя (панель, где иконка находилась изначально)
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        // Находим родительский Canvas
        canvas = GetComponentInParent<Canvas>();

        // Сохраняем исходные значения
        originalLocalPosition = transform.localPosition;
        originalParent = transform.parent;
    }

    // Начало перетаскивания
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 1. Сбрасываем родителя временно на Canvas (чтобы элемент был поверх всего)
        transform.SetParent(canvas.transform);

        canvasGroup.blocksRaycasts = false; // Позволяем лучу проходить сквозь этот объект (чтобы найти DropSlot)

        // 2. Очищаем выбранный продукт при начале перетаскивания
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.selectedProductForAnalysis = "";
        }
    }

    // Во время перетаскивания
    public void OnDrag(PointerEventData eventData)
    {
        // Перемещаем иконку за курсором
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Конец перетаскивания (когда кнопка мыши отпущена)
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Снова делаем объект блокирующим луч

        // Проверяем, был ли элемент успешно брошен в DropSlot.
        // Если DropSlot успешно сработал (OnDrop), он уже изменит родителя.

        // Если текущий родитель - это КАНВАС (а не новый DropSlot),
        // значит, элемент не был брошен в DropSlot.
        if (transform.parent == canvas.transform)
        {
            // Возвращаем на исходного родителя
            transform.SetParent(originalParent);
            // Возвращаем на исходную локальную позицию
            transform.localPosition = originalLocalPosition;
        }

        // Если родитель НЕ Canvas, значит, DropSlot сработал и установил себя как родителя.
    }

    // Опционально: предотвращаем случайные клики, которые могут сбросить позицию
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ничего не делаем при клике, только Drag/Drop
    }
}