using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energyBar : MonoBehaviour
{
	public MoveCam target;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Slider>().maxValue = target.fullEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Slider>().maxValue = target.fullEnergy;
        this.GetComponent<Slider>().value = target.energy;
    }
}
