using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class WaterParticle : MonoBehaviour
{
    public float mass = 0.05f;
    public float velocity = 0f;
    public float surface = 0f;
    public float r = 0.1f;

    private PhysicsCode PhysicsCalc = null;

    public GameObject drop;

    public GameObject pend;

    private int pind;

    public LineRenderer line, dropping;

    Rot code;

    bool willdraw = true;

    Vector3 projec_speed;

    // Start is called before the first frame update
    void Start()
    {
        //line = GameObject.Find("Line").GetComponent<LineRenderer>();
        //line.startColor = Color.white;
        //line.endColor = Color.white;

        //line.startWidth = 0.3f;
        //line.endWidth = 0.3f;
        projec_speed = Vector3.zero;
        pend = GameObject.Find("Pendulum");
        code = pend.GetComponent<Rot>();
        if (code.prev_phi != 0 && code.prev_theta != 0)
        {
            projec_speed = (code.pos2 - code.pos1) / 0.02f;
            projec_speed = new Vector3(projec_speed.x * 2, projec_speed.y*0.5f, projec_speed.z * 2);

        }
        else
        {
            willdraw = false;
        }
        mass = 0.05f;
        velocity = 0f;
        surface = 0f;
        r = 0.1f;
        surface = 3.1415f * r * r;
        transform.localScale = new Vector3(r, r, r);
        PhysicsCalc = new PhysicsCode();
        //dropping = GameObject.Find("DroppingLiquid").GetComponent<LineRenderer>();
        if(willdraw)
        {
            pind = dropping.positionCount;
            dropping.positionCount++;
            dropping.SetPosition(pind, transform.position);
        }
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
        if (code.State != 1 || transform.localPosition.y < 0)
        {
            Destroy(gameObject);
        }
        if(dropping.positionCount > 0 && willdraw)
        {
            dropping.SetPosition(pind, transform.position);
        }
        float t = Time.deltaTime;


        if (line.positionCount == 0)
        {
            if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2 && code.State == 1 && willdraw)
            {
                line.positionCount += 2;
                line.SetPosition(0, line.worldToLocalMatrix * new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
                line.SetPosition(1, line.worldToLocalMatrix * new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
            }
            if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0 && code.State == 1 && willdraw)
            {
                line.positionCount += 2;
                line.SetPosition(0, line.worldToLocalMatrix * new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
                line.SetPosition(1, line.worldToLocalMatrix * new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y <= 0.21f && code.State == 1 && willdraw)
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, line.worldToLocalMatrix * new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
                Destroy(gameObject);
            }
            if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y <= 0.01f && code.State == 1 && willdraw)
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, line.worldToLocalMatrix * new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
                Destroy(gameObject);
            }
        }
        if (transform.localPosition.y == 0)
        {
            Destroy(gameObject);
        }

        //if (line.positionCount == 0)
        //{
        //    if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2 && code.State == 1 && willdraw)
        //    {
        //        line.positionCount += 2;
        //        line.SetPosition(0, new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
        //        line.SetPosition(1, new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
        //    }
        //    if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0 && code.State == 1 && willdraw)
        //    {
        //        line.positionCount += 2;
        //        line.SetPosition(0, new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
        //        line.SetPosition(1, new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
        //        Destroy(gameObject);
        //    }
        //}
        //else
        //{
        //    if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2 && code.State == 1 && willdraw)
        //    {
        //        line.positionCount += 1;
        //        line.SetPosition(line.positionCount - 1, new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
        //        Destroy(gameObject);
        //    }
        //    if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0 && code.State == 1 && willdraw)
        //    {
        //        line.positionCount += 1;
        //        line.SetPosition(line.positionCount - 1, new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
        //        Destroy(gameObject);
        //    }
        //}


        projec_speed = projec_speed * 0.95f;
        float total_force = -5f*PhysicsCalc.getGravity(mass).y - PhysicsCalc.getAirResistance(surface, velocity);
        float acceleration = total_force / mass;
        float speed = velocity + abs_dbl(acceleration) * t;
        if (acceleration >= 0)
        {
            transform.localPosition = new Vector3((float)transform.localPosition.x + projec_speed.x * t, (float)(transform.localPosition.y - speed * t + projec_speed.y * t), (float)transform.localPosition.z + projec_speed.z * t);
        }
        else
        {
            transform.localPosition = new Vector3((float)transform.localPosition.x + projec_speed.x * t, (float)(transform.localPosition.y + speed * t + projec_speed.y * t), (float)transform.localPosition.z + projec_speed.z * t);
        }
        velocity = speed;
    }
}
