using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    private bool isMovingBack;
    public Vector3 originalPosition;
    PlayerConstrutor player = new PlayerConstrutor(100,10,10);
    
    
    
    void Start()
    {
       
        player.currentHealth = player.Hp;
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
            player.RecieveDmg(10);
            healthBar.SetHealth(player.currentHealth);
        }
        if (other.gameObject.tag == "Water")
        {
            StartCoroutine(StartTimer(2));
        }
    } 
    IEnumerator StartTimer(int timeRemaining)
    {
        for (int i = timeRemaining; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }
}