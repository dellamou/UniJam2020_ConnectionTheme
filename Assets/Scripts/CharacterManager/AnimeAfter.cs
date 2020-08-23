using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeAfter : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private Animator anim1;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {   
        anim1.enabled = false;
        coroutine = waitForSeconds(waitTime);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator waitForSeconds(float WaitTime) {
        yield return new WaitForSeconds(WaitTime);
        anim1.enabled = true;
    }
}
