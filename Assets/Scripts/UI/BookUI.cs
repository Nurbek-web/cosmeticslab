using UnityEngine;
using UnityEngine.UI; // ����������� ��� ������ � UI ����������

public class BookUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject bookContainer;     // 'BackgroundDim' - ����� ��������� ��� ���������� � �����
    public Image bookImage;              // 'Book' - ��������� Image ����� �����
    public Button nextPageButton;        // 'NextPageButton'
    public Button prevPageButton;        // 'PrevPageButton'

    [Header("Book Pages")]
    // ������ ��� �������� �������� ������� �����. ���������� ���� 4 PNG ���� � ����������
    public Sprite[] pages;

    private int currentPageIndex = 0; // ������� ������ �������� (�������� � 0)

    void Start()
    {
        // �������������. ���������, ��� ����� � ������ ������ ��� ������.
        bookContainer.SetActive(false);

        // ����������� ������ � �������� ������� �� ������
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);

        // ��������, ��� ��������� �������� ���������
        if (pages.Length > 0)
        {
            bookImage.sprite = pages[currentPageIndex];
            UpdateButtonVisibility(); // ��������� ��������� ������
        }
    }

    // ����� ��� ��������/�������� �����
    void Update()
    {
        // ��������� ������� �� ������ 'B' (KeyCode.B)
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBook();
        }
    }

    public void ToggleBook()
    {
        // ����������� ���������� ����������
        bool isActive = !bookContainer.activeSelf;
        bookContainer.SetActive(isActive);

        // ��� ��������, ������������� ������� �������� � ��������� ������
        if (isActive && pages.Length > 0)
        {
            currentPageIndex = 0; // ����� ���������� �� ������ �������� ��� ��������
            bookImage.sprite = pages[currentPageIndex];
            UpdateButtonVisibility();
        }

        // ��� �������������, ����� ������������� ����: 
        // Time.timeScale = isActive ? 0f : 1f; 
    }

    // ����� ��� �������������� ������
    public void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++; // ����������� ������
            bookImage.sprite = pages[currentPageIndex]; // ������ ������ (�������� �������� ���������)
            UpdateButtonVisibility(); // ��������� ��������� ������
        }
    }

    // ����� ��� �������������� �����
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--; // ��������� ������
            bookImage.sprite = pages[currentPageIndex]; // ������ ������
            UpdateButtonVisibility(); // ��������� ��������� ������
        }
    }

    // ����� ��� ���������� ���������� ������
    void UpdateButtonVisibility()
    {
        // ������ ����� �����, ������ ���� �� �� �� ������ ��������
        prevPageButton.gameObject.SetActive(currentPageIndex > 0);

        // ������ ������ �����, ������ ���� �� �� �� ��������� ��������
        nextPageButton.gameObject.SetActive(currentPageIndex < pages.Length - 1);
    }
}