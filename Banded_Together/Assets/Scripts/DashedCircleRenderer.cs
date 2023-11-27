using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashedCircleRenderer : MonoBehaviour
{
    public int dashCount = 36; // Number of dashes
    public float radius = 2f;  // Radius of the circle

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = dashCount * 2;

        float deltaTheta = (2f * Mathf.PI) / dashCount;
        float theta = 0f;

        for (int i = 0; i < dashCount * 2; i += 2)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            lineRenderer.SetPosition(i + 1, new Vector3(x, y, 0f));

            theta += deltaTheta;
        }
    }
}