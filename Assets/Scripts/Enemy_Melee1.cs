using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1 : MonoBehaviour
{
    public Transform player_t;
    public float avoidanceDistance;
    public float cornerAvoidance;
    public float speed = 40;
    public float punchSpeed;
    public float punchRange;

    bool retract = false;
    Transform Obstacle;
    Transform fist;
    Vector3 topRight;
    Vector3 bottomRight;
    Vector3 topLeft;
    Vector3 bottomLeft;
    SpriteRenderer rend;

    float playerTo_topRight;
    float playerTo_bottomRight;
    float playerTo_topLeft;
    float playerTo_bottomLeft;
    LayerMask obstacleMask;
    // Start is called before the first frame update
    void Start()
    {
        obstacleMask = LayerMask.GetMask("Obstacles");
        fist = this.transform.GetChild(0);
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 EnemyToPlayer = player_t.position - this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, EnemyToPlayer.normalized, EnemyToPlayer.magnitude, obstacleMask);
        if(hit.collider != null && (hit.transform.position - this.transform.position).sqrMagnitude > avoidanceDistance * avoidanceDistance)
        {
            
                
            Obstacle = hit.transform;
            rend = Obstacle.gameObject.GetComponent<SpriteRenderer>();
            //topRight = new Vector3(Obstacle.position.x + (Obstacle.lossyScale.x / 2) + this.transform.lossyScale.x * cornerAvoidance, Obstacle.position.y + (Obstacle.lossyScale.y / 2) + this.transform.lossyScale.y * cornerAvoidance, 0);
            //bottomRight = new Vector3(Obstacle.position.x + (Obstacle.lossyScale.x / 2) + this.transform.lossyScale.x * cornerAvoidance, Obstacle.position.y - (Obstacle.lossyScale.y / 2) - this.transform.lossyScale.y * cornerAvoidance, 0);
            //topLeft = new Vector3(Obstacle.position.x - (Obstacle.lossyScale.x / 2) - this.transform.lossyScale.x * cornerAvoidance, Obstacle.position.y + (Obstacle.lossyScale.y / 2) + this.transform.lossyScale.y * cornerAvoidance, 0);
            //bottomLeft = new Vector3(Obstacle.position.x - (Obstacle.lossyScale.x / 2) - this.transform.lossyScale.x * cornerAvoidance, Obstacle.position.y - (Obstacle.lossyScale.y / 2) - this.transform.lossyScale.y * cornerAvoidance, 0);
            topRight = new Vector3(rend.bounds.max.x + this.transform.lossyScale.x * cornerAvoidance, rend.bounds.max.y + this.transform.lossyScale.y * cornerAvoidance);
            bottomRight = new Vector3(rend.bounds.max.x + this.transform.lossyScale.x * cornerAvoidance, rend.bounds.min.y - this.transform.lossyScale.y * cornerAvoidance);
            topLeft = new Vector3(rend.bounds.min.x - this.transform.lossyScale.x * cornerAvoidance, rend.bounds.max.y + this.transform.lossyScale.y * cornerAvoidance);
            bottomLeft = new Vector3(rend.bounds.min.x - this.transform.lossyScale.x * cornerAvoidance, rend.bounds.min.y - this.transform.lossyScale.y * cornerAvoidance);
            playerTo_topRight = (topRight - player_t.position).sqrMagnitude;
            playerTo_topLeft = (topLeft - player_t.position).sqrMagnitude;
            playerTo_bottomRight = (bottomRight - player_t.position).sqrMagnitude;
            playerTo_bottomLeft = (bottomLeft - player_t.position).sqrMagnitude;
            
            float[] playerTo = {playerTo_topRight, playerTo_topLeft, playerTo_bottomRight, playerTo_bottomLeft};
            float smallestVal = playerTo_topRight;
            foreach(float f in playerTo)
            {
                if (f < smallestVal) smallestVal = f;
            }
            if(smallestVal == playerTo_topRight || smallestVal == playerTo_topLeft)
            {
                if(this.transform.position.y < topLeft.y)
                {

                    this.transform.position = Vector3.MoveTowards(this.transform.position, topLeft, Time.deltaTime * speed);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, topRight, Time.deltaTime * speed);
                }
            }
            else if(smallestVal == playerTo_topLeft)
            {
                if (this.transform.position.y < topRight.y)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, topRight, Time.deltaTime * speed);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, topLeft, Time.deltaTime * speed);
                }
            }
            else if (smallestVal == playerTo_bottomRight)
            {
                if (this.transform.position.y > bottomLeft.y)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, bottomLeft, Time.deltaTime * speed);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, bottomRight, Time.deltaTime * speed);
                }
            }
            else if (smallestVal == playerTo_bottomLeft)
            {
                if (this.transform.position.y > bottomRight.y)
                { 
                    this.transform.position = Vector3.MoveTowards(this.transform.position, bottomRight, Time.deltaTime * speed);
                }
                else
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, bottomLeft, Time.deltaTime * speed);
                }
            }
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, player_t.position, Time.deltaTime * speed);
            if ((player_t.position - this.transform.position).sqrMagnitude <= punchRange * punchRange && retract == false)
            {
                fist.position = Vector3.MoveTowards(fist.position, player_t.position, Time.deltaTime * punchSpeed);
            }
            else if (retract || (player_t.position - this.transform.position).sqrMagnitude > punchRange * punchRange)
            {
               if (fist.position == this.transform.position)
                {
                    retract = false;
                }
                fist.position = Vector3.MoveTowards(fist.position, this.transform.position, Time.deltaTime * punchSpeed);
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (fist.position != this.transform.position) { retract = true; }
    }
}
