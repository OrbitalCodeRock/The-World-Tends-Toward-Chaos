using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public RectTransform healthBar;

    public GameObject projectile;
    public Transform weapon;
    public float fireDelay;

    private float healthBarSize_x;
    private KeyCode fire_key;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSize_x = healthBar.sizeDelta.x;
        health = maxHealth;
        fire_key = this.gameObject.GetComponent<KeyBindings>().fire_key;
    }
  
    // Update is called once per frame
    private double LastShot = 0;
    void Update()
    {
        if (Input.GetKey(fire_key)){
            if (Time.fixedUnscaledTimeAsDouble - LastShot > fireDelay)
            {
                Instantiate(projectile, weapon.position, Quaternion.identity);
                LastShot = Time.fixedUnscaledTimeAsDouble;
            }
         
        }
    }
    void onDeath()
    {
        print("You Died.");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Melee1"))
        {
            if(Time.fixedUnscaledTimeAsDouble - collision.gameObject.GetComponent<Enemy_Melee1>().lastPunch > collision.gameObject.GetComponent<Enemy_Melee1>().punchDelay)
            {
                collision.gameObject.GetComponent<Enemy_Melee1>().lastPunch = Time.fixedUnscaledTimeAsDouble;
                health -= collision.gameObject.GetComponent<Enemy_Melee1>().damage;
                healthBar.sizeDelta = new Vector2(Mathf.Clamp(healthBarSize_x * health / maxHealth, 0, healthBarSize_x), healthBar.sizeDelta.y);
                if (health <= 0){health = 0; onDeath(); }
            }
        }
    }
}
