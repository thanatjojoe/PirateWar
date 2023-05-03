using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject changeEnemy;
    public TurnBase turnBase;
    public Vector3 originalPosition;
    private bool isMovingBack;

    public int hp = 100; 
    
    public  int currentHealthEnemy;
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public GameObject hpBar;

    void Start()
    {
        currentHealthEnemy = hp;
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
                EnemyGun.move = false;
            }
        }
        if (EnemyGun.move == true)
        {
            StartCoroutine(Delay(1f));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BulletPlayer")
        { 
            int atk = 50;
            RecieveDmg(atk);
           SetHealth(currentHealthEnemy);

            // Push the enemy back
            Vector2 bulletVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            transform.position -= (Vector3)bulletVelocity.normalized * 0.5f;
            if (currentHealthEnemy <= 0)
            {
                turnBase.OnEnemyDestroyed(gameObject);
                TurnBase.score = TurnBase.score + 10; 
                Destroy(hpBar.gameObject);
                Destroy(gameObject);
                nextEnemy();
            }
        }

        if (other.gameObject.tag == "BulletRocket")
        {
            int atk = 100;
            RecieveDmg(atk);
            SetHealth(currentHealthEnemy);

            // Push the enemy back
            Vector2 bulletVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            transform.position -= (Vector3) bulletVelocity.normalized * 0.5f;
            if (currentHealthEnemy <= 0)
            {
                turnBase.OnEnemyDestroyed(gameObject);
                TurnBase.score = TurnBase.score + 10;
                Destroy(hpBar.gameObject);
                Destroy(gameObject);
                nextEnemy();
            }
        }
        if (other.gameObject.tag == "Water")
        {
            turnBase.OnEnemyDestroyed(gameObject);
            TurnBase.score = TurnBase.score + 10;
            nextEnemy();
            Destroy(hpBar.gameObject);

        }


    }
    public void RecieveDmg(int atkEnemy)
    { 
        
        currentHealthEnemy = currentHealthEnemy - atkEnemy;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void nextEnemy()
    {
        changeEnemy.gameObject.SetActive(true);
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