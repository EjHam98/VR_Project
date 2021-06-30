using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetInputOnClick : MonoBehaviour
{
    
    //get input filed
    public InputField inputUser1;
    public InputField inputUser2;
    public InputField inputUser3;
    public InputField inputUser4;
    public InputField inputUser5;

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
        Display.displays[0].Activate();
        GameObject pend = GameObject.Find("Pendulum");
        Debug.Log(pend);
        Rot code = pend.GetComponent<Rot>();
        code.theta = int.Parse(inputUser1.text)/180f;
        code.phi = int.Parse(inputUser2.text)/180f;
        code.liters_of_paint = Mathf.Min(20, Mathf.Max(0, int.Parse(inputUser3.text)));
        code.damping = Mathf.Min(9, Mathf.Max(0, int.Parse(inputUser4.text)));
        //code.theta = float.Parse(inputUser1.text);
    }
}
