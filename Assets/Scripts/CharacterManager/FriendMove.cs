﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class FriendMove : MonoBehaviour
{
    public float walkingSpeed = 1.5f;
    private Vector3 target;

    private IEnumerator coroutine;

    public void moveTowards(Vector3 target)
    {
        this.target = target;
        coroutine = WaitSwitchScene(1.5f);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, walkingSpeed * Time.deltaTime);
    }

    private IEnumerator WaitSwitchScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.GetComponent<recordSaver>().SaveJson("true");
        SceneManager.LoadScene("TrueEndDisplay");
    }
}
