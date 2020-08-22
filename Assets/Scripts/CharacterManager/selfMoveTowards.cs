using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class selfMoveTowards : MonoBehaviour
{
    public float walkingSpeed = 1.5f;
    public Vector3 target;

    private GameObject model;
    private GameObject BeautifyUi;
    public Text MidNotice;
    public Text TopNotice;

    private IEnumerator coroutine;
    private bool check = false;

    public bool trueEnd;

    private void Start()
    {
        if (trueEnd)
        {
            this.moveTowards(target);
        }
    }

    public void moveTowards(Vector3 target)
    {
        this.target = target;
        coroutine = WaitSwitchScene(1.5f);
        StartCoroutine(coroutine);
        model = GameObject.Find("PlayerModel");
        if (!trueEnd)
        {
            BeautifyUi = GameObject.Find("BeautifyUi");
            BeautifyUi.SetActive(false);
        }
    }

    void Update()
    {
        //not in position yet
        if (!check){
            transform.position = Vector3.MoveTowards(transform.position, target, walkingSpeed * Time.deltaTime);
        }
        //move to next scene
        else{
            if (!trueEnd)
            {
                TopNotice.text = "Finally I find my friend but Why everything is Up side Down... Am I Dreaming? ";
                MidNotice.text = "Press Any Key to Continue";
                if (Input.anyKey)
                {
                    MidNotice.text = "";
                    TopNotice.text = "";
                    // load different scene according to the state
                    if (GameObject.FindWithTag("Player").GetComponent<ItemCollectionManager>().photo)
                    {
                        SceneManager.LoadScene("TrueEnd");
                    }
                    else
                    {
                        SceneManager.LoadScene("GoodEnd");
                    }
                }
            }
            else
            {
                TopNotice.text = "Now I realised that when I'm looking for him, he is looking for me too.";
                MidNotice.text = "Press Any Key to Continue";
                if (Input.anyKey)
                {
                    MidNotice.text = "";
                    TopNotice.text = "";
                    SceneManager.LoadScene("TrueEndDisplay");
                }
            }
        }
    }

    private IEnumerator WaitSwitchScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // save the record
        if (!trueEnd)
        {
            // if collected photo, unlock true end
            if (GameObject.FindWithTag("Player").GetComponent<ItemCollectionManager>().photo)
            {
                this.GetComponent<recordSaver>().SaveJson("true");
            }
            else
            {
                this.GetComponent<recordSaver>().SaveJson("good");
            }
        }
        check = true;
        model.GetComponent<Animation>().Play("idle");
    }
}
