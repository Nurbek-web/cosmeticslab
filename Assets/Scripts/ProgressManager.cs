using UnityEngine;

public static class ProgressManager
{
    // Ключ для PlayerPrefs
    private const string MaxLevelKey = "MaxAccessibleDay";
    // День 1 всегда доступен
    private const int DefaultMaxLevel = 1;

    /// <summary>
    /// Возвращает номер максимального дня, который игрок может начать.
    /// </summary>
    public static int GetMaxAccessibleDay()
    {
        return PlayerPrefs.GetInt(MaxLevelKey, DefaultMaxLevel);
    }

    /// <summary>
    /// Устанавливает новый максимальный день, если он больше текущего и не превышает 7.
    /// </summary>
    /// <param name="completedDay">Номер дня, который игрок только что завершил (например, 1).</param>
    public static void CompleteDay(int completedDay)
    {
        int nextDay = completedDay + 1;
        int currentMaxDay = GetMaxAccessibleDay();

        if (nextDay > currentMaxDay && nextDay <= 8) // До 8, так как 8 означает "все 7 дней завершены"
        {
            PlayerPrefs.SetInt(MaxLevelKey, nextDay);
            PlayerPrefs.Save();
            Debug.Log($"Прогресс сохранен: Открыт Day {nextDay}");
        }
    }

    // Опционально: метод для сброса прогресса (для тестирования)
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(MaxLevelKey);
        PlayerPrefs.Save();
        Debug.Log("Прогресс сброшен до Day 1.");
    }
}