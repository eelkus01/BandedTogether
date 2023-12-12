using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spellblast"))
        {
            //add particle effect and then destroy
            other.GetComponent<ImpactParticles>().CreateParticles();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("IceSpike"))
        {
            // other.GetComponent<ImpactParticles>().CreateParticles();
            Destroy(other.gameObject);
        }
    }
}
