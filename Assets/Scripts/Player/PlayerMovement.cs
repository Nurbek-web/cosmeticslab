using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;

    // 👉 Добавляем свойство для взаимодействия
    public Vector2 LastMoveDir { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Считываем ввод
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // НОРМАЛИЗАЦИЯ: Убеждаемся, что длина вектора не превышает 1.
        // Это устраняет более быстрое диагональное движение.
        movement.Normalize();

        // Если игрок двигается — запоминаем направление
        if (movement != Vector2.zero)
        {
            LastMoveDir = movement;
        }

        // Обновляем анимацию
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        // Используем .sqrMagnitude после нормализации для проверки, двигается ли игрок.
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Двигаем игрока, используя нормализованный вектор
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}