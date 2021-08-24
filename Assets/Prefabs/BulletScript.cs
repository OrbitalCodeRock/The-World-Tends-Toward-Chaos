using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    Vector3 heading;
    float distance;
    Vector3 direction;
    Vector3 MousePos;
    // Start is called before the first frame update
    void Start()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        direction = (MousePos - this.gameObject.transform.position).normalized;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
