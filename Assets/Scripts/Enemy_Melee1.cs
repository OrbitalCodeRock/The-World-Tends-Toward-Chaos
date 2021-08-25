using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1 : MonoBehaviour
{
    public Transform player_t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 EnemyToPlayer = player_t.position - this.transform.position;
        //Debug.DrawRay(this.transform.position, EnemyToPlayer.normalized, EnemyToPlayer.magnitude);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, EnemyToPlayer.normalized, EnemyToPlayer.magnitude);
        if(hit.collider != null)
        {
            print(hit.transform.gameObject.name);
        }
    }

}
