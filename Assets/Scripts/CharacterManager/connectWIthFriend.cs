using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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

    public float stopTime = 10f;

    private LineRenderer lineRenderer;
    private float distance;

    private float destHeight;

    private IEnumerator coroutine;
    private bool CR_running = false;

    // Start is called before the first frame update
    void Start()
    {
        destHeight = origin.position.y;
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
            coroutine = WaitThenStopGame(stopTime);
            StartCoroutine(coroutine);
        } else if (CR_running && distance <= warningDistance) {
            Debug.Log("??");
            StopCoroutine(coroutine);
            CR_running = false;
        }

        lineRenderer.startColor = currentColor;
        lineRenderer.startWidth = normalLineWidth * (1-distance/furthestDistance);
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, new Vector3(dest.position.x, destHeight, dest.position.z));
    }

    private IEnumerator WaitThenStopGame(float waitTime)
    {
        CR_running = true;
        yield return new WaitForSeconds(waitTime);
        if (CR_running)
        {
            SceneManager.LoadScene("BadEnd");
            CR_running = false;
        }
        CR_running = false;
    }
}
