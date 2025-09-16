using UnityEngine;

public class BookUI : MonoBehaviour {
    public GameObject panelRoot;

    void Update() {
        if (Input.GetKeyDown(KeyCode.B))
            Toggle();
    }

    public void Toggle() {
        if (panelRoot) panelRoot.SetActive(!panelRoot.activeSelf);
    }
}
