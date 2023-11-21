using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumAttack : MonoBehaviour
{
    private Transform objectTransform;

    // Start is called before the first frame update
    void Start()
    {
        objectTransform = transform;
        StartCoroutine(AnimateScale());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AnimateScale()
    {
        // Scale up from Vector3.zero to Vector3(5, 4, 1) over the course of 0.5 seconds
        float elapsedTime = 0f;
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = new Vector3(5f, 4f, 1f);

        while (elapsedTime < 0.5f)
        {
            objectTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectTransform.localScale = targetScale; // Ensure it's exactly at the target scale

        // Wait for 0.25 seconds
        yield return new WaitForSeconds(0.25f);

        // Instantly scale back to zero
        objectTransform.localScale = Vector3.zero;

        // Destroy the object
        Destroy(gameObject);
    }
}
