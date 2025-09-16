using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour {
    public GameObject root;
    public TextMeshProUGUI text;

    void Awake() => Hide();

    public void Show(string prompt) {
        if (text) text.text = prompt;
        if (root) root.SetActive(true);
    }

    public void Hide() {
        if (root) root.SetActive(false);
    }
}
