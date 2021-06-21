using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class WaterParticle : MonoBehaviour
{
    public float mass = 0.05f;
    public float velocity = 0f;
    public float surface = 0f;
    public float r = 0.1f;

    private PhysicsCode PhysicsCalc = null;

    // Start is called before the first frame update
    void Start()
    {
        mass = 0.05f;
        velocity = 0f;
        surface = 0f;
        r = 0.1f;
        surface = 3.1415f * r * r;
        transform.localScale = new Vector3(r, r, r);
        PhysicsCalc = new PhysicsCode();
    }

    private float abs_dbl(float x)
    {
        if (x < 0)
        {
            return -1.0f * x;
        }
        return x;
    }

    // Update is called once per frame
    void Update()
    {
        //float t = Time.deltaTime;
        //if(transform.localPosition.y < -1 || transform.localPosition.y > 100)
        //{
        //    Destroy(gameObject);
        //}
        //float total_force = PhysicsCalc.getGravity(mass) - PhysicsCalc.getAirResistance(surface, velocity);
        //float acceleration = total_force / mass;
        //float speed = velocity + abs_dbl(acceleration) * t;
        //if (acceleration >= 0)
        //{
        //    transform.localPosition = new Vector3((float)transform.localPosition.x, (float)(transform.localPosition.y - speed * t), (float)transform.localPosition.z);
        //}
        //else
        //{
        //    transform.localPosition = new Vector3((float)transform.localPosition.x, (float)(transform.localPosition.y + speed * t), (float)transform.localPosition.z);
        //}
        //velocity = speed;
    }
}
