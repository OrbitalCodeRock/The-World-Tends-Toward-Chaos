using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialMovement_Player : MonoBehaviour
{
    public float distancePerSecond = 1f;
    public Transform player_t;
    
    KeyCode up_key;
    KeyCode right_key;
    KeyCode down_key;
    KeyCode left_key;
    KeyCode fire_key;

    // Start is called before the first frame update
    void Start()
    {
        player_t = this.gameObject.transform;
        up_key = this.gameObject.GetComponent<KeyBindings>().up_key;
        right_key = this.gameObject.GetComponent<KeyBindings>().right_key;
        down_key = this.gameObject.GetComponent<KeyBindings>().down_key;
        left_key = this.gameObject.GetComponent<KeyBindings>().left_key;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up_key))
        {
            player_t.Translate(Vector3.up * Time.deltaTime * distancePerSecond);
        }
        else if (Input.GetKey(down_key))
        {
            player_t.Translate(Vector3.down * Time.deltaTime * distancePerSecond);
        }
        if (Input.GetKey(right_key))
        {
            player_t.Translate(Vector3.right * Time.deltaTime * distancePerSecond);
        }
        else if (Input.GetKey(left_key))
        {
            player_t.Translate(Vector3.left * Time.deltaTime * distancePerSecond);
        }
    }
}
