﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Rot : MonoBehaviour
{
    private const float bucket_mass = 0.5f;
    private const float paint_liter_mass = 1.354f;
    private const float surface = 0.48f;
    private const float rope_length = 12.5f; //0.8m

    private int State = 0;

    private Vector3 eulerAngs, eulerAngs1, eulerAngs2;
    private float total_mass;

    private float theangle = 0.25f;

    public int liters_of_paint = 18;
    private float velocity = 0f;
    private float r = 0.1f;

    private float v1 = 0f;
    private float v2 = 0f;
    private float theta_max1 = 60f;
    private float theta_max2 = 60f;

    private float theta = 0.35f;
    private float phi = 0f;
    private float prev_theta = 0f;
    private float prev_phi = 0f;
    private float vtheta = 0f;
    private float vphi = 0.5f;
    private float atheta = 0f;
    private float aphi = 0f;

    private float old_th;
    private float old_ph;

    private float tm = 0.0f;
    private float delta_t = 0.02f;
    private float[] y = { 0.0f, 0.5f, 0.35f, 0f };

    public int randr = 3;
    public float prand = 0.1f;

    private float curtime;

    private PhysicsCode PhysicsCalc = null;

    // Start is called before the first frame update
    void Start()
    {


        total_mass = bucket_mass + liters_of_paint * paint_liter_mass;

        eulerAngs = new Vector3(0f, -1f * y[3] * 180f, -1f * y[2] * 180f);

        //transform.Rotate(Vector3.forward, eulerAngs.z, Space.World);
        //transform.Rotate(Vector3.up, eulerAngs.y, Space.World);
        y[2] = theangle;
        theta = theangle;
        prev_phi = 0;
        prev_theta = 0;
        //ApplyMovement();
        //transform.GetChild(0).transform.Rotate(Vector3.up, y[3] * 180f, Space.Self);
        //transform.GetChild(0).transform.Rotate(Vector3.forward, -1f * y[2] * 180f, Space.Self);

    }

    void ApplyRotation()
    {
        transform.Rotate(Vector3.up, (phi - prev_phi) * 60f, Space.World);
        transform.Rotate(Vector3.forward, (theta - prev_theta) * 180f, Space.Self);
    }

    float[] G(float[] y, float t)
    {
        atheta = y[1] * y[1] * Mathf.Sin(y[2]) * Mathf.Cos(y[2]) - 9.8f / 4.5f * Mathf.Sin(y[2]);
        aphi = -2f * y[0] * y[1] * (1f / Mathf.Tan(y[2]));
        return new float[4] { atheta, aphi, y[0], y[1] };
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
            theta = 0.5f;
            phi = 0f;
            prev_theta = 0f;
            prev_phi = 0f;
            y[0] = 0f;
            y[1] = 0.5f;
            y[2] = 0.5f;
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
        else if (State == 2)
        {
            ApplyMovement();
            tm += delta_t;
            float[] tmp = RK4Step(y, tm, delta_t);
            for (int i = 0; i < 4; i++)
            {
                y[i] += tmp[i];
            }
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