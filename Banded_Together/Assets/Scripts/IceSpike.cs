using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    public float speed = 15.0f; // Speed of the ice spike

    // Start is called before the first frame update
    void Start()
    {
        // Optional: Any initialization specific to the IceSpike
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
