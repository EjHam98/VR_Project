using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject WaterParticle;

    public bool simul = false;

    public int steps = 9;
    public float step_size = 0.05f;

    private GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        steps = 9;
        step_size = 0.01f;
    }

    private void Spawn()
    {
        drop = Instantiate(WaterParticle) as GameObject;
        Vector3 curpos = drop.transform.localPosition;
        curpos = curpos + new Vector3(step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps), step_size * Random.Range(-1 * steps, steps));
        drop.transform.localPosition = curpos;
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
            Spawn();
        }
        else
        {
            Destroy(drop);
        }
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