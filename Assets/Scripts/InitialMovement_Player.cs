using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialMovement_Player : MonoBehaviour
{
    public float distancePerSecond = 1f;

    public float maxStamina = 10f;
    public float stamina = 1f;
    public RectTransform staminaBar;

    public float dashMultiplier = 1.5f;
    public float regenMagnitude;
    public float depletionMagnitude;
    public Transform player_t;
    
    KeyCode up_key;
    KeyCode right_key;
    KeyCode down_key;
    KeyCode left_key;
    KeyCode dash_key;

    private float regularSpeed;
    private bool canDash = true;
    private float staminaBarSize_x;
    // Start is called before the first frame update
    void Start()
    {
        //player_t = this.gameObject.GetComponent<Rigidbody2D>();
        regularSpeed = distancePerSecond;
        staminaBarSize_x = staminaBar.sizeDelta.x;
        player_t = this.gameObject.transform;
        up_key = this.gameObject.GetComponent<KeyBindings>().up_key;
        right_key = this.gameObject.GetComponent<KeyBindings>().right_key;
        down_key = this.gameObject.GetComponent<KeyBindings>().down_key;
        left_key = this.gameObject.GetComponent<KeyBindings>().left_key;
        dash_key = this.gameObject.GetComponent<KeyBindings>().dash_key;

    }
    IEnumerator regainStamina(float regenRate, float magnitude)
    {
        while(stamina < maxStamina)
        {
            stamina += magnitude;
            staminaBar.sizeDelta = new Vector2(Mathf.Clamp(staminaBarSize_x * stamina / maxStamina, 0, staminaBarSize_x), staminaBar.sizeDelta.y);
            yield return new WaitForSeconds(regenRate);
        }
        canDash = true;
    }
    IEnumerator Dash(float depletionRate, float magnitude)
    {
        canDash = false;
        distancePerSecond *= dashMultiplier;
        while(stamina > 0)
        {
            stamina -= magnitude;
            staminaBar.sizeDelta = new Vector2(Mathf.Clamp(staminaBarSize_x * stamina / maxStamina, 0, staminaBarSize_x), staminaBar.sizeDelta.y);
            yield return new WaitForSeconds(depletionRate);
        }
        distancePerSecond = regularSpeed;
        StartCoroutine(regainStamina(1, regenMagnitude));

    }
    // Update is called once per frame
    void Update()
    {
        if(stamina > maxStamina) { stamina = maxStamina; }
        if(stamina < 0) { stamina = 0; }
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
        if (Input.GetKey(dash_key) && canDash)
        {
            if(stamina == maxStamina) { StartCoroutine(Dash(1,depletionMagnitude));} 
        }
    }
    void FixedUpdate()
    {
       /* if (Input.GetKey(up_key))
        {
            player_t.AddForce(Vector2.up * Time.deltaTime * distancePerSecond, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(down_key))
        {
            player_t.AddForce(Vector2.down * Time.deltaTime * distancePerSecond, ForceMode2D.Impulse);
        }
        if (Input.GetKey(right_key))
        {
            player_t.AddForce(Vector2.right * Time.deltaTime * distancePerSecond, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(left_key))
        {
            player_t.AddForce(Vector2.left * Time.deltaTime * distancePerSecond, ForceMode2D.Impulse);
        } */
    }
}
