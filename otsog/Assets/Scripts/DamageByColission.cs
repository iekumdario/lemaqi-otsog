using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByColission : MonoBehaviour {

    public int lifePoints = 15;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Player")
        {
            return;
        }

        lifePoints--;

        if (this.lifePoints <= 0) {
            Destroy(gameObject);
        }
        
    }
}
