using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
   private Rigidbody2D rb;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   void Update()
   {
      float angle = Mathf.Atan2(rb.velocity.y,rb.velocity.x) * Mathf.Rad2Deg;
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.tag == "Water" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boat")
      {
         GetComponent<Collider2D>().enabled = false;
         StartCoroutine(StartTimer(1));
      }
   }
   IEnumerator StartTimer(int timeRemaining)
   {
      for (int i = timeRemaining; i > 0; i--)
      {
         yield return new WaitForSeconds(1);
      }
      TurnBase.EnemyShoot = true; 
      Destroy(gameObject);
     
   }
}
