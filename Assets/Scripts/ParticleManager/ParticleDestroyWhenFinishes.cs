using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyWhenFinishes : MonoBehaviour
{
    public ParticleSystem targetParticleSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.targetParticleSystem.IsAlive())
        {
            Destroy(targetParticleSystem.gameObject);
        }
    }
}