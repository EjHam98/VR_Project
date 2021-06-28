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

    LineRenderer line;

    Rot code;

    Vector3 projec_speed;

    // Start is called before the first frame update
    void Start()
    {
        line = GameObject.Find("Line").GetComponent<LineRenderer>();
        line.startColor = Color.white;
        line.endColor = Color.white;

        line.startWidth = 0.3f;
        line.endWidth = 0.3f;
        projec_speed = Vector3.zero;
        pend = GameObject.Find("Pendulum");
        code = pend.GetComponent<Rot>();
        if (code.prev_phi != 0 && code.prev_theta != 0)
        {
            projec_speed = (code.pos2 - code.pos1) / 0.02f;
            projec_speed = new Vector3(projec_speed.x * 2, projec_speed.y*0.5f, projec_speed.z * 2);

        }
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
        float t = Time.deltaTime; 
        //    line.startColor = Color.white;
        //    line.endColor = Color.white;

        //    line.startWidth = 0.3f;
        //    line.endWidth = 0.3f;

        //    if(currlines==0)
        //    {
        //        line.positionCount+=2;
        //        //old_pos = old_pos * -1;
        //        line.SetPosition(currlines, old_pos);
        //        currlines++;
        //        //new_pos = new_pos * -1;
        //        line.SetPosition(currlines, new_pos);
        //        currlines++;

        //    }
        if(line.positionCount==0)
        {
            if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2)
            {
                line.positionCount += 2;
                line.SetPosition(0, new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
                line.SetPosition(1, new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
            }
            if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0)
            {
                line.positionCount += 2;
                line.SetPosition(0, new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
                line.SetPosition(1, new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2)
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, new Vector3(transform.localPosition.x, 0.21f, transform.localPosition.z));
                Destroy(gameObject);
            }
            if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0)
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z));
                Destroy(gameObject);
            }
        }

        //if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2)
        //{
        //    GameObject tmp = Instantiate(drop);
        //    tmp.transform.localPosition = new Vector3(transform.localPosition.x, tmp.transform.localPosition.y + 0.21f, transform.localPosition.z);
        //}
        //if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0)
        //{
        //    GameObject tmp = Instantiate(drop);
        //    tmp.transform.localPosition = new Vector3(transform.localPosition.x, tmp.transform.localPosition.y + 0.01f, transform.localPosition.z);
        //}
        //if (transform.localPosition.x >= -7.5 && transform.localPosition.x <= 7.5 && transform.localPosition.z >= -7.5 && transform.localPosition.z <= 7.5 && transform.localPosition.y < 0.2)
        //{
        //    RawImage tmp = Instantiate(drop);
        //    tmp.transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        //    tmp.transform.parent = board.transform;
        //}
        //if ((transform.localPosition.x < -7.5 || transform.localPosition.x > 7.5 || transform.localPosition.z < -7.5 || transform.localPosition.z > 7.5) && transform.localPosition.y < 0)
        //{
        //    RawImage tmp = Instantiate(drop);
        //    tmp.transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        //    tmp.transform.parent = ground.transform;
        //}
        projec_speed = projec_speed * 0.95f;
        if (transform.localPosition.y < -1 || transform.localPosition.y > 100)
        {
            Destroy(gameObject);
        }
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
