using System.Collections;
using System.Collections.Generic;

public class PlayerConstrutor

{ 
    //prop
    public int Hp; 
    public int Mp;
    public int Atk;
    public int currentHealth;
  

    //constructor
    public PlayerConstrutor(int hp, int mp, int atk)
    {
        Hp = hp;
        
    }
  
    //method
    public void Attack()
    {
     
    }
    public void RecieveDmg(int atkEnemy)
    { 
        currentHealth -= atkEnemy;
    }

    
}