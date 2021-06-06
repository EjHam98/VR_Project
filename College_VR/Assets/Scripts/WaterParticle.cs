using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public double mass = 0.05;
    public double velocity = 0;
    public double surface = 0;

    public float r = 0.5f;

    public double t = 0.02;

    public GameObject pcobj;

    public PhysicsCode PhysicsCalc;
    // Start is called before the first frame update
    void Start()
    {
        r = 0.1f;
        surface = 3.1415 * r * r;
        transform.localScale = new Vector3(r, r, r);
        Vector3 curpos = transform.localPosition;
        curpos = curpos + new Vector3(0.1f * Random.Range(-3, 3), 0.1f * Random.Range(-3, 3), 0.1f * Random.Range(-3, 3));
        transform.localPosition = curpos;
        PhysicsCalc = (PhysicsCode) pcobj.GetComponent<PhysicsCode>();
    }

    private double abs_dbl(double x)
    {
        if (x < 0)
        {
            return -1.0 * x;
        }
        return x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y < -1 || transform.localPosition.y > 100)
        {
            Destroy(gameObject);
        }
        double total_force = PhysicsCalc.getGravity(mass) - PhysicsCalc.getAirResistance(surface, velocity);
        double acceleration = total_force / mass;
        double speed = velocity + abs_dbl(acceleration) * t;
        if (acceleration >= 0)
        {
            transform.localPosition = new Vector3((float)transform.localPosition.x, (float)(transform.localPosition.y - speed * t), (float)transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3((float)transform.localPosition.x, (float)(transform.localPosition.y + speed * t), (float)transform.localPosition.z);
        }
        velocity = speed;
    }
}
