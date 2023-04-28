using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] public Transform bullet;
    [SerializeField] public Transform spawnPoint;
    
    [SerializeField] public float fireInterval = 5f; // The time interval between each fire
    
    [SerializeField] public float detectionRadius = 5f; // The radius that the enemy can detect the player
    [SerializeField] public LayerMask playerLayer; // The layer that the player is on

    private Transform player; // The player object
    
    
    private bool isPlayerDetected = false; // Whether the player is detected or not
    private float timeSinceLastFire = 0f; // Time elapsed since the last bullet was fired
    
    public static bool move = false;
    public  bool canShoot = false;

    private void Update()
    { 
        // Update the time elapsed since the last bullet was fired
        timeSinceLastFire += Time.fixedDeltaTime;

        // Check if the player is within detection range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                // Set the player as the target and mark them as detected
                player = collider.transform;
                isPlayerDetected = true;
                break;
            }
        }

        if (player != null && isPlayerDetected && timeSinceLastFire >= fireInterval)
        {
            // Calculate the direction towards the player
            Vector2 direction = player.position - transform.position;

            // Add some randomness to the direction
            float angleOffset = Random.Range(-20f, 0f);
            Quaternion rotation = Quaternion.AngleAxis(angleOffset, Vector3.forward);
            direction = rotation * direction;

            // Rotate the enemy towards the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Check if the spawnPoint variable is not null
            if (spawnPoint != null)
            {
                spawnPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            } 
            
            // Calculate the fire velocity
            float r = Random.Range(10, 30);
            Vector2 fireVelocity = direction.normalized * r;

            // Check if the player is within firing range
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            if (distanceToPlayer < detectionRadius)
            {
               
              
                // Fire the projectile
                if (TurnBase.EnemyShoot == true)
                {
                    move = true;
                    CountTime();
                }
                if (move == true && canShoot == true)
                {
                    FireProjectile(spawnPoint, fireVelocity);
                }
                
            } 
            // Reset the time elapsed since the last bullet was fired
            timeSinceLastFire = -1f;
            canShoot = false;
        }
    }

    public void FireProjectile(Transform firePoint, Vector2 fireVelocity)
    {
        Transform pr = Instantiate(bullet, firePoint.position, Quaternion.identity);
        pr.GetComponent<Rigidbody2D>().velocity = fireVelocity;
    }
    IEnumerator StartTimer(int timeRemaining = 3)
    {
        for (int i = timeRemaining; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
        }
        canShoot = true;
    }
    void CountTime()
    {
        StartCoroutine(StartTimer());
    }
}
