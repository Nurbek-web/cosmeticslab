using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 (�����), 0, 1 (������)

        // ����������� ��������
        animator.SetBool("isWalking", moveX != 0);

        // �������� ���������
        transform.Translate(Vector2.right * moveX * speed * Time.deltaTime);

        // ��������� �������
        if (moveX > 0) spriteRenderer.flipX = false;
        else if (moveX < 0) spriteRenderer.flipX = true;
    }
}
