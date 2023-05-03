using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public static bool playerMoveToOrigin = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y,rb.velocity.x) * Mathf.Rad2Deg;
    }
   
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Water" || other.gameObject.tag == "Player" || other.gameObject.tag == "Boat")
        {
            StartCoroutine(StartTimer(0.5f));
        }
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator StartTimer(float timeRemaining)
    {
        for (float i = timeRemaining; i > 0; i--)
        {
            yield return new WaitForSeconds(0.5f);
        }
        playerMoveToOrigin = true;
        TurnBase.EnemyShoot = false;
        TurnBase.PlayerShoot = true;
        TurnBase.turnPlayer = true;
        EnemyGun.move = false;
        Destroy(gameObject);
    }
}