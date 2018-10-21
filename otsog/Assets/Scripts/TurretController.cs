using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;
    private GameObject[] enemies;

    private float nextFire;
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0) {
                shotSpawn.LookAt(enemies[0].transform);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
