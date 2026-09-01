﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool reverseControls = false;

    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetPaddle();
    }

    public void ResetPaddle()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if (reverseControls)
            input *= -1f;

        direction = new Vector2(input, 0f);
    }

    public void ReverseControls(float duration)
    {
        StopCoroutine(nameof(ReverseRoutine));
        StartCoroutine(ReverseRoutine(duration));
    }

    private System.Collections.IEnumerator ReverseRoutine(float duration)
    {
        reverseControls = true;

        yield return new WaitForSeconds(duration);

        reverseControls = false;
    }

    public void ResetPaddlePosition()
    {
        ResetPaddle();
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero) {
            rb.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) {
            return;
        }

        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        Vector2 ballDirection = ball.linearVelocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

        ball.linearVelocity = ballDirection * ball.linearVelocity.magnitude;
    }

}