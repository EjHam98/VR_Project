using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetInputOnClick : MonoBehaviour
{
    
    //get input filed
    public InputField input_r;
    public InputField input_g;
    public InputField input_b;
    public InputField input_liters;
    public InputField input_damping;
    public InputField input_trails;

    public Button btnClick;

    public Camera curcam;
    public Camera nextcam;

    private void Start()
    {
        //attach button event 
        btnClick.onClick.AddListener(GetInputOnClickHandler);
    }

//<summary>

    // Method that will run on click

// </summary>
    public void GetInputOnClickHandler()
    {
        //show input text when we click the button
        //Debug.Log("Log input: " + inputUser1.text);
        //Debug.Log("Log input: " + inputUser2.text);
        //Debug.Log("Log input: " + inputUser3.text);
        //Debug.Log("Log input: " + inputUser4.text);
        //Debug.Log("Log input: " + inputUser5.text);
        curcam.enabled = false;
        nextcam.enabled = true;
        //Display.displays[0].Activate();
        GameObject pend = GameObject.Find("Pendulum");
        //Debug.Log(pend);
        Rot code = pend.GetComponent<Rot>();
        GameObject ps = GameObject.Find("ParticleSpawner");
        //Debug.Log(pend);
        Spawner pscode = ps.GetComponent<Spawner>();
        if(input_r.text.Length>0)
        {
            //Debug.Log((byte)Mathf.Min(255, Mathf.Max(0, int.Parse(input_r.text))));
            pscode.r = (byte)Mathf.Min(255, Mathf.Max(0, int.Parse(input_r.text)));
        }
        if (input_g.text.Length > 0)
        {
            //Debug.Log((byte)Mathf.Min(255, Mathf.Max(0, int.Parse(input_g.text))));
            pscode.g = (byte)Mathf.Min(255, Mathf.Max(0, int.Parse(input_g.text)));
        }
        if (input_b.text.Length > 0)
        {
            //Debug.Log((byte)Mathf.Min(255, Mathf.Max(0, int.Parse(input_b.text))));
            pscode.b = (byte)Mathf.Min(255, Mathf.Max(0, int.Parse(input_b.text)));
        }
        if (input_liters.text.Length > 0)
        {
            code.liters_of_paint = Mathf.Min(20, Mathf.Max(0, int.Parse(input_liters.text)));
        }
        if (input_damping.text.Length > 0)
        {
            code.damping = Mathf.Min(9, Mathf.Max(1, int.Parse(input_damping.text)));
        }
        if (input_trails.text.Length > 0)
        {
            code.tracks = Mathf.Min(5, Mathf.Max(1, int.Parse(input_trails.text)));
        }
        code.reset_vars();
        //code.theta = int.Parse(inputUser1.text)/180f;
        //code.phi = int.Parse(inputUser2.text)/180f;
        //code.liters_of_paint = Mathf.Min(20, Mathf.Max(0, int.Parse(inputUser3.text)));
        //code.damping = Mathf.Min(9, Mathf.Max(0, int.Parse(inputUser4.text)));
        //code.theta = float.Parse(inputUser1.text);
    }
}
