using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour
{
    // =======================================================
    // ЧАСТЬ 1: Визуальное отображение (Объекты Image)
    // =======================================================
    [Tooltip("Перетащите сюда 7 объектов (GameObjects), каждый из которых содержит Image с соответствующим PNG. Индекс 0 = Day 1, Индекс 6 = Day 7.")]
    public GameObject[] backgroundDayObjects = new GameObject[7];

    // =======================================================
    // ЧАСТЬ 2: Конфигурация Кнопок (Логика кликов)
    // =======================================================
    [System.Serializable]
    public class DayButtonConfig
    {
        public int dayNumber;           // 1, 2, 3...
        public Button buttonComponent;   // Кнопка для клика
        public string sceneName;        // "Day1", "Day2", и т.д.
    }

    public List<DayButtonConfig> dayButtons;
    public string menuSceneName = "Menu";

    void Start()
    {
        UpdateLobbyVisuals();
    }

    /// <summary>
    /// Обновляет внешний вид и интерактивность кнопок.
    /// </summary>
    private void UpdateLobbyVisuals()
    {
        int maxAccessibleDay = ProgressManager.GetMaxAccessibleDay();

        // --- 1. Управление Фоновыми Объектами PNG ---

        // Индекс для активации объекта = (Макс. доступный день) - 1.
        // Clamp гарантирует, что мы не выйдем за пределы массива [0...6].
        int targetIndex = Mathf.Clamp(maxAccessibleDay - 1, 0, backgroundDayObjects.Length - 1);

        // Перебираем все фоновые объекты
        for (int i = 0; i < backgroundDayObjects.Length; i++)
        {
            if (backgroundDayObjects[i] != null)
            {
                // Активным делаем только тот объект, чей индекс соответствует targetIndex
                backgroundDayObjects[i].SetActive(i == targetIndex);
            }
        }

        // --- 2. Управление Кнопками для Клика ---

        foreach (var config in dayButtons)
        {
            bool isAccessible = config.dayNumber <= maxAccessibleDay;

            if (config.buttonComponent != null)
            {
                // Отключаем или включаем возможность нажать на кнопку
                config.buttonComponent.interactable = isAccessible;

                // Настраиваем действие при клике
                config.buttonComponent.onClick.RemoveAllListeners();
                config.buttonComponent.onClick.AddListener(() => LoadLevel(config.sceneName, isAccessible));
            }
        }
    }

    /// <summary>
    /// Загружает указанный уровень, если он доступен.
    /// </summary>
    public void LoadLevel(string sceneName, bool isAccessible)
    {
        if (isAccessible)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// Метод для возврата в Главное Меню.
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}