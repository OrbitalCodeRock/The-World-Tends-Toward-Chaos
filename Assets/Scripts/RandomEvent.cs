using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvent : MonoBehaviour
{
    public float eventFrequency;
    public double lastEvent;

    public GameObject[] Enemies;
    public GameObject exclamationPoint;
    public GameObject eventCanvas;

    Vector3[] fourCourners = new Vector3[4];
    Transform player_t;
    GameObject EnemyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        player_t = GameObject.FindGameObjectWithTag("Player").transform;
        eventCanvas.GetComponent<RectTransform>().GetWorldCorners(fourCourners);
        StartCoroutine(randomEventGenerator());
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
    IEnumerator randomEventGenerator()
    {
        while (true)
        {
            StartCoroutine(StartEvent(1));
            yield return new WaitForSeconds(eventFrequency);
        }
    }
    IEnumerator StartEvent(int i)
    {
        switch (i)
        {
            case (1):
                Vector3 spawnPos;
                EnemyToSpawn = Enemies[Random.Range(0, Enemies.Length)];
                while (true)
                {
                    spawnPos = new Vector3(Random.Range(fourCourners[0].x, fourCourners[2].x), Random.Range(fourCourners[0].y, fourCourners[2].y));
                    Collider2D wallPerhaps = Physics2D.OverlapCircle(spawnPos, EnemyToSpawn.transform.lossyScale.x);
                    if(wallPerhaps == null) { break; }
                }
                GameObject warningSign = Instantiate(exclamationPoint, spawnPos, Quaternion.identity, eventCanvas.transform);
                StartCoroutine(BlinkForSeconds(2, 0.5f, warningSign.GetComponentsInChildren<Image>()));
                yield return new WaitForSeconds(2);
                Destroy(warningSign);
                Instantiate(EnemyToSpawn, spawnPos, Quaternion.identity);
                break;
            case (2):
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
