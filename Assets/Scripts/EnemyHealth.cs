using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public GameObject[] powerUps;

    public RectTransform healthBar;
    public Canvas statusCanvas;
    private float healthBarSize_x;
    // Start is called before the first frame update
    void Start()
    {

        statusCanvas = this.gameObject.GetComponentsInChildren<Canvas>()[0];
        statusCanvas.worldCamera = Camera.main;
        healthBar = this.gameObject.GetComponentsInChildren<RectTransform>()[2];
        healthBarSize_x = healthBar.sizeDelta.x;
        health = maxHealth;
    }

    // Update is called once per frame
    void OnDeath()
    {
        if(Random.Range(0,101) < 20)
        {
            Instantiate(powerUps[Random.Range(0, 3)], this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= collision.gameObject.GetComponent<ProjectileTowardsMouse>().damage;
            if (health <= 0) { health = 0; OnDeath(); }
            healthBar.sizeDelta = new Vector2(Mathf.Clamp(healthBarSize_x * health / maxHealth, 0, healthBarSize_x), healthBar.sizeDelta.y);

        }
    }
}
