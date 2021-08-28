using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public float healthBarSize_x;

    public Transform maxHealthBar;
    public Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSize_x = healthBar.localScale.x;
        health = maxHealth;
        healthBar.localScale = new Vector3(healthBarSize_x * health/maxHealth, healthBar.localScale.y);
    }

    // Update is called once per frame
    void OnDeath()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= collision.gameObject.GetComponent<BulletScript>().damage;
            if (health < 0){health = 0; OnDeath(); }
            healthBar.localScale = new Vector3(healthBarSize_x * health / maxHealth, healthBar.localScale.y);

        }
    }
}
