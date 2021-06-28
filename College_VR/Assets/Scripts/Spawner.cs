using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject WaterParticle;
    public GameObject pend;

    public bool simul = false;

    public int speedup=1;

    public int steps = 9;
    public float step_size = 0.05f;

    private GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        steps = 9;
        step_size = 0.01f;
        speedup = 10;
    }

    private void Spawn()
    {
        //float x, y, z;
        ////float teta = pend.transform.eulerAngles.y;
        ////float fai = pend.transform.eulerAngles.z;
        Rot code = pend.GetComponent<Rot>();
        float teta = code.phi;
        float fai = code.theta;
        //x = -15f * Mathf.Sin(teta) * Mathf.Cos(fai);
        //y = -15f * Mathf.Sin(teta) * Mathf.Sin(fai);
        //z = -15f * Mathf.Cos(teta)+19;
        drop = Instantiate(WaterParticle) as GameObject;
        drop.transform.position = transform.position;// +new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        
        //Vector3 curpos = drop.transform.localPosition;
        ////curpos = curpos + new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        ////curpos = new Vector3(x, y+5, z)+ new Vector3(step_size * Random.Range(-1 * steps, steps), /*step_size * Random.Range(-1 * steps, steps)*/0, step_size * Random.Range(-1 * steps, steps));
        //curpos = curpos + new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        //drop.transform.localPosition = curpos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            simul = !simul;
        }
        if (simul)
        {
            for (int i = 0; i < speedup; i++)
            {
                Spawn();
            }
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