using TMPro;
using UnityEngine;

public class AnnaDialogueUI : MonoBehaviour {
    public GameObject root;
    public TextMeshProUGUI text;

    void Awake() {
        if (root) root.SetActive(true);
    }

    public void Show(string message) {
        if (text) text.text = message;
        if (root && !root.activeSelf) root.SetActive(true);
    }
}
