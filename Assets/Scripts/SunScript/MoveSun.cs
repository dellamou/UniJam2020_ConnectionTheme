using UnityEngine;

public class MoveSun : MonoBehaviour
{
    public float DayLen_Disable_by_Zero;
    private float _rotationSpeed;
 
    void Update(){
        _rotationSpeed = Time.deltaTime / DayLen_Disable_by_Zero;
        transform.Rotate (0, _rotationSpeed, 0);
    }
}
