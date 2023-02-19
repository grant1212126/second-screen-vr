using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOrder : MonoBehaviour
{

    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void keyPad1()
    {
        inputField.text = "1";
    }

    public void keyPad2()
    {
        inputField.text = "2";
    }

    public void keyPad3()
    {
        inputField.text = "3";
    }

    public void keyPad4()
    {
        inputField.text = "4";
    }
}
