using UnityEngine;

public class DoorController2D : MonoBehaviour {
    public Collider2D barrierCollider;
    public SpriteRenderer spriteRenderer;
    public Color lockedColor = new Color(0.8f, 0.2f, 0.2f, 1f);
    public Color openColor = new Color(0.2f, 0.8f, 0.2f, 1f);

    bool _isLocked = true;

    void Awake() {
        SetLocked(true);
    }

    public void SetLocked(bool locked) {
        _isLocked = locked;
        if (barrierCollider) barrierCollider.enabled = locked;
        if (spriteRenderer) spriteRenderer.color = locked ? lockedColor : openColor;
    }
}
