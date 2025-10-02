using UnityEngine;
using UnityEngine.UI; // Обязательно для работы с UI элементами

public class BookUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject bookContainer;     // 'BackgroundDim' - общий контейнер для затемнения и книги
    public Image bookImage;              // 'Book' - компонент Image самой книги
    public Button nextPageButton;        // 'NextPageButton'
    public Button prevPageButton;        // 'PrevPageButton'

    [Header("Book Pages")]
    // Массив для хранения спрайтов страниц книги. Перетащите ваши 4 PNG сюда в Инспекторе
    public Sprite[] pages;

    private int currentPageIndex = 0; // Текущий индекс страницы (начинаем с 0)

    void Start()
    {
        // Инициализация. Убедитесь, что книга и кнопки скрыты при старте.
        bookContainer.SetActive(false);

        // Привязываем методы к событиям нажатия на кнопки
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);

        // Убедимся, что начальная страница загружена
        if (pages.Length > 0)
        {
            bookImage.sprite = pages[currentPageIndex];
            UpdateButtonVisibility(); // Обновляем видимость кнопок
        }
    }

    // Метод для открытия/закрытия книги
    void Update()
    {
        // Проверяем нажатие на кнопку 'B' (KeyCode.B)
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBook();
        }
    }

    public void ToggleBook()
    {
        // Переключаем активность контейнера
        bool isActive = !bookContainer.activeSelf;
        bookContainer.SetActive(isActive);

        // При открытии, устанавливаем текущую страницу и обновляем кнопки
        if (isActive && pages.Length > 0)
        {
            currentPageIndex = 0; // Можно сбрасывать на первую страницу при открытии
            bookImage.sprite = pages[currentPageIndex];
            UpdateButtonVisibility();
        }

        // При необходимости, можно приостановить игру: 
        // Time.timeScale = isActive ? 0f : 1f; 
    }

    // Метод для перелистывания вперед
    public void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++; // Увеличиваем индекс
            bookImage.sprite = pages[currentPageIndex]; // Меняем спрайт (страница меняется мгновенно)
            UpdateButtonVisibility(); // Обновляем видимость кнопок
        }
    }

    // Метод для перелистывания назад
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--; // Уменьшаем индекс
            bookImage.sprite = pages[currentPageIndex]; // Меняем спрайт
            UpdateButtonVisibility(); // Обновляем видимость кнопок
        }
    }

    // Метод для управления видимостью кнопок
    void UpdateButtonVisibility()
    {
        // Кнопка назад видна, только если мы не на первой странице
        prevPageButton.gameObject.SetActive(currentPageIndex > 0);

        // Кнопка вперед видна, только если мы не на последней странице
        nextPageButton.gameObject.SetActive(currentPageIndex < pages.Length - 1);
    }
}