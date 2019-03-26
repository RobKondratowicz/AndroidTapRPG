using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : TapBlock
{
    public float Health = 10;
    public float AttackPower = 1;
    public bool Dead = false;
    public float healthRegenDelay = 2f;
    public float healthRegenSpeed = 5f;
    
    private float timeSinceLastAttacked = 0f;
    private float maxHealth = 0;
    
    public SpriteRenderer healthBar;
    public TextMesh healthNumber;

    public override void Start()    
    {
        base.Start();
        healthBar = gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
        healthNumber = gameObject.GetComponentsInChildren<TextMesh>()[0];
        healthBar.enabled = true;
        healthBar.drawMode = SpriteDrawMode.Tiled;
        maxHealth = Health;

        Vector3 newScale = new Vector3();
        newScale.x = ((PixelsPerUnit * GetWidth())  - (Border.x + Border.z))/PixelsPerUnit;
        newScale.y = ((PixelsPerUnit * GetHeight()) - (Border.y + Border.w))/PixelsPerUnit;
        newScale.z = 1;
        healthBar.transform.localScale = newScale;

        healthNumber.transform.localPosition = new Vector3(GetWidth()/2, GetHeight()/2, healthNumber.transform.position.z);
    }

    public override void Update()
    {
        base.Update();

        timeSinceLastAttacked += Time.deltaTime;
        if(timeSinceLastAttacked > healthRegenDelay && Health < maxHealth && !Dead)
        {
            Health += healthRegenSpeed * Time.deltaTime;
            if(Health > maxHealth)
                Health = maxHealth;
        }

        //Update HealthBar
        healthBar.size = new Vector2((Health/maxHealth), healthBar.size.y);
        if(!Dead)
        {
            healthNumber.text = ((int)Health).ToString();
        }
        else
        {
            healthNumber.text = "X";
        }
    }

    private void Die()
    {
        Dead = true;
        //Destroy(gameObject);
    }

    public void Damage(float amount)
    {
        timeSinceLastAttacked = 0f;
        Health -= amount;
        if(Health <= 0)
        {
            Health = 0;
            Dead = true;
            //Die();
        }
    }

    public override void Tap(GameManager gm)
    {
        Player p = gm.getPlayer();
        if(!Dead)
        {
            Damage(p.AttackPower);
            if(!Dead)
            {
                p.Damage(AttackPower);
            }
            if(Dead)
            {
                p.upgradePoints++;
            }
        }
    }
}
