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
        // ������������� Order in Layer.
        // ��� ������ Y-������� (���� �� ������), ��� ������ sortingOrder (�������).
        // ��� ������ Y-������� (���� �� ������), ��� ������ sortingOrder (�����).
        // ��������� �� -100 ����������� ��������, ����� �������� ����������.

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
        }
    }
}