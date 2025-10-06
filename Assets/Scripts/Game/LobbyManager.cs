using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour
{
    // =======================================================
    // ����� 1: ���������� ����������� (������� Image)
    // =======================================================
    [Tooltip("���������� ���� 7 �������� (GameObjects), ������ �� ������� �������� Image � ��������������� PNG. ������ 0 = Day 1, ������ 6 = Day 7.")]
    public GameObject[] backgroundDayObjects = new GameObject[7];

    // =======================================================
    // ����� 2: ������������ ������ (������ ������)
    // =======================================================
    [System.Serializable]
    public class DayButtonConfig
    {
        public int dayNumber;           // 1, 2, 3...
        public Button buttonComponent;   // ������ ��� �����
        public string sceneName;        // "Day1", "Day2", � �.�.
    }

    public List<DayButtonConfig> dayButtons;
    public string menuSceneName = "Menu";

    void Start()
    {
        UpdateLobbyVisuals();
    }

    /// <summary>
    /// ��������� ������� ��� � ��������������� ������.
    /// </summary>
    private void UpdateLobbyVisuals()
    {
        int maxAccessibleDay = ProgressManager.GetMaxAccessibleDay();

        // --- 1. ���������� �������� ��������� PNG ---

        // ������ ��� ��������� ������� = (����. ��������� ����) - 1.
        // Clamp �����������, ��� �� �� ������ �� ������� ������� [0...6].
        int targetIndex = Mathf.Clamp(maxAccessibleDay - 1, 0, backgroundDayObjects.Length - 1);

        // ���������� ��� ������� �������
        for (int i = 0; i < backgroundDayObjects.Length; i++)
        {
            if (backgroundDayObjects[i] != null)
            {
                // �������� ������ ������ ��� ������, ��� ������ ������������� targetIndex
                backgroundDayObjects[i].SetActive(i == targetIndex);
            }
        }

        // --- 2. ���������� �������� ��� ����� ---

        foreach (var config in dayButtons)
        {
            bool isAccessible = config.dayNumber <= maxAccessibleDay;

            if (config.buttonComponent != null)
            {
                // ��������� ��� �������� ����������� ������ �� ������
                config.buttonComponent.interactable = isAccessible;

                // ����������� �������� ��� �����
                config.buttonComponent.onClick.RemoveAllListeners();
                config.buttonComponent.onClick.AddListener(() => LoadLevel(config.sceneName, isAccessible));
            }
        }
    }

    /// <summary>
    /// ��������� ��������� �������, ���� �� ��������.
    /// </summary>
    public void LoadLevel(string sceneName, bool isAccessible)
    {
        if (isAccessible)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// ����� ��� �������� � ������� ����.
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}