using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Rot : MonoBehaviour
{
    private const float bucket_mass = 0.5f;
    private const float paint_liter_mass = 1.354f;
    private const float surface = 0.48f;
    private const float rope_length = 12.5f; //0.8m

    private Vector3 eulerAngs, eulerAngs1, eulerAngs2;
    private float total_mass;

    public int liters_of_paint = 18;
    private float velocity = 0f;
    private float r = 0.1f;

    private float v1 = 0f;
    private float v2 = 0f;
    private float theta_max1 = 60f;
    private float theta_max2 = 60f;

    private float theta = 45f;
    private float phi = -90f;
    private float vtheta = 0f;
    private float vphi = 0f;
    private float atheta = 0f;
    private float aphi = 0f;

    public int randr = 3;
    public float prand = 0.1f;

    private float curtime;

    private PhysicsCode PhysicsCalc = null;

    // Start is called before the first frame update
    void Start()
    {
        

        total_mass = bucket_mass + liters_of_paint * paint_liter_mass;

        eulerAngs = new Vector3(0f, -1f*phi, -1f*theta);

        transform.Rotate(Vector3.forward, eulerAngs.z, Space.World);
        transform.Rotate(Vector3.up, eulerAngs.y, Space.World);

    }

    void Update()
    {
        // 90y 10theta

        if(phi < 90)
        {
            phi += 0.9f;
            transform.Rotate(Vector3.up, 0.9f, Space.World);
            transform.Rotate(Vector3.forward, 0.3f, Space.Self);
        }

        // - 30 * abs(cos(phi))
        //phi += 4f;

        //float tmp = theta;
        //theta = 20f + 45f * Mathf.Cos(phi / 180f);

        //eulerAngs = new Vector3(0f, -1f * phi, -1f * theta);

        //transform.Rotate(Vector3.right, 4f, Space.Self);
        //transform.Rotate(Vector3.right, tmp - theta, Space.Self);
        //float tmp = theta;
        //atheta = vphi * vphi * Mathf.Sin(theta/180f) * Mathf.Cos(theta/180f) - 9.8f / 0.8f * Mathf.Sin(theta/180f);
        //aphi = -2 * vtheta * vphi * (Mathf.Cos(theta/180f) / Mathf.Sin(theta/180f));
        //vtheta = vtheta + atheta *0.2f;
        //vphi = vphi + aphi * 0.2f;
        //theta = theta + vtheta * 0.2f;
        //phi = phi + vphi * 0.2f;
        //phi = phi - tmp + theta;
        //eulerAngs = new Vector3(eulerAngs.x, -1f*phi, -1f*theta);
        //transform.eulerAngles = eulerAngs;
    }
}
    //void Start()
    //{
    //    total_mass = bucket_mass + liters_of_paint * paint_liter_mass;
    //    eulerAngs1 = Vector3.zero;
    //    eulerAngs2 = Vector3.zero;
    //    curtime = 0f;

    //    eulerAngs1 = new Vector3(theta_max1, 0f, 0f);
    //    eulerAngs2 = new Vector3(0f, theta_max2, 0f);

    //    //transform.GetChild(0).eulerAngles = eulerAngs2;
    //    transform.eulerAngles = eulerAngs1;

    //}

    //void Update()
    //{

    //    float acc = -1f * total_mass * 9.8f * Mathf.Sin(eulerAngs1.x / 180f) / 0.8f;
    //    v1 = v1 + acc * 0.02f;

    //    eulerAngs1 = new Vector3(eulerAngs1.x + v1 * 0.02f, eulerAngs1.y, eulerAngs1.z);

    //    transform.eulerAngles = eulerAngs1;
    //}