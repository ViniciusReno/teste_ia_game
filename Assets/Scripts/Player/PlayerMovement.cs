using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        float speed = input.magnitude;
        animator.SetFloat("speed", speed);
        if (speed > 0.01f)
        {
            float direction = Mathf.Atan2(input.y, input.x);
            animator.SetFloat("direction", direction);
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPos = rb.position + input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    private void LateUpdate()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, 0f);
    }
}
