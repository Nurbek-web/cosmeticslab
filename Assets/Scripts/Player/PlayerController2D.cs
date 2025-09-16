using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour {
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // 👉 это свойство нужно для PlayerInteraction2D
    public Vector2 LastMoveDir { get; private set; } = Vector2.right;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;

        // сохраняем направление движения
        if (moveInput.sqrMagnitude > 0.001f)
            LastMoveDir = moveInput;
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
