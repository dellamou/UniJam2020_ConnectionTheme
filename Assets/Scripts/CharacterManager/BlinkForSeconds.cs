using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkForSeconds : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private Animator anim1;
    [SerializeField]
    private Animator anim2;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = waitForSeconds(waitTime);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator waitForSeconds(float WaitTime) {
        yield return new WaitForSeconds(WaitTime);
        anim1.enabled = false;
        anim2.enabled = false;
    }
}
