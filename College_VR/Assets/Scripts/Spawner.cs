using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject WaterParticle;
    public GameObject pend;

    public bool simul = false;

    public int speedup=5;

    public byte r = 255, g = 255, b = 255;

    private Color paint_c;
    public float thickness;

    public int steps = 9;
    public float step_size = 0.05f;

    public LineRenderer track1, track2, track3, track4, track5, trail1, trail2, trail3, trail4, trail5;

    private GameObject drop;

    public GameObject hole1, hole2, hole3, hole4;

    public GameObject paint_l;

    Rot code;
    // Start is called before the first frame update
    void Start()
    {
        paint_c = new Color32(r, g, b, 255);
        paint_l.GetComponent<Renderer>().material.SetColor("_Color", paint_c);
        thickness = 0.2f;
        code = pend.GetComponent<Rot>();
        steps = 9;
        step_size = 0.01f;
        speedup = 1;

        track1.startColor = paint_c;
        track1.endColor = paint_c;
        track1.startWidth = thickness;
        track1.endWidth = thickness;
        track1.positionCount = 0;

        track2.startColor = paint_c;
        track2.endColor = paint_c;
        track2.startWidth = thickness;
        track2.endWidth = thickness;
        track2.positionCount = 0;
        
        track3.startColor = paint_c;
        track3.endColor = paint_c;
        track3.startWidth = thickness;
        track3.endWidth = thickness;
        track3.positionCount = 0;

        track4.startColor = paint_c;
        track4.endColor = paint_c;
        track4.startWidth = thickness;
        track4.endWidth = thickness;
        track4.positionCount = 0;
        
        track5.startColor = paint_c;
        track5.endColor = paint_c;
        track5.startWidth = thickness;
        track5.endWidth = thickness;
        track5.positionCount = 0;

        trail1.startColor = paint_c;
        trail1.endColor = paint_c;
        trail1.startWidth = thickness;
        trail1.endWidth = thickness;
        trail1.positionCount = 0;

        trail2.startColor = paint_c;
        trail2.endColor = paint_c;
        trail2.startWidth = thickness;
        trail2.endWidth = thickness;
        trail2.positionCount = 0;

        trail3.startColor = paint_c;
        trail3.endColor = paint_c;
        trail3.startWidth = thickness;
        trail3.endWidth = thickness;
        trail3.positionCount = 0;

        trail4.startColor = paint_c;
        trail4.endColor = paint_c;
        trail4.startWidth = thickness;
        trail4.endWidth = thickness;
        trail4.positionCount = 0;

        trail5.startColor = paint_c;
        trail5.endColor = paint_c;
        trail5.startWidth = thickness;
        trail5.endWidth = thickness;
        trail5.positionCount = 0;
    }

    private void Spawn()
    {
        //float x, y, z;
        ////float teta = pend.transform.eulerAngles.y;
        ////float fai = pend.transform.eulerAngles.z;
        float teta = code.phi;
        float fai = code.theta;
        //x = -15f * Mathf.Sin(teta) * Mathf.Cos(fai);
        //y = -15f * Mathf.Sin(teta) * Mathf.Sin(fai);
        //z = -15f * Mathf.Cos(teta)+19;
        for (int i = 0; i < speedup; i++)
        {
            drop = Instantiate(WaterParticle) as GameObject;
            drop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            WaterParticle wp = drop.GetComponent<WaterParticle>();
            wp.line = track1;
            wp.dropping = trail1; 
            if(code.tracks > 1)
            {
                drop = Instantiate(WaterParticle) as GameObject;
                drop.transform.position = new Vector3(hole1.transform.position.x, hole1.transform.position.y, hole1.transform.position.z);
                wp = drop.GetComponent<WaterParticle>();
                wp.line = track2;
                wp.dropping = trail2; 
            }
            if(code.tracks > 2)
            {
                drop = Instantiate(WaterParticle) as GameObject;
                drop.transform.position = new Vector3(hole2.transform.position.x, hole2.transform.position.y, hole2.transform.position.z);
                wp = drop.GetComponent<WaterParticle>();
                wp.line = track3;
                wp.dropping = trail3;
            }
            if (code.tracks > 3)
            {
                drop = Instantiate(WaterParticle) as GameObject;
                drop.transform.position = new Vector3(hole3.transform.position.x, hole3.transform.position.y, hole3.transform.position.z);
                wp = drop.GetComponent<WaterParticle>();
                wp.line = track4;
                wp.dropping = trail4;
            }
            if (code.tracks > 4)
            {
                drop = Instantiate(WaterParticle) as GameObject;
                drop.transform.position = new Vector3(hole4.transform.position.x, hole4.transform.position.y, hole4.transform.position.z);
                wp = drop.GetComponent<WaterParticle>();
                wp.line = track5;
                wp.dropping = trail5;
            }
            //if(code.tracks > 1)
            //{
            //    drop = Instantiate(WaterParticle) as GameObject;
            //    drop.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            //    wp = drop.GetComponent<WaterParticle>();
            //    wp.line = track2;
            //    wp.dropping = trail2; 
            //}
            //if(code.tracks > 2)
            //{
            //    drop = Instantiate(WaterParticle) as GameObject;
            //    drop.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            //    wp = drop.GetComponent<WaterParticle>();
            //    wp.line = track3;
            //    wp.dropping = trail3;
            //}
            //if (code.tracks > 3)
            //{
            //    drop = Instantiate(WaterParticle) as GameObject;
            //    drop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
            //    wp = drop.GetComponent<WaterParticle>();
            //    wp.line = track4;
            //    wp.dropping = trail4;
            //}
            //if (code.tracks > 4)
            //{
            //    drop = Instantiate(WaterParticle) as GameObject;
            //    drop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
            //    wp = drop.GetComponent<WaterParticle>();
            //    wp.line = track5;
            //    wp.dropping = trail5;
            //}
        }
        //drop = Instantiate(WaterParticle) as GameObject;
        //drop.transform.position = transform.position;// +new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        
        //Vector3 curpos = drop.transform.localPosition;
        ////curpos = curpos + new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        ////curpos = new Vector3(x, y+5, z)+ new Vector3(step_size * Random.Range(-1 * steps, steps), /*step_size * Random.Range(-1 * steps, steps)*/0, step_size * Random.Range(-1 * steps, steps));
        //curpos = curpos + new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        //drop.transform.localPosition = curpos;
    }

    // Update is called once per frame
    void Update()
    {
        if(code.clear)
        {
            trail1.positionCount = 0;
            trail2.positionCount = 0;
            trail3.positionCount = 0;
            trail4.positionCount = 0;
            trail5.positionCount = 0;
            track1.positionCount = 0;
            track2.positionCount = 0;
            track3.positionCount = 0;
            track4.positionCount = 0;
            track5.positionCount = 0;
            code.clear = false;
        }
        paint_c = new Color32(r, g, b, 255);

        paint_l.GetComponent<Renderer>().material.SetColor("_Color", paint_c);

        track1.startColor = paint_c;
        track1.endColor = paint_c;

        track2.startColor = paint_c;
        track2.endColor = paint_c;

        track3.startColor = paint_c;
        track3.endColor = paint_c;

        track4.startColor = paint_c;
        track4.endColor = paint_c;

        track5.startColor = paint_c;
        track5.endColor = paint_c;

        trail1.startColor = paint_c;
        trail1.endColor = paint_c;

        trail2.startColor = paint_c;
        trail2.endColor = paint_c;

        trail3.startColor = paint_c;
        trail3.endColor = paint_c;

        trail4.startColor = paint_c;
        trail4.endColor = paint_c;

        trail5.startColor = paint_c;
        trail5.endColor = paint_c;
        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    simul = !simul;
        //}
        if (code.State == 1)
        {
            if(code.transform.GetChild(3).localScale.y > 0)
            {
                Spawn();
            }
        }
        else if(code.State == 0)
        {
            trail1.positionCount = 0;
            trail2.positionCount = 0;
            trail3.positionCount = 0;
            trail4.positionCount = 0;
            trail5.positionCount = 0;
        }
        //else
        //{
        //    Destroy(drop);
        //}
    }
}


////////////////////////////////////////////////////////////////////
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Spawner : MonoBehaviour
//{
//    public GameObject WaterParticle;
//    public bool simul = true;

//    private GameObject drop;

//    public GameObject PhysicsCalc;
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    private void Spawn()
//    {
//        WaterParticle.GetComponent<WaterParticle>().pcobj = PhysicsCalc;
//        drop = Instantiate(WaterParticle) as GameObject;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(Input.GetKeyUp(KeyCode.Space))
//        {
//            simul = !simul;
//            if (simul)
//            {
//                Spawn();
//            }
//            else
//            {
//                Destroy(drop);
//            }
//        }
//    }
//}