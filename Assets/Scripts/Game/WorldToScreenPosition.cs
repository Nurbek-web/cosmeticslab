using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    // ������, � �������� ����� ��������� UI (��� ����/������ ��������)
    public Transform worldObject;

    // �������� �� ��� Y (��������, 1.0f), ����� PNG ��� ��� ��������
    public Vector3 offset;

    // ��������� RectTransform ������ UI-��������
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // ����������� ���������, ��� ��� UI-������� ��������� �� Canvas 
        // � ��� ������������ Canvas ��������� � ������ Render Mode: Screen Space - Overlay ��� Screen Space - Camera.
    }

    void Update()
    {
        if (worldObject == null || rectTransform == null)
            return;

        // 1. �������� ������� ������� ������� � ��������� ��������
        Vector3 worldPosition = worldObject.position + offset;

        // 2. ����������� ������� ������� � �������� �������
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // 3. ������������� ��� ������� ��� RectTransform
        // ��������� RectTransform �������� � ����������� Canvas, ��� ������������� �������� ���
        rectTransform.position = screenPosition;
    }
}