using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public GameObject changePlyer;
    public TurnBase turnBase;
    private bool isMovingBack;
    public Vector3 originalPosition;

    private int hp = 100;
    
    public int currentHealth;
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public GameObject hpBar;

    void Start()
    {
        currentHealth = hp;
        isMovingBack = false;
    }
    
    void Update()
    {
        if (currentHealth <= 0)
        {
            turnBase.OnPlayerDestroyed(gameObject);
            nextPlayer();
            Destroy(hpBar.gameObject);
            
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
                BulletEnemy.playerMoveToOrigin = false;
            }
        }
        if (BulletEnemy.playerMoveToOrigin == true)
        {
            StartCoroutine(Delay(1f));
            
        }
    }
    
    //private void OnTriggerEnter2D(Collider2D other)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BulletEnemy")
        {
            int atk = 50;
            RecieveDmg(atk);
            SetHealth(currentHealth);
            // Push the enemy back
            Vector2 bulletVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            transform.position -= (Vector3)bulletVelocity.normalized * 0.5f;
        }
        if (other.gameObject.tag == "Water")
        {
            turnBase.OnPlayerDestroyed(gameObject);
            nextPlayer();
            Destroy(hpBar.gameObject);
          
        }
        if (other.gameObject.tag == "Player")
        {
            // Handle collision with another player here
            RecieveDmg(0);
        }
    }
    public void RecieveDmg(int atkEnemy)
    { 
        
        currentHealth = currentHealth - atkEnemy;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    IEnumerator Delay(float timeRemaining)
    {
        for (float i = timeRemaining; i > 0; i--)
        {
            yield return new WaitForSeconds(0.5f);
        }
        isMovingBack = true;
    }
    public void nextPlayer()
    {
       changePlyer.gameObject.SetActive(true);
    }
}