using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFoV : MonoBehaviour
{
    public float t = 0.01f;
    public float maxFOV = 70;
    public float quickEnd = 1;
    private float minFOV = 30;
    private Camera MainCam;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        MainCam = this.GetComponent<Camera>();
        MainCam.fieldOfView = minFOV;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, maxFOV, t);

        //if finish changing, disable this script and enable movement.
        
        if (MainCam.fieldOfView >= maxFOV-quickEnd){
            player.GetComponent<MoveCam>().enabled = true;
            MainCam.fieldOfView = maxFOV;
            //if we have Guide on hand, open it
            if (player.GetComponent<PressEOpenGuide>()){
                player.GetComponent<PressEOpenGuide>().guide.SetActive(true);
            }
            this.enabled = false;
        }
    }
}
