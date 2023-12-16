using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClean : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy the GameObject after 2 seconds
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
