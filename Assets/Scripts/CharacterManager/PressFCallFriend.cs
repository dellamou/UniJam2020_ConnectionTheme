using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//the origin must go to the checkPoint and interact with checkPoint, then come back to the dest.
public class PressFCallFriend : MonoBehaviour
{
    private float distance;
    public GameObject origin;
    public GameObject checkPoint;
    public GameObject dest;
    private bool check = false;
    public float interactDistance = 1f;
    public Text MidScreenNotice;
    public Text TopScreenNotice;

    private GameObject skype;
    // Start is called before the first frame update
    void Start()
    {
        checkPoint.GetComponent<PressFToInteract>().enabled = true;
        dest.GetComponent<PressFToInteract>().enabled = false;
        skype = GameObject.Find("SkypeScreen");
        skype.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        //if check point already checked.
        if (check){
            distance = Vector3.Distance(origin.transform.position, dest.transform.position);
            if (distance < interactDistance) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    SceneManager.LoadScene("GoodEndDisplay");
                }
            }
        }
        else{
            distance = Vector3.Distance(origin.transform.position, checkPoint.transform.position);
            if (distance < interactDistance) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    check = true;
                    MidScreenNotice.text = "";
                    TopScreenNotice.text = "Maybe I should give him a Call via COMPUTER on the desk.";
                    checkPoint.GetComponent<PressFToInteract>().enabled = false;
                    dest.GetComponent<PressFToInteract>().enabled = true;
                    skype.SetActive(true);
                }
            }
        }
    }
}
