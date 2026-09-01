﻿using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Sprite[] states = new Sprite[0];
    public int points = 100;
    public bool unbreakable;

    private SpriteRenderer spriteRenderer;
    private int health;

    public GameObject powerUpPrefab;
    [Range(0f,1f)]
    public float powerChance = 0.3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

        public void ResetBrick()
{
    gameObject.SetActive(true);

    if (states == null || states.Length == 0)
    {
        Debug.LogError("Brick sin sprites asignados");
        return;
    }

    int difficulty = GameManager.Instance != null 
        ? GameManager.Instance.GetBrickHealth() 
        : 1;

    health = Mathf.Min(difficulty, states.Length);

    spriteRenderer.sprite = states[health - 1];
}

   private void Hit()
    {
        if (unbreakable) return;

        health--;

        if (health <= 0)
        {
            SpawnPowerUp();   
            gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite =
                states[Mathf.Clamp(health - 1, 0, states.Length - 1)];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    void SpawnPowerUp()
    {
        Debug.Log("Intentando crear powerup");

        if (Random.value <= powerChance && powerUpPrefab != null)
        {
            Debug.Log("PowerUp creado");

            Instantiate(
                powerUpPrefab,
                transform.position + Vector3.down * 0.3f,
                Quaternion.identity
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") {
            Hit();
        }
    }

}