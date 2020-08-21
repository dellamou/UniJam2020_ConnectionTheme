using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Animation when starting the game, Camera falls from above
public class UseTorch : MonoBehaviour
{
    private Vector3 finalPos;
    public float InitialHeight = -5; 
    public float accFactor = 3;
    public float RaiseSpeed = 1;
    private GameObject PhysicalTorch;
    private bool useTorch=false;
    private bool TorchActive = false;
    // Start is called before the first frame update
    void Start()
    {
        finalPos = this.transform.localPosition;
        this.transform.localPosition = finalPos + new Vector3 (0.0f,InitialHeight,0.0f);
        PhysicalTorch = GameObject.Find("PhysicalTorch");
        PhysicalTorch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            useTorch = !useTorch;
        }
        //if pressed button and torch not active, turn it on
        if ((!TorchActive) && useTorch){
            PhysicalTorch.SetActive(true);
            //if the torch finished move and reached the player
            if (this.transform.localPosition.y>=finalPos.y){
                this.transform.localPosition = finalPos;
                TorchActive = true;
                useTorch = false;
                PhysicalTorch.SetActive(true);
                }
            //object mode towards final position
            else {
                this.transform.localPosition += new Vector3 (0.0f,RaiseSpeed*Time.deltaTime * accFactor,0.0f);
            }
        }
        //if pressed button and torch is active now, turn it off
        else if ((TorchActive) && useTorch){
            this.transform.localPosition += new Vector3 (0.0f,-RaiseSpeed*Time.deltaTime * accFactor,0.0f);
            //finished turn off animation
            if (this.transform.localPosition.y<=InitialHeight){
                TorchActive = false;
                useTorch = false;
                PhysicalTorch.SetActive(false);
            }
        }
    }
}
