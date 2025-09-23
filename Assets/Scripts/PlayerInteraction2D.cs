using UnityEngine;

public class PlayerInteraction2D : MonoBehaviour {
    [Header("Raycast")]
    public float interactRange = 1.5f;
    public LayerMask interactableMask;

    [Header("Refs")]
    public PlayerController2D controller;
    public InteractionPromptUI promptUI;

    IInteractable _hovered;

    void Reset() {
        controller = GetComponent<PlayerController2D>();
    }

    void Update() {
        UpdateRaycast();

        if (Input.GetKeyDown(KeyCode.E) && _hovered != null)
            _hovered.Interact(this);
    }

    void UpdateRaycast() {
        _hovered = null;

        Vector2 origin = transform.position;
        Vector2 dir = controller != null ? controller.LastMoveDir : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, dir, interactRange, interactableMask);
        if (hit.collider) {
            _hovered = hit.collider.GetComponent<IInteractable>();
        }

        if (_hovered != null) promptUI.Show(_hovered.Prompt);
        else promptUI.Hide();
    }
}
