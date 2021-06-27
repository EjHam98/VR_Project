using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInputOnClick : MonoBehaviour
{
    
    //get input filed
    public InputField inputUser1;
    public InputField inputUser2;
    public InputField inputUser3;
    public InputField inputUser4;

    public Button btnClick;

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
        Debug.Log("Log input: " + inputUser1.text);
        Debug.Log("Log input: " + inputUser2.text);
        Debug.Log("Log input: " + inputUser3.text);
        Debug.Log("Log input: " + inputUser4.text);
    }
}
