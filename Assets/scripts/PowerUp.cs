using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float fallSpeed = 3f;

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TOCO: " + collision.name);

        if (collision.CompareTag("Paddle"))
        {
            ActivatePower(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("ResetZone"))
        {
            Destroy(gameObject);
        }
    }

    void ActivatePower(GameObject paddle)
    {
        Paddle p = paddle.GetComponent<Paddle>();

        if (p != null)
        {
            p.ReverseControls(5f);
        }
    }
}