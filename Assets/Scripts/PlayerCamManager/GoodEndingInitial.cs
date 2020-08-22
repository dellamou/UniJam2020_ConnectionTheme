using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Animation when starting the game, simulating wake up effect
public class GoodEndingInitial : MonoBehaviour
{
    private Vector3 finalPos;
    private float InitialHeight = -0.8f; 
    private float InitialZ = 0.8f; 
    private float accFactor = 2;
    private float raiseSpeed = 0.1f;
    private GameObject player;
    private MoveCam CamMoveScript;
    private Camera MainCam;
    // Start is called before the first frame update
    void Start()
    {
        finalPos = this.transform.localPosition;
        this.transform.LookAt(GameObject.Find("10105_Computer Keyboard_v1_L3").transform);
        this.transform.localPosition += new Vector3 (0.0f,InitialHeight,InitialZ);
        player = GameObject.Find("Player");
        CamMoveScript = player.GetComponent<MoveCam>();
        MainCam = player.GetComponent<Camera>();
        //disable camera controller at first and look from sky
        CamMoveScript.enabled = false;
        //change camera FOV as well
        this.GetComponent<ChangeFoV>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //head from forward to back;
        if ((this.transform.localPosition.z>=finalPos.z)){
            this.transform.localPosition += new Vector3 (0.0f,0.0f, -raiseSpeed*Time.deltaTime * accFactor);
        }
        //if the camera falls and reached the player, starting rotation
        if (this.transform.localPosition.y>=finalPos.y){
            if (this.transform.localRotation.x >= finalPos.x+0.2){
                this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * 30, new Vector3 (-10.0f,0.0f,0.0f));
            }
            //finished camera movement, enable control and disable this script
            else{
                //player.GetComponent<PressF1OpenGuide>().guide.SetActive(true);
                this.transform.localPosition = finalPos;
                this.enabled = false;
            }
        }

        //camera raise to the screen
        else {
            this.transform.localPosition += new Vector3 (0.0f,+raiseSpeed*Time.deltaTime * accFactor,0.0f);
        }
    }
}
