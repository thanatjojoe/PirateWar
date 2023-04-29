using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
  
    public Vector3 originalPosition;
    private bool isMovingBack;
    
    
    public int hp = 100; 
    public static int currentHealth;
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public GameObject hpBar;

    void Start()
    {
        currentHealth = hp;
        originalPosition = transform.position;
        isMovingBack = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(hpBar.gameObject);
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
                EnemyGun.move = false;
            }
        }
        if (EnemyGun.move == true)
        {

            StartCoroutine(Delay(1f));
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        { 
            RecieveDmg(10);
           SetHealth(currentHealth);

            // Push the enemy back
            Vector2 bulletVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            transform.position -= (Vector3)bulletVelocity.normalized * 0.5f;
            
        }
        if (other.gameObject.tag == "Water")
        {
            Destroy(hpBar.gameObject);
            Destroy(gameObject);
        }
    }
    public void RecieveDmg(int atkEnemy)
    { 
        currentHealth -= atkEnemy;
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
}