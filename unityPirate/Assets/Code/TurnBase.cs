using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TurnBase : MonoBehaviour
{
    public static bool turnPlayer = true;
    
    public static bool PlayerShoot = false;
    public static bool EnemyShoot = false;

    
    public static int score;
    public static int rocket = 0;
    public string checkPoint;
   
    
    public static int stage = 1;
    public List<GameObject> enemies;
    public List<GameObject> players;
    
    // Start is called before the first frame update
    
    void Start()
    {
        turnPlayer = true;
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy")); // find all objects with the "Enemy" tag and add them to the list
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")); // find all objects with the "Enemy" tag and add them to the list

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0)
        {
            stage++;
            SceneManager.LoadScene(checkPoint);
        }
        if (players.Count == 0)
        {
            SceneManager.LoadScene(checkPoint);
        }

        
    }
    public void OnEnemyDestroyed(GameObject enemy)
    {
        // Remove the destroyed enemy from the list
        enemies.Remove(enemy);
        
        // Destroy the enemy GameObject
        Destroy(enemy);
    }
    public void OnPlayerDestroyed(GameObject player)
    {
        // Remove the destroyed enemy from the list
        players.Remove(player);
        
        // Destroy the enemy GameObject
        Destroy(player);
    }

   
   
   
}
