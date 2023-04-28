using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public Vector3 originalPosition;
    private bool isMovingBack;

    PlayerConstrutor enemy = new PlayerConstrutor(100, 10, 10);

    void Start()
    {
        enemy.currentHealth = enemy.Hp;
        originalPosition = transform.position;
        isMovingBack = false;
    }

    void Update()
    {
        if (isMovingBack == true)
        {
            // Move the enemy back to their original position
            float step = 5f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);

            // Check if the enemy has reached their original position
            if (transform.position == originalPosition)
            {
                isMovingBack = false;
            }
        }
        if (EnemyGun.move == true)
        {
            isMovingBack = true;
            EnemyGun.move = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemy.RecieveDmg(10);
            healthBar.SetHealth(enemy.currentHealth);

            // Push the enemy back
            Vector2 bulletVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            transform.position -= (Vector3)bulletVelocity.normalized * 0.5f;
            
        }
        if (other.gameObject.tag == "Water")
        {
            Destroy(gameObject);
        }
    }
}