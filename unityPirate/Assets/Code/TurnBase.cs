using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnBase : MonoBehaviour
{
    public static bool turnPlayer = true;
    
    public static bool PlayerShoot = false;
    public static bool EnemyShoot = false;

    public GameObject EnemyGun;
    public GameObject Enemy1Gun;
    public static bool two = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyGun == null && two == false)
        {
            Enemy1Gun.gameObject.SetActive(true);
            two = true;
        }
        
    }
}
