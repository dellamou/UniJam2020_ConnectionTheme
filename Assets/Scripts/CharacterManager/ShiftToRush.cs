using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftToRush : MonoBehaviour
{
    private bool isRushing = false;
    // set to -10 so that playe can rush at the start of this game
    public float lastRush = -10f;

    // after a rush, character has to wait for 10 sec for next rush
    public float RUSH_COOLDOWN_TIME = 10f;
    public float RUSH_TIME = 5f;

    public float RUSH_BONUS = 4f;

    private CharacterMovement mainChar;

    // a special effects will be generated when character rushing
    public GameObject rushParticle;

    // Update is called once per frame
    void Update()
    {
        mainChar = this.GetComponent<CharacterMovement>();
        if (Time.realtimeSinceStartup - lastRush >= RUSH_TIME && isRushing)
        {
            mainChar.forwardSpeed -= RUSH_BONUS;
            mainChar.backwardSpeed -= RUSH_BONUS;
            isRushing = false;
        }

        // if shift is pressed and rush is not in cooldown, 
        // let the character start rushing
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        && !isRushing && Time.realtimeSinceStartup - lastRush >= RUSH_COOLDOWN_TIME)
        {
            isRushing = true;
            lastRush = Time.realtimeSinceStartup;
            mainChar.forwardSpeed += RUSH_BONUS;
            mainChar.backwardSpeed += RUSH_BONUS;
        }

        // 这是生成rush后面的烟尘particle
        
        if (isRushing)
        {
            GameObject obj = Instantiate<GameObject>(rushParticle);
            obj.transform.position = this.transform.position;
        }
        
    }

}
