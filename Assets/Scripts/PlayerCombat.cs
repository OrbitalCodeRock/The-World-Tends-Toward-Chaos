using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject projectile;
    public Transform weapon;
    public float fireDelay;

    private KeyCode fire_key;

    // Start is called before the first frame update
    void Start()
    {
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
}
