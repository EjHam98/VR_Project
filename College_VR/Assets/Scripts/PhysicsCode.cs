using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCode : MonoBehaviour
{
    public double cd = 0.47;
    public double ro = 1.225;
    public double g = 9.98;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public double getGravity(double mass)
    {
        return mass * g;
    }

    public double getAirResistance(double surface, double velocity)
    {
        return 0.5 * cd * ro * surface * velocity * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
