using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerEnterHandler
{

    public InterfaceButton pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        //pauseButton.GetComponent<InterfaceButton>().disableButton(pauseButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pauseButton.GetComponent<InterfaceButton>().disableButton(pauseButton);
    }
}
