using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    // Объект, к которому нужно привязать UI (ваш стол/объект проверки)
    public Transform worldObject;

    // Смещение по оси Y (например, 1.0f), чтобы PNG был над объектом
    public Vector3 offset;

    // Компонент RectTransform вашего UI-элемента
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Обязательно убедитесь, что ваш UI-элемент находится на Canvas 
        // и его родительский Canvas находится в режиме Render Mode: Screen Space - Overlay или Screen Space - Camera.
    }

    void Update()
    {
        if (worldObject == null || rectTransform == null)
            return;

        // 1. Получаем мировую позицию объекта и применяем смещение
        Vector3 worldPosition = worldObject.position + offset;

        // 2. Преобразуем мировую позицию в экранную позицию
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // 3. Устанавливаем эту позицию для RectTransform
        // Поскольку RectTransform работает в координатах Canvas, это автоматически привяжет его
        rectTransform.position = screenPosition;
    }
}