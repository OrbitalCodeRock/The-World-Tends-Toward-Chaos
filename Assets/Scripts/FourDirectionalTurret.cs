using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirectionalTurret : MonoBehaviour
{
    public GameObject projectile;
    public Transform[] projectileSpawns;
    public float volleyDelay;
    public float projectileSpacing;
    public int volleySize;
    public float damage;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(firePattern());
    }
    IEnumerator firePattern()
    {
        float rotationAngle = 45f;
        while (true)
        {
            StartCoroutine(fireTurrets(projectileSpacing, volleySize));
            yield return new WaitForSeconds(volleyDelay);
            this.transform.Rotate(new Vector3(0, 0, rotationAngle));
            rotationAngle *= -1;
        }

    }
    IEnumerator fireTurrets(float delay, int repetitions)
    {
        for(int i = 0; i < projectileSpawns.Length; i++)
        {
            GameObject shot = Instantiate(projectile, projectileSpawns[i].position, Quaternion.identity);
            shot.AddComponent<ProjectileTowardsDirection>();
            shot.tag = "Bullet";
            ProjectileTowardsDirection ptd = shot.GetComponent<ProjectileTowardsDirection>();
            ptd.damage = damage;
            ptd.speed = bulletSpeed;
            ptd.direction = projectileSpawns[i].up;
        }
        yield return new WaitForSeconds(delay);
        if (repetitions > 0) { StartCoroutine(fireTurrets(delay, repetitions - 1)); }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
