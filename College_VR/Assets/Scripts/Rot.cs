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

    private Vector3 eulerAngs;
    private float total_mass;

    public int liters_of_paint = 18;
    public float velocity = 0f;
    public float r = 0.1f;

    public float v1 = 0f;
    public float v2 = 0f;
    public float theta_max1 = 60f;
    public float theta_max2 = 60f;

    public int randr = 3;
    public float prand = 0.1f;

    private float curtime;

    private PhysicsCode PhysicsCalc = null;

    // Start is called before the first frame update
    void Start1()
    {
        total_mass = bucket_mass + liters_of_paint * paint_liter_mass;
        eulerAngs = Vector3.zero;
        transform.localScale = new Vector3(r, r, r);
        Vector3 curpos = transform.localPosition;
        curpos = curpos + new Vector3(prand * Random.Range(-1 * randr, randr), prand * Random.Range(-1 * randr, randr), prand * Random.Range(-1 * randr, randr));
        transform.localPosition = curpos;
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
    //void Update1()
    //{
    //    float t = Time.deltaTime;
    //    if (transform.localPosition.y < -1 || transform.localPosition.y > 100)
    //    {
    //        Destroy(gameObject);
    //    }
    //    float total_force = PhysicsCalc.getGravity(mass) - PhysicsCalc.getAirResistance(surface, velocity);
    //    float acceleration = total_force / mass;
    //    float speed = velocity + abs_dbl(acceleration) * t;
    //    if (acceleration >= 0)
    //    {
    //        transform.localPosition = new Vector3((float)transform.localPosition.x, (float)(transform.localPosition.y - speed * t), (float)transform.localPosition.z);
    //    }
    //    else
    //    {
    //        transform.localPosition = new Vector3((float)transform.localPosition.x, (float)(transform.localPosition.y + speed * t), (float)transform.localPosition.z);
    //    }
    //    velocity = speed;
    //}

    void Start()
    {
        //transform.Rotate(0f, 0f, 45f, Space.World);
        //transform.Rotate(0f, 45f, 0f, Space.World);
        //transform.Rotate(45f, 0f, 0f, Space.World);
        //transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, 45f);
        //transform.rotation = transform.rotation * Quaternion.Euler(0f, 45f, 0f);
        //transform.rotation = transform.rotation * Quaternion.Euler(45f, 0f, 0f);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 0f);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 0f);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, 0f);

        
        //transform.eulerAngles = transform.eulerAngles + new Vector3(-45f, 0f, 0f);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, -45f);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 90f, 0f);

        //Debug.Log(transform.eulerAngles);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(30f, 0f, 0f);
        //Debug.Log(transform.eulerAngles);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 30f, 0f);
        //Debug.Log(transform.eulerAngles);
        //transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 0f, -45f);

        total_mass = bucket_mass + liters_of_paint * paint_liter_mass;
        eulerAngs = Vector3.zero;
        curtime = 0f;

        eulerAngs = new Vector3(theta_max1, 0f, 0f);

        transform.eulerAngles = eulerAngs;

    }

    void Update()
    {
        ////-g sin(theta)
        //transform.eulerAngles = eulerAngs;
        //curtime += Time.deltaTime;
        ////float force = total_mass * 9.8f * Mathf.Sin(eulerAngs.x / 180f);
        //float acc = -0.0007f * total_mass * 9.8f * Mathf.Sin(eulerAngs.x/180f);
        ////velocity = velocity + acc * 0.02f;
        //velocity += acc;
        
        //eulerAngs = new Vector3(eulerAngs.x + velocity, 0f, 0f);

        //-g sin(theta)
        
        //float force = total_mass * 9.8f * Mathf.Sin(eulerAngs.x / 180f);
        float acc = -1f * total_mass * 9.8f * Mathf.Sin(eulerAngs.x / 180f) / 0.8f;
        //velocity = velocity + acc * 0.02f;
        v1 = v1 + acc * 0.02f;

        eulerAngs = new Vector3(eulerAngs.x + v1 * 0.02f, eulerAngs.y, eulerAngs.z);

        transform.eulerAngles = eulerAngs;
    }
}
