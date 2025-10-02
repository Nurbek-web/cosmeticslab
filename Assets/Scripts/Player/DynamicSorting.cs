using UnityEngine;

public class DynamicSorting : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Устанавливает Order in Layer.
        // Чем меньше Y-позиция (ниже на экране), тем больше sortingOrder (спереди).
        // Чем больше Y-позиция (выше на экране), тем меньше sortingOrder (сзади).
        // Умножение на -100 увеличивает диапазон, чтобы избежать конфликтов.

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
        }
    }
}