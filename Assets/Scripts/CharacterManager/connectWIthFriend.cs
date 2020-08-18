using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectWIthFriend : MonoBehaviour
{
    public Transform origin;
    public Transform dest;

    public float normalLineWidth = 0.05f;

    public float furthestDistance = 10f;
    public float warningDistance = 8f;

    public Color normalColor = new Color(213, 233, 255, 0.5f);
    public Color warningColor = Color.red;

    public float lineDrawSpeed = 6f;

    private LineRenderer lineRenderer;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        distance = Vector3.Distance(origin.position, dest.position);

        Color currentColor = normalColor;
        if (distance > warningDistance)
        {
            currentColor = warningColor;
        }

        lineRenderer.startColor = currentColor;
        lineRenderer.startWidth = normalLineWidth * (1-distance/furthestDistance);
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, dest.position);
    }
}
