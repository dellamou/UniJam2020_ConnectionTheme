using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public connectWIthFriend target;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Slider>().maxValue = target.fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Slider>().maxValue = target.fullHealth;
        this.GetComponent<Slider>().value = target.health;
    }
}