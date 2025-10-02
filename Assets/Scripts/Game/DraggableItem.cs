using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic; // ��������, ���� ����������� �������

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // (��������� � ����������) ��� ��������: "WC1", "WC2", "WC3" ��� "WC4"
    public string productName;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // �����: ������ �������� ��������� �������.
    private Vector3 originalLocalPosition;

    // ������ ������ �� ��������� �������� (������, ��� ������ ���������� ����������)
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        // ������� ������������ Canvas
        canvas = GetComponentInParent<Canvas>();

        // ��������� �������� ��������
        originalLocalPosition = transform.localPosition;
        originalParent = transform.parent;
    }

    // ������ ��������������
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 1. ���������� �������� �������� �� Canvas (����� ������� ��� ������ �����)
        transform.SetParent(canvas.transform);

        canvasGroup.blocksRaycasts = false; // ��������� ���� ��������� ������ ���� ������ (����� ����� DropSlot)

        // 2. ������� ��������� ������� ��� ������ ��������������
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.selectedProductForAnalysis = "";
        }
    }

    // �� ����� ��������������
    public void OnDrag(PointerEventData eventData)
    {
        // ���������� ������ �� ��������
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // ����� �������������� (����� ������ ���� ��������)
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // ����� ������ ������ ����������� ���

        // ���������, ��� �� ������� ������� ������ � DropSlot.
        // ���� DropSlot ������� �������� (OnDrop), �� ��� ������� ��������.

        // ���� ������� �������� - ��� ������ (� �� ����� DropSlot),
        // ������, ������� �� ��� ������ � DropSlot.
        if (transform.parent == canvas.transform)
        {
            // ���������� �� ��������� ��������
            transform.SetParent(originalParent);
            // ���������� �� �������� ��������� �������
            transform.localPosition = originalLocalPosition;
        }

        // ���� �������� �� Canvas, ������, DropSlot �������� � ��������� ���� ��� ��������.
    }

    // �����������: ������������� ��������� �����, ������� ����� �������� �������
    public void OnPointerClick(PointerEventData eventData)
    {
        // ������ �� ������ ��� �����, ������ Drag/Drop
    }
}