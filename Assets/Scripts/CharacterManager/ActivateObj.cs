using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateObj : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField]
    private float waitTime = 1f;
    private GameObject EyeLidTop;
    private GameObject EyeLidBtm;
    [SerializeField]
    private GameObject model;
    public Animator anim1;
    public Animator anim2;
    // Start is called before the first frame update
    void Start()
    {   
        if (model.activeInHierarchy)
        {
            model.SetActive(false);
        }
        coroutine = waitForSeconds(waitTime);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator waitForSeconds(float WaitTime) {
        yield return new WaitForSeconds(WaitTime);

        EyeLidBtm = GameObject.Find("EyeLidBtm");
        EyeLidTop = GameObject.Find("EyeLidTop");

        model.SetActive(true);
        anim1.enabled = false;
        anim2.enabled = false;
    }
}
