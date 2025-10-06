using UnityEngine;

public static class ProgressManager
{
    // ���� ��� PlayerPrefs
    private const string MaxLevelKey = "MaxAccessibleDay";
    // ���� 1 ������ ��������
    private const int DefaultMaxLevel = 1;

    /// <summary>
    /// ���������� ����� ������������� ���, ������� ����� ����� ������.
    /// </summary>
    public static int GetMaxAccessibleDay()
    {
        return PlayerPrefs.GetInt(MaxLevelKey, DefaultMaxLevel);
    }

    /// <summary>
    /// ������������� ����� ������������ ����, ���� �� ������ �������� � �� ��������� 7.
    /// </summary>
    /// <param name="completedDay">����� ���, ������� ����� ������ ��� �������� (��������, 1).</param>
    public static void CompleteDay(int completedDay)
    {
        int nextDay = completedDay + 1;
        int currentMaxDay = GetMaxAccessibleDay();

        if (nextDay > currentMaxDay && nextDay <= 8) // �� 8, ��� ��� 8 �������� "��� 7 ���� ���������"
        {
            PlayerPrefs.SetInt(MaxLevelKey, nextDay);
            PlayerPrefs.Save();
            Debug.Log($"�������� ��������: ������ Day {nextDay}");
        }
    }

    // �����������: ����� ��� ������ ��������� (��� ������������)
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(MaxLevelKey);
        PlayerPrefs.Save();
        Debug.Log("�������� ������� �� Day 1.");
    }
}