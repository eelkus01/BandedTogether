using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    Transform healthBarTransform;
    // Start is called before the first frame update
    void Start()
    {
        // Get the Image component on this GameObject
        healthBarTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void UpdateHealthBar(float health, float maxHealth)
    {
        // Calculate the scale factor based on health and maxHealth
        float scaleFactor = Mathf.Clamp01(health / maxHealth);

        // Set the local scale of the health bar GameObject (adjust the x-axis as needed)
        healthBarTransform.localScale = new Vector3(scaleFactor, 1f, 1f);
    }

}
