using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class selfMoveTowards : MonoBehaviour
{
    public float walkingSpeed = 1.5f;
    private Vector3 target;

    private GameObject model;
    private GameObject BeautifyUi;
    public Text MidNotice;
    public Text TopNotice;

    private IEnumerator coroutine;
    private bool check = false;

    public void moveTowards(Vector3 target)
    {
        this.target = target;
        coroutine = WaitSwitchScene(1.5f);
        StartCoroutine(coroutine);
        model = GameObject.Find("PlayerModel");
        BeautifyUi = GameObject.Find("BeautifyUi");
        BeautifyUi.SetActive(false);
    }

    void Update()
    {
        //not in position yet
        if (!check){
            transform.position = Vector3.MoveTowards(transform.position, target, walkingSpeed * Time.deltaTime);
        }
        //move to next scene
        else{
            TopNotice.text = "Finally I find my friend but Why everything is Up side Down... Am I Dreaming? ";
            MidNotice.text = "Press Any Key to Continue";
            if (Input.anyKey){
                MidNotice.text = "";
                TopNotice.text = "";
                SceneManager.LoadScene("GoodEnd");
            }
        }
    }

    private IEnumerator WaitSwitchScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.GetComponent<recordSaver>().SaveJson("good");
        check = true;
        model.GetComponent<Animation>().Play("idle");
    }
}
