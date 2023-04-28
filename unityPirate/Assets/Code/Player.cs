using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    private bool isMovingBack;
    
    PlayerConstrutor player = new PlayerConstrutor(100,10,10);
    
    
    
    void Start()
    {
       
        player.currentHealth = player.Hp;
    }
    
    void Update()
    {

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