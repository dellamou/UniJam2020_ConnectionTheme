using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSun : MonoBehaviour
{
    public float DayLength;
    private float _rotationSpeed;
 
    void Update(){
        _rotationSpeed = Time.deltaTime / DayLength;
        transform.Rotate (0, _rotationSpeed, 0);
    }
}
