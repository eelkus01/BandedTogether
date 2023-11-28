using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashedCircleRenderer : MonoBehaviour
{
    public float radius = 5f; // Adjust the radius as needed
    public int pointCount = 36;

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount;

        for (int i = 0; i < pointCount + 10; i++)
        {
            float theta = (2f * Mathf.PI * i) / pointCount;
            float x = 0 + radius * Mathf.Cos(theta);
            float y = 0 + radius * Mathf.Sin(theta);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
}