using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Paddle paddle;
    private LineRenderer line;

    public float speed = 20f;
    private bool launched = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        paddle = FindObjectOfType<Paddle>();
        line = GetComponent<LineRenderer>();

        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.positionCount = 2;
            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
        }
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        launched = false;
        rb.linearVelocity = Vector2.zero;

        line.enabled = true;
    }

    private void Update()
    {
        if (!launched)
        {
            transform.position =
                paddle.transform.position + Vector3.up * 0.5f;

            Aim();
        }
    }

    void Aim()
    {
        Vector3 mouse =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction =
            (mouse - transform.position).normalized;

        line.SetPosition(0, transform.position);
        line.SetPosition(1,
            transform.position + (Vector3)(direction * 3f));

        if (Input.GetMouseButtonDown(0))
        {
            launched = true;
            line.enabled = false;

            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (launched)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * speed;
        }
    }
}