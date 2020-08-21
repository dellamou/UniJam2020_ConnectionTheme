using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private float destHeight;

    private IEnumerator coroutine;
    private bool CR_running = false;

    private bool renderedText = false;

    public float health = 100f;
    public float fullHealth = 100f;
    public float healthTakePerMS = 1f;
    public float healthRecoverPerMS = 2f;

    public Text TopScreenNotice;

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
            coroutine = WaitThenTakeHealth(0.1f);
            StartCoroutine(coroutine);
            if (!renderedText) // render text only once
            {
                TopScreenNotice.text = "I'm lossing the connection with my friend! I have to get closer to him!";
                renderedText = true;
            }
        } else {
            renderedText = false; // reset
            coroutine = WaitThenRecoverHealth(0.1f);
            StartCoroutine(coroutine);
        }

        lineRenderer.startColor = currentColor;
        lineRenderer.startWidth = normalLineWidth * (1-distance/furthestDistance);
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, new Vector3(dest.position.x, destHeight, dest.position.z));
    }

    private IEnumerator WaitThenTakeHealth(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        health -= healthTakePerMS;
        if (health <= 0)
        {
            SceneManager.LoadScene("BadEnd");
        }
    }

    private IEnumerator WaitThenRecoverHealth(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (health <= fullHealth - healthRecoverPerMS)
        {
            health += healthRecoverPerMS;
        }
        else if (health < fullHealth && health > fullHealth - healthRecoverPerMS)
        {
            health = fullHealth;
        }
    }
}
