using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//get the text moving
public class Move : MonoBehaviour
{
    public Vector3 StartPos = new Vector3(-3.48f,-2.36f,-5.4f);
    public Vector3 EndPos = new Vector3(-3.48f,6.88f,-5.4f);
    public double Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(transform.position.y >= EndPos.y)){
            transform.position += new Vector3(0, (float)Speed*Time.deltaTime,0);
        }
    }
}
