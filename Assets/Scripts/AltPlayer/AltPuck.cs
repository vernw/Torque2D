using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPuck : AltActor
{
    public bool playerMoving = false;

    float startMass;
    float fakeMass;
    float maxFakeMass;
    float maxFakeMassMultiplier = 5f;
    float massDecayTime = 1f;
    float fakeMassIncreaseTime = 1f;
    float fakeMassDecayTime = 1f;

    public override void Initialize(float _mass, Util.COLOR color, AltActor _target = null, float _distance = 0f)
    {
        fakeMass = startMass = _mass;
        maxFakeMass = startMass * maxFakeMassMultiplier;
        base.Initialize(_mass, color, _target, _distance);
    }

    public override void ApplyVelocity()
    {
        //if (playerMoving)
        //{
        //    mass = Mathf.Lerp(mass, startMass, Time.deltaTime / massDecayTime);
        //    fakeMass = Mathf.Lerp(fakeMass, maxFakeMass, Time.deltaTime / fakeMassIncreaseTime);
        //} else
        //{
        //    fakeMass = Mathf.Lerp(fakeMass, startMass, Time.deltaTime / fakeMassDecayTime);
        //    mass = fakeMass;
        //}
        if (playerMoving)
        {
            mass = startMass;
        } else
        {
            mass = startMass * 100f;
        }
        base.ApplyVelocity();
        //Debug.Log(mass);
    }
}
