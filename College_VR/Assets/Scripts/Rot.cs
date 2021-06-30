using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Rot : MonoBehaviour
{
    public const float bucket_mass = 0.5f;
    public const float paint_liter_mass = 1.354f;
    public const float surface = 0.48f;
    public const float rope_length = 12.5f; //0.8m

    public int State = 0;
    public int speedup = 1;

    public int damping = 5;

    public LineRenderer line, dropping;
    public int currlines = 0;

    public Vector3 eulerAngs, eulerAngs1, eulerAngs2;
    public float total_mass;

    public float theangle = 0.25f;

    public float liters_of_paint = -1f;
    public float velocity = 0f;
    public float r = 0.1f;

    public float v1 = 0f;
    public float v2 = 0f;
    public float theta_max1 = 60f;
    public float theta_max2 = 60f;

    public Vector3 pos1, pos2;

    public float theta = 0.35f;
    public float phi = 0f;
    public float prev_theta = 0f;
    public float prev_phi = 0f;
    public float vtheta = 0f;
    public float vphi = 0.5f;
    public float atheta = 0f;
    public float aphi = 0f;

    public float old_th;
    public float old_ph;

    public float tm = 0.0f;
    public float delta_t = 0.02f;
    public float[] y = { 0.0f, 0.5f, 0.35f, 0f };

    public int randr = 3;
    public float prand = 0.1f;

    public float curtime;

    public PhysicsCode PhysicsCalc = null;

    // Start is called before the first frame update
    void Start()
    {
        if (liters_of_paint == -1) liters_of_paint = 20;
        //damping = 5;

        line.positionCount = 0;
        line.startColor = Color.white;
        line.endColor = Color.white;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        //line.SetPosition(0, Vector3.zero);
        total_mass = bucket_mass + liters_of_paint * paint_liter_mass;

        eulerAngs = new Vector3(0f, -1f * y[3] * 180f, -1f * y[2] * 180f);

        transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, 0.9f*0.05f*Mathf.Min(20, Mathf.Max(0, liters_of_paint)), transform.GetChild(3).localScale.z);

        //transform.Rotate(Vector3.forward, eulerAngs.z, Space.World);
        //transform.Rotate(Vector3.up, eulerAngs.y, Space.World);
        y[2] = theangle;
        theta = theangle;
        prev_phi = 0;
        prev_theta = 0;
        pos1 = Vector3.zero;
        pos2 = Vector3.zero;
        //transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, Mathf.Max(0, transform.GetChild(3).localScale.y - (1/3000f)), transform.GetChild(3).localScale.z);
        //Renderer meesh = transform.GetChild(3).GetComponent<Renderer>();
        //transform.GetChild(3).localPosition = new Vector3(transform.GetChild(3).localPosition.x, -15.7f + ((meesh.bounds.size.y * transform.GetChild(3).localScale.y) / 2f), transform.GetChild(3).localPosition.z);
        
        //ApplyMovement();
        //transform.GetChild(0).transform.Rotate(Vector3.up, y[3] * 180f, Space.Self);
        //transform.GetChild(0).transform.Rotate(Vector3.forward, -1f * y[2] * 180f, Space.Self);
        dropping = GameObject.Find("DroppingLiquid").GetComponent<LineRenderer>();
        dropping.startColor = Color.white;
        dropping.endColor = Color.white;
        dropping.startWidth = 0.2f;
        dropping.endWidth = 0.2f;
        dropping.positionCount = 0;
    }

    void ApplyRotation()
    {
        if(Mathf.Abs(theta - prev_theta) <= 0.005f && Mathf.Abs(theta) <= 0.001f)
        {
            State = 0;
            theta = theangle;
            phi = 0f;
            prev_theta = 0f;
            prev_phi = 0f;
            y[0] = 0f;
            y[1] = 0.5f;
            y[2] = theangle;
            y[3] = 0f;
            tm = 0;
            transform.GetChild(0).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(2).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(3).transform.localPosition = new Vector3(0f, -15.17f, 0f);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //transform.GetChild(0).transform.eulerAngles = Vector3.zero;
            return;
        }
        Vector3 old_pos = transform.GetChild(0).transform.position;
        pos1 = old_pos;
        if (old_pos.x >= -7.5 && old_pos.x <= 7.5 && old_pos.z >= -7.5 && old_pos.z <= 7.5)
        {
            old_pos.y = 0.21f;
        }
        if (old_pos.x < -7.5 || old_pos.x > 7.5 || old_pos.z < -7.5 || old_pos.z > 7.5)
        {
            old_pos.y = 0.01f;
        }
        transform.Rotate(Vector3.up, (phi - prev_phi) * -60f, Space.World);
        //transform.Rotate(Vector3.up, (phi - prev_phi) * 60f, Space.Self);
        transform.Rotate(Vector3.forward, (theta - prev_theta) * 180f, Space.Self);
        transform.GetChild(0).transform.Rotate(Vector3.up, (phi - prev_phi) * 60f, Space.Self);
        transform.GetChild(1).transform.Rotate(Vector3.up, (phi - prev_phi) * 60f, Space.Self);
        transform.GetChild(2).transform.Rotate(Vector3.up, (phi - prev_phi) * 60f, Space.Self);
        Vector3 new_pos = transform.GetChild(0).transform.position;
        pos2 = new_pos;
        if (new_pos.x >= -7.5 && new_pos.x <= 7.5 && new_pos.z >= -7.5 && new_pos.z <= 7.5)
        {
            new_pos.y = 0.21f;
        }
        if (new_pos.x < -7.5 || new_pos.x > 7.5 || new_pos.z < -7.5 || new_pos.z > 7.5)
        {
            new_pos.y = 0.01f;
        }

        //if(prev_phi != 0 && prev_theta != 0)
        //{
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
        //    else
        //    {
        //        line.positionCount += 1;
        //        //new_pos = new_pos * -1;
        //        line.SetPosition(currlines, new_pos);
        //        currlines++;
        //    }

        //    // set the position
        //    //currlines++;
        //}
    }

    int SignOf(float num)
    {
        if(num < 0)
        {
            return -1;
        }
        return 1;
    }

    float[] G(float[] y, float t)
    {
        atheta = y[1] * y[1] * Mathf.Sin(y[2]) * Mathf.Cos(y[2]) - 9.8f / 4.5f * Mathf.Sin(y[2]);
        if(Mathf.Abs(y[2]) <= 0.001f)
        {
            aphi = 0;
        }
        aphi = -2f * y[0] * y[1] * (1f / Mathf.Tan(y[2]));

        //atheta = SignOf(atheta) * (Mathf.Abs(atheta) + t * 0.1f);
        //aphi = SignOf(aphi) * (Mathf.Abs(aphi) + t * 0.1f);

        return new float[4] { atheta, aphi, y[0] - 0.002f - (0.00025f * Mathf.Max(0, damping - 1)), y[1] - 0.002f - (0.00025f * Mathf.Max(0, damping - 1)) };
        //return new float[4] { atheta, aphi, y[0] - 0.003f, y[1] - 0.003f };
    }

    void ApplyMovement()
    {
        float xm = -15f * Mathf.Sin(y[2]) * Mathf.Cos(y[3]);// +offset[0];
        float ym = -15f * Mathf.Cos(y[2]);// +offset[1];
        float zm = -15f * Mathf.Sin(y[2]) * Mathf.Sin(y[3]);
        float xm2 = -15.17f * Mathf.Sin(y[2]) * Mathf.Cos(y[3]);// +offset[0];
        float ym2 = -15.17f * Mathf.Cos(y[2]);// +offset[1];
        float zm2 = -15.17f * Mathf.Sin(y[2]) * Mathf.Sin(y[3]);
        transform.GetChild(0).transform.localPosition = new Vector3(xm, ym, zm);
        transform.GetChild(2).transform.localPosition = new Vector3(xm, ym, zm);
        transform.GetChild(3).transform.localPosition = new Vector3(xm2, ym2, zm2);
    }

    float[] RK4Step(float[] y, float t, float dt)
    {
        float[] tmp = new float[4];
        float[] k1 = G(y, t);
        for (int i = 0; i < 4; i++)
        {
            tmp[i] = y[i] + 0.5f * k1[i] * dt;
        }
        float[] k2 = G(tmp, t + 0.5f * dt);
        for (int i = 0; i < 4; i++)
        {
            tmp[i] = y[i] + 0.5f * k2[i] * dt;
        }
        float[] k3 = G(tmp, t + 0.5f * dt);
        for (int i = 0; i < 4; i++)
        {
            tmp[i] = y[i] + k3[i] * dt;
        }
        float[] k4 = G(tmp, t + dt);
        for (int i = 0; i < 4; i++)
        {
            tmp[i] = dt * (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]) / 6f;
        }

        return tmp;
    }

    void Update()
    {
        //ApplyMovement();
        //transform.GetChild(0).transform.Rotate(Vector3.up, (old_ph-y[3]) * 180f, Space.Self);
        //transform.GetChild(0).transform.Rotate(Vector3.forward, -1f*(old_th - y[2]) * 180f, Space.Self);
        if (Input.GetKeyUp(KeyCode.U))
        {
            transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, 0.9f * 0.05f * Mathf.Min(20, Mathf.Max(0, liters_of_paint)), transform.GetChild(3).localScale.z);
            line.positionCount = 0;
            State = 1;
            theta = theangle;
            phi = 0f;
            prev_theta = 0f;
            prev_phi = 0f;
            y[0] = 0f;
            y[1] = 0.5f;
            y[2] = theangle;
            y[3] = 0f;
            tm = 0;
            transform.GetChild(0).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(2).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(3).transform.localPosition = new Vector3(0f, -15.17f, 0f);
            transform.eulerAngles = Vector3.zero;
            transform.GetChild(0).transform.eulerAngles = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            State = 2;
            theta = theangle;
            phi = 0f;
            prev_theta = 0f;
            prev_phi = 0f;
            y[0] = 0f;
            y[1] = 0.5f;
            y[2] = theangle;
            y[3] = 0f;
            tm = 0;
            transform.GetChild(0).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(2).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(3).transform.localPosition = new Vector3(0f, -15.17f, 0f);
            transform.eulerAngles = Vector3.zero;
            transform.GetChild(0).transform.eulerAngles = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            line.positionCount = 0;
            State = 0;
            theta = theangle;
            phi = 0f;
            prev_theta = 0f;
            prev_phi = 0f;
            y[0] = 0f;
            y[1] = 0.5f;
            y[2] = theangle;
            y[3] = 0f;
            tm = 0;
            transform.GetChild(0).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(2).transform.localPosition = new Vector3(0f, -15f, 0f);
            transform.GetChild(3).transform.localPosition = new Vector3(0f, -15.17f, 0f);
            transform.eulerAngles = Vector3.zero;
            transform.GetChild(0).transform.eulerAngles = Vector3.zero;
        }
        if (State == 1)
        {
            for(int q=0;q<speedup;q++)
            {
                transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, Mathf.Max(0, transform.GetChild(3).localScale.y - (1/3000f)), transform.GetChild(3).localScale.z);
                if(transform.GetChild(3).localScale.y == 0)
                {
                    transform.GetChild(3).GetComponent<Renderer>().enabled = false;
                }
                Renderer meesh = transform.GetChild(3).GetComponent<Renderer>();
                transform.GetChild(3).localPosition = new Vector3(transform.GetChild(3).localPosition.x, Mathf.Min(-15f, -15.7f + (meesh.bounds.size.y*transform.GetChild(3).localScale.y/2f)), transform.GetChild(3).localPosition.z);
                ApplyRotation();
                tm += delta_t;
                float[] tmp = RK4Step(y, tm, delta_t);
                for (int i = 0; i < 4; i++)
                {
                    y[i] += tmp[i];
                }

                prev_theta = theta;
                theta = y[2];
                prev_phi = phi;
                phi = y[3];
            }
        }
        else if (State == 2)
        {
            ApplyMovement();
            tm += delta_t;
            float[] tmp = RK4Step(y, tm, delta_t);
            for (int i = 0; i < 4; i++)
            {
                y[i] += tmp[i];
            }
            //ApplyRotation();
            //prev_phi = phi;
            //prev_theta = theta;
        }
        //if (phi < 90)
        //{
        //    phi += 0.9f;
        //    transform.Rotate(Vector3.up, 0.9f, Space.World);
        //    transform.Rotate(Vector3.forward, 0.3f, Space.Self);
        //}

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