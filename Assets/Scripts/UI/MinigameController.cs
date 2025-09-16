using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour {
    public GameObject panelRoot;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI ingredientsText;
    public TextMeshProUGUI certificateText;
    public Button approveButton;
    public Button rejectButton;

    DayController _day;
    CosmeticSample _current;

    void Awake() {
        Close();
        approveButton.onClick.AddListener(() => OnDecision(true));
        rejectButton.onClick.AddListener(() => OnDecision(false));
    }

    public void Bind(DayController day) => _day = day;

    public void Open(CosmeticSample sample) {
        _current = sample;
        if (_current == null) return;

        if (panelRoot) panelRoot.SetActive(true);
        if (titleText) titleText.text = $"Проверка состава: {_current.displayName}";
        if (ingredientsText) ingredientsText.text = "Состав (на этикетке):\n- " + string.Join("\n- ", _current.declaredIngredients);
        if (certificateText) certificateText.text = "Сертификат: " + (_current.hasCertificate ? "есть" : "нет");
    }

    public void Close() {
        if (panelRoot) panelRoot.SetActive(false);
        _current = null;
    }

    void OnDecision(bool approve) {
        bool correct = _day.CheckDecision_Day1(_current, approve);
        Close();
        _day.OnMinigameFinished(correct);
    }
}
