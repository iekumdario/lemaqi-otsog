using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageByColission : MonoBehaviour {

    public int lifePoints = 15;
    public Text lifeText;

    void Start()
    {
        lifeText.text = "Shield: " + lifePoints;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Player")
        {
            return;
        }

        lifePoints--;
        lifeText.text = "Shield: " + lifePoints;

        if (this.lifePoints <= 0) {
            Destroy(gameObject);
        }
        
    }
}
