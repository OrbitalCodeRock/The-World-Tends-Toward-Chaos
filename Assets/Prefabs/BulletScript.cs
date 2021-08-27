using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int damage;
    Vector3 direction;
    Vector3 MousePos;
    // Start is called before the first frame update
    void Start()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        direction = (MousePos - this.gameObject.transform.position).normalized;
        //this.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime * speed, ForceMode2D.Impulse);
        transform.Translate(direction * Time.deltaTime * speed);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
