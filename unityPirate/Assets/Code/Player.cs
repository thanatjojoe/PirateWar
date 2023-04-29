using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    private bool isMovingBack;
    public Vector3 originalPosition;

    private int hp = 100;
    public static int currentHealth;

   
    
    void Start()
    {
        currentHealth = hp;
        isMovingBack = false;
    }
    
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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
        if (BulletEnemy.playerMoveToOrigin == true)
        {
            isMovingBack = true;
            BulletEnemy.playerMoveToOrigin = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            RecieveDmg(10);
            healthBar.SetHealth(currentHealth);
        }
        if (other.gameObject.tag == "Water")
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }
    public void RecieveDmg(int atkEnemy)
    { 
        currentHealth -= atkEnemy;
    }
}