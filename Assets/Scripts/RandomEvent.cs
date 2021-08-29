using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvent : MonoBehaviour
{
    public float eventFrequency;
    public double lastEvent;

    public GameObject[] Enemies;
    public GameObject[] warningObjects;
    public GameObject eventCanvas;
    public GameObject gameSpeedDisplay;

    Vector3[] fourCourners = new Vector3[4];
    Transform player_t;
    GameObject EnemyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        player_t = GameObject.FindGameObjectWithTag("Player").transform;
        eventCanvas.GetComponent<RectTransform>().GetWorldCorners(fourCourners);
        StartCoroutine(randomEventGenerator());
        StartCoroutine(increaseEventFrequency());
    }
    IEnumerator BlinkForSeconds(float seconds, float flickerRate, Image[] warningImage)
    {
        float s = 0;
        while (s < seconds)
        {
            s += flickerRate;
            for (int i = 0; i < warningImage.Length; i++) { warningImage[i].enabled = !warningImage[i].enabled; }
            yield return new WaitForSeconds(flickerRate);
        }
    }
    IEnumerator increaseEventFrequency()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(30f);
            eventFrequency *= 0.85f;
        }
    }
    IEnumerator randomEventGenerator()
    {
        
        while (true)
        {
            StartCoroutine(StartEvent(1));
            StartCoroutine(StartEvent(2));
            if(Random.Range(0f,100f) < 10 && Time.timeScale == 1)
            {
                StartCoroutine(StartEvent(3));
            }
            yield return new WaitForSeconds(eventFrequency);
        }
    }
    int[] enemyProbabilities = new int[] { 20, 50 };
    IEnumerator StartEvent(int i)
    {
        Vector3 spawnPos;
        GameObject warningSign;
        float explosionDamage;
        switch (i)
        {
            case (1):
                int choice = Random.Range(0, 100);
                if(choice < enemyProbabilities[0]) { EnemyToSpawn = Enemies[2]; }
                else if(choice >= enemyProbabilities[0] && choice < enemyProbabilities[1]) { EnemyToSpawn = Enemies[1]; }
                else if (choice >= enemyProbabilities[1]) { EnemyToSpawn = Enemies[0]; }
                while (true)
                {
                    spawnPos = new Vector3(Random.Range(fourCourners[0].x, fourCourners[2].x), Random.Range(fourCourners[0].y, fourCourners[2].y));
                    Collider2D wallPerhaps = Physics2D.OverlapCircle(spawnPos, EnemyToSpawn.transform.lossyScale.x);
                    if(wallPerhaps == null) { break; }
                }
                warningSign = Instantiate(warningObjects[0], spawnPos, Quaternion.identity, eventCanvas.transform);
                StartCoroutine(BlinkForSeconds(2, 0.5f, warningSign.GetComponentsInChildren<Image>()));
                yield return new WaitForSeconds(2);
                Destroy(warningSign);
                Instantiate(EnemyToSpawn, spawnPos, Quaternion.identity);
                break;


            case (2):
                explosionDamage = 15f;
                float spawnRadius = 3f;
                float randomAngle;
                while (true)
                {
                    randomAngle = Random.Range(0, Mathf.PI * 2);
                    spawnPos = new Vector3(player_t.position.x + Mathf.Cos(randomAngle) * spawnRadius, player_t.position.y + Mathf.Sin(randomAngle) * spawnRadius);
                    if(spawnPos.x > fourCourners[0].x && spawnPos.x < fourCourners[2].x && spawnPos.y > fourCourners[0].y && spawnPos.y < fourCourners[2].y) { break; }
                }
                warningSign = Instantiate(warningObjects[1], spawnPos, Quaternion.identity, eventCanvas.transform);
                Image[] warningImages = warningSign.GetComponentsInChildren<Image>();
                StartCoroutine(BlinkForSeconds(1f, 0.5f, warningImages));
                yield return new WaitForSeconds(1f);
                warningImages[0].enabled = true;
                warningImages[1].enabled = false;
                yield return new WaitForSeconds(0.2f);
                Collider2D playerPerhaps = Physics2D.OverlapCircle(spawnPos, 1.5f);
                if (playerPerhaps != null && playerPerhaps.gameObject.CompareTag("Player")) { player_t.GetComponent<PlayerCombat>().health -= explosionDamage; }
                Destroy(warningSign);
                break;


            case (3):
                Text displayText = gameSpeedDisplay.GetComponentInChildren<Text>();
                Time.timeScale *= 1.5f;
                displayText.text = string.Format("{0:N2}x", Time.timeScale);
                gameSpeedDisplay.SetActive(true);
                yield return new WaitForSecondsRealtime(5f); 
                Time.timeScale = 1;
                displayText.text = string.Format("{0:N2}x", Time.timeScale);
                yield return new WaitForSeconds(3f);
                gameSpeedDisplay.SetActive(false);
                break;

            case (4):
                PlayerCombat pc = player_t.GetComponent<PlayerCombat>();
                float previousFireDelay = pc.fireDelay;
                pc.fireDelay /= 2;
                yield return new WaitForSecondsRealtime(5f);
                pc.fireDelay = previousFireDelay;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
