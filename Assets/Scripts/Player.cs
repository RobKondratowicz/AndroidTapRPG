using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public float Health           {get; set;} = 2;
    public float AttackPower      {get; set;} = 1;
    public bool  Dead             {get; set;} = false;
    public int   upgradePoints    {get; set;} = 0;
    public float HealthRegenDelay {get; set;} = 2;
    public float HealthRegenSpeed {get; set;} = 5;
    
    public float maxHealth        {get; set;} = 10;
    public float maxAttackPower   {get; set;} = 10;

    public float currentHealth  {get; set;}
    

    private float timeSinceLastDamaged = 0;

    public Player()
    {
        currentHealth = Health;
        Debug.Log(Health);
    }
    
    public bool Damage(float amount)
    {
        timeSinceLastDamaged = 0;
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Dead = true;
        }
        return Dead;
    }

    public void upgradeHealth()
    {
        if(upgradePoints > 0)
        {
            Health++;
            upgradePoints--;
        }
    }

    public void upgradeAttackPower()
    {
        if(upgradePoints > 0)
        {
            AttackPower++;
            upgradePoints--;
        }
    }

    public void update()
    {
        Debug.Log(timeSinceLastDamaged < HealthRegenDelay);
        timeSinceLastDamaged += Time.deltaTime;
        if(timeSinceLastDamaged > HealthRegenDelay && currentHealth < Health && !Dead)
        {
            currentHealth += HealthRegenSpeed * Time.deltaTime;
            if(currentHealth > Health)
                currentHealth = Health;
        }
    }
}
