using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Rotate items when it's on the ground, jump when the player is near enough
public class itemMotion : MonoBehaviour
{
    private float spinSpeed=50;
    public GameObject player;
    private float jumpRadius = 3;

    private float yLimit = 0.25f;
    private float moveSpeed = 0.25f;

    private bool back =false;
    private bool startJump = false;
    private float distance;
    private Vector3 originPos;
    void Start()
    {
        originPos = this.transform.position;
    }
    void Update()
    {
        //rotate
        this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * spinSpeed, new Vector3(0.0f, 1.0f, 0.0f));
    
        //jump when distance is close enough
        distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance <= jumpRadius){
                startJump=true;
            } 

            if (startJump){
                if (this.transform.position.y > originPos.y + yLimit){
                    back = true;
                }
                else if (this.transform.position.y < originPos.y){
                    back = false;
                } 
                if (back){
                    this.transform.localPosition += new Vector3 (0.0f,-moveSpeed*Time.deltaTime,0.0f);
                }
                else{
                    this.transform.localPosition += new Vector3 (0.0f,moveSpeed*Time.deltaTime,0.0f);
                }
            }
            if (distance > jumpRadius){
                startJump=false;
            } 
    }
}
