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
    public int tracks = 5;
    public int speedup = 5;
    public int damping = -1;
    public int currlines = 0;
    public float liters_of_paint = -1f;

    public float init_theta = 0.45f;
    public float init_phi = 0.5f;

    public Vector3 pos1, pos2;

    public bool clear = false;

    public float theta = 0.35f;
    public float phi = 0f;
    public float prev_theta = 0f;
    public float prev_phi = 0f;

    public float tm = 0f;
    public float delta_t = 0.02f;
    public float[] y = { 0.0f, 0.0f, 0.25f, 0f };

    public Camera cam_interface, cam_main, cam_up, cam_good;

    public PhysicsCode PhysicsCalc = null;

    public void reset_vars()
    {
        State = 0;
        currlines = 0;
        transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, 0.9f * 0.05f * Mathf.Min(20, Mathf.Max(0, liters_of_paint)), transform.GetChild(3).localScale.z);
        Renderer meesh = transform.GetChild(3).GetComponent<Renderer>();
        transform.GetChild(3).localPosition = new Vector3(transform.GetChild(3).localPosition.x, Mathf.Min(-15f, -15.7f + (meesh.bounds.size.y * transform.GetChild(3).localScale.y / 2f)), transform.GetChild(3).localPosition.z);
        transform.GetChild(3).GetComponent<Renderer>().enabled = true;
        theta = init_theta;
        phi = init_phi;
        prev_theta = 0f;
        prev_phi = 0f;
        y[0] = 0f;
        y[1] = 0.0f;
        y[2] = init_theta;
        y[3] = init_phi;
        tm = 0;
        transform.eulerAngles = Vector3.zero;
        transform.GetChild(0).transform.eulerAngles = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (liters_of_paint == -1) liters_of_paint = 20;
        if (damping == -1) damping = 5;
        if (tracks == -1) tracks = 1;

        transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, 0.9f*0.05f*Mathf.Min(20, Mathf.Max(0, liters_of_paint)), transform.GetChild(3).localScale.z);

        Renderer meesh = transform.GetChild(3).GetComponent<Renderer>();
        //transform.GetChild(3).localPosition = new Vector3(transform.GetChild(3).localPosition.x, Mathf.Min(-15f, -15.7f + (meesh.bounds.size.y * transform.GetChild(3).localScale.y / 2f)), transform.GetChild(3).localPosition.z);
        transform.GetChild(3).localPosition = new Vector3(transform.GetChild(3).localPosition.x, -15.7f + (meesh.bounds.size.y * transform.GetChild(3).localScale.y / 2f), transform.GetChild(3).localPosition.z);

        y[2] = init_theta;
        theta = init_theta;
        y[3] = init_phi;
        phi = init_phi;
        prev_phi = 0;
        prev_theta = 0;
        pos1 = Vector3.zero;
        pos2 = Vector3.zero;
    }

    void ApplyRotation()
    {
        if(Mathf.Abs(theta - prev_theta) <= 0.005f && Mathf.Abs(theta) <= 0.001f)
        {
            State = 0;
            theta = init_theta;
            phi = 0f;
            prev_theta = 0f;
            prev_phi = 0f;
            y[0] = 0f;
            y[1] = 0.0f;
            y[2] = init_theta;
            y[3] = init_phi;
            tm = 0;
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
        float aphi, atheta = y[1] * y[1] * Mathf.Sin(y[2]) * Mathf.Cos(y[2]) - 9.8f / 4.5f * Mathf.Sin(y[2]);
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
        if (Input.GetKeyUp(KeyCode.U)&&!cam_interface.enabled)
        {
            //transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, 0.9f * 0.05f * Mathf.Min(20, Mathf.Max(0, liters_of_paint)), transform.GetChild(3).localScale.z);
            //line.positionCount = 0;
            //State = 1;
            //theta = init_theta;
            //phi = 0f;
            //prev_theta = 0f;
            //prev_phi = 0f;
            //y[0] = 0f;
            //y[1] = 0.5f;
            //y[2] = init_theta;
            //y[3] = 0f;
            //tm = 0;
            //transform.GetChild(0).transform.localPosition = new Vector3(0f, -15f, 0f);
            //transform.GetChild(2).transform.localPosition = new Vector3(0f, -15f, 0f);
            //transform.GetChild(3).transform.localPosition = new Vector3(0f, -15.17f, 0f);
            //transform.eulerAngles = Vector3.zero;
            //transform.GetChild(0).transform.eulerAngles = Vector3.zero;
            clear = true;
            reset_vars();
            State = 1;
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            //line.positionCount = 0;
            //State = 0;
            //theta = init_theta;
            //phi = 0f;
            //prev_theta = 0f;
            //prev_phi = 0f;
            //y[0] = 0f;
            //y[1] = 0.5f;
            //y[2] = init_theta;
            //y[3] = 0f;
            //tm = 0;
            //transform.GetChild(0).transform.localPosition = new Vector3(0f, -15f, 0f);
            //transform.GetChild(2).transform.localPosition = new Vector3(0f, -15f, 0f);
            //transform.GetChild(3).transform.localPosition = new Vector3(0f, -15.17f, 0f);
            //transform.eulerAngles = Vector3.zero;
            //transform.GetChild(0).transform.eulerAngles = Vector3.zero;
            reset_vars();
            clear = true;
        }
        if (Input.GetKeyUp(KeyCode.I) && !cam_interface.enabled)
        {
            if(cam_main.enabled)
            {
                cam_up.enabled = true;
                cam_main.enabled = false;
            }
            else if (cam_up.enabled)
            {
                cam_up.enabled = false;
                cam_good.enabled = true;
            }
            else if (cam_good.enabled)
            {
                cam_good.enabled = false;
                cam_main.enabled = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            cam_up.enabled = false;
            cam_good.enabled = false;
            cam_main.enabled = false;
            cam_interface.enabled = true;
            reset_vars();
            clear = true;
        }
        if (State == 1)
        {
            for(int q=0;q<speedup;q++)
            {
                transform.GetChild(3).localScale = new Vector3(transform.GetChild(3).localScale.x, Mathf.Max(0, transform.GetChild(3).localScale.y - (1/(3000f - 400f * (tracks-1)))), transform.GetChild(3).localScale.z);
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
    }
}