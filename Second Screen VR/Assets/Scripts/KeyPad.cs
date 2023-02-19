using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
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

    public void keyPad0()
    {
        inputField.text = inputField.text + "0";
    }

    public void keyPad1()
    {
        inputField.text = inputField.text + "1";
    }

    public void keyPad2()
    {
        inputField.text = inputField.text + "2";
    }

    public void keyPad3()
    {
        inputField.text = inputField.text + "3";
    }

    public void keyPad4()
    {
        inputField.text = inputField.text + "4";
    }

    public void keyPad5()
    {
        inputField.text = inputField.text + "5";
    }

    public void keyPad6()
    {
        inputField.text = inputField.text + "6";
    }

    public void keyPad7()
    {
        inputField.text = inputField.text + "7";
    }

    public void keyPad8()
    {
        inputField.text = inputField.text + "8";
    }

    public void keyPad9()
    {
        inputField.text = inputField.text + "9";
    }

    public void keyPadBackSpace()
    {
        if (inputField.text.Length < 1)
        {
            return;
        }
        else
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
        }
    }
}
