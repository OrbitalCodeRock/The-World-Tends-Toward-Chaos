using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public float damage;
    public float projectileSpeed;
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
        if(health > maxHealth) { health = maxHealth; }
        healthBar.sizeDelta = new Vector2(Mathf.Clamp(healthBarSize_x * health / maxHealth, 0, healthBarSize_x), healthBar.sizeDelta.y);
        if (health <= 0) { health = 0; onDeath(); }
        if (Input.GetKey(fire_key)){
            if (Time.fixedTimeAsDouble - LastShot > fireDelay)
            {
                GameObject shot = Instantiate(projectile, weapon.position, Quaternion.identity);
                shot.tag = "PlayerBullet";
                shot.AddComponent<ProjectileTowardsMouse>();
                shot.GetComponent<ProjectileTowardsMouse>().damage = damage;
                shot.GetComponent<ProjectileTowardsMouse>().speed = projectileSpeed;
                LastShot = Time.fixedTimeAsDouble;
            }
         
        }
    }
    void onDeath()
    {
        SceneManager.LoadScene(2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Melee1"))
        {
            if(Time.fixedTimeAsDouble - collision.gameObject.GetComponent<Enemy_Melee1>().lastPunch > collision.gameObject.GetComponent<Enemy_Melee1>().punchDelay)
            {
                collision.gameObject.GetComponent<Enemy_Melee1>().lastPunch = Time.fixedTimeAsDouble;
                health -= collision.gameObject.GetComponent<Enemy_Melee1>().damage;
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= collision.gameObject.GetComponent<ProjectileTowardsDirection>().damage;
        }
    }
    IEnumerator gainPower(int powerUp, float duration)
    {
        switch(powerUp){
            case (1):
                fireDelay /= 2;
                yield return new WaitForSecondsRealtime(duration);
                fireDelay *= 2;
                break;
            case (2):
                damage *= 2;
                yield return new WaitForSecondsRealtime(duration);
                damage /= 2;
                break;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPickup"))
        {
            health += maxHealth / 2;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("FireRatePowerUp"))
        {
            StartCoroutine(gainPower(1, 5f));
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("DamagePowerUp"))
        {
            StartCoroutine(gainPower(2, 5f));
            Destroy(collision.gameObject);
        }
    }
}
