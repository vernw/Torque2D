using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltActor : MonoBehaviour {
    public static float dragCo;
    public float mass;
    public Vector2 velocity;

    //Vector2 tempLocation;
    SpriteRenderer sr;
    AltActor target;
    float distance;

    public virtual void Initialize(float _mass, Util.COLOR color, AltActor _target = null, float _distance = 0f)
    {
        mass = _mass;
        sr = GetComponent<SpriteRenderer>();
        sr.color = Util.ConvertColor(color);
        target = _target;
        distance = _distance;
    }

    public virtual void ApplyVelocity()
    {
        transform.Translate(velocity);
    }

    public void ApplyConstraint()
    {
        float angleToTarget = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x);
        float d = Mathf.Sqrt(Mathf.Pow(target.transform.position.x - transform.position.x, 2) + Mathf.Pow(target.transform.position.y - transform.position.y, 2)) - distance;
        float myPortion = d * (mass / (mass + target.mass));
        float theirPortion = d * (target.mass / (mass + target.mass));
        Vector2 myDelta = new Vector2(Mathf.Cos(angleToTarget) * theirPortion, Mathf.Sin(angleToTarget) * theirPortion);
        transform.Translate(myDelta);
        velocity += myDelta;
        float angleToMe = angleToTarget + Mathf.PI;
        Vector2 theirDelta = new Vector2(Mathf.Cos(angleToMe) * myPortion, Mathf.Sin(angleToMe) * myPortion);
        target.transform.Translate(theirDelta);
        target.velocity += theirDelta;
    }

    //void ApplyDrag()
    //{
    //    velocity = new Vector2(velocity.x + velocity.x * velocity.x * -dragCo * Time.deltaTime,
    //                            velocity.y + velocity.y * velocity.y * -dragCo * Time.deltaTime);
    //}
}
