using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReportUI : MonoBehaviour {
    public GameObject panelRoot;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI bodyText;
    public Button continueButton;

    Action _onClose;

    void Awake() {
        Close();
        continueButton.onClick.AddListener(() => {
            Close();
            _onClose?.Invoke();
        });
    }

    public void Open(int day, bool success, Action onClose) {
        _onClose = onClose;
        if (panelRoot) panelRoot.SetActive(true);

        if (titleText) titleText.text = $"Отчёт — День {day}";
        if (bodyText) bodyText.text = success
            ? "Все решения верны. Готов к следующему дню!"
            : "Есть ошибки в решениях. Попробуйте ещё раз.";

        // Текст на кнопке можно менять через TMP компонент прямо в инспекторе,
        // либо добавить здесь логику смены надписи.
    }

    public void Close() {
        if (panelRoot) panelRoot.SetActive(false);
    }
}
