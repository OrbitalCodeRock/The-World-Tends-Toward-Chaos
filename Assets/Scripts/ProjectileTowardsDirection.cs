using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTowardsDirection : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
