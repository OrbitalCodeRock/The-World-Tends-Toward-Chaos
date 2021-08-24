using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public float rotationSpeed;

    Vector3 difference_mouse;
    Vector3 difference_weapon;
    Transform player;
    float mouseAngle;
    float weaponAngle;
    float rotation_z;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.transform.parent.gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        difference_weapon = this.gameObject.transform.position - player.position;
        difference_weapon.Normalize();
        weaponAngle = Mathf.Atan2(difference_weapon.y, difference_weapon.x) * Mathf.Rad2Deg;
        difference_mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
        difference_mouse.Normalize();
        mouseAngle = Mathf.Atan2(difference_mouse.y, difference_mouse.x) * Mathf.Rad2Deg;
        rotation_z = mouseAngle - weaponAngle;
        this.gameObject.transform.RotateAround(player.position, Vector3.forward, rotation_z * Time.deltaTime * rotationSpeed);
    }
}
