using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
    //public Done_Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Start()
    {
        //var bullseye = GameObject.FindGameObjectsWithTag("Boundary")[0];
        //this.shotSpawn.LookAt(bullseye.transform);
    }

    void Update()
    {
        if (Time.time > nextFire) //delay
        {
            if (Input.touchSupported && Input.touchCount == 1)
            {
                Vector3 shotDirection = new Vector3(240 - Input.touches[0].position.y, 0.0f, Input.touches[0].position.x);
                Shoot(shotDirection);
            }
            else if (Input.GetMouseButton(0))
            {
                //Debug.Log(Input.mousePosition);
                Vector3 shotDirection = new Vector3(240-Input.mousePosition.y,  0.0f, Input.mousePosition.x);
                Debug.Log(shotDirection);
                Shoot(shotDirection);
            }

        }
     
    }

    void Shoot(Vector3 shotDirection)
    {
        nextFire = Time.time + fireRate;
        shotSpawn.LookAt(shotDirection);
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }
}
