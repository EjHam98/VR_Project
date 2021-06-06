using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject WaterParticle;
    public bool simul = false;

    private GameObject drop;

    public GameObject PhysicsCalc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Spawn()
    {
        WaterParticle.GetComponent<WaterParticle>().pcobj = PhysicsCalc;
        drop = Instantiate(WaterParticle) as GameObject;
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