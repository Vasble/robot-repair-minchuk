
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    public bool vertical;
    public bool randomPath;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2d;
    Animator animator;

    bool broken = true;
    float timer;
    int direction = 1;
    bool movingVertical;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        movingVertical = vertical;
        timer = changeTime;
    }

    void FixedUpdate()
    {
        if (!broken) return;

        Vector2 movement;

        if (movingVertical)
        {
            movement = new Vector2(0, direction);
        }
        else
        {
            movement = new Vector2(direction, 0);
        }

        Vector2 position = rigidbody2d.position
            + movement * speed * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);

        animator.SetFloat("Move X", movement.x);
        animator.SetFloat("Move Y", movement.y);
    }

    void Update()
    {
        if (!broken) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;

            if (randomPath)
            {
                movingVertical = Random.value < 0.5f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player =
            other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
