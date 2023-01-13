using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class VideoControl : MonoBehaviour
{

    private XRIDefaultInputActions userControls;
    public GameObject[] Screens;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        Debug.Log("Awake");

        userControls = new XRIDefaultInputActions();

        //userControls.XRILeftHandInteraction.Activate.performed += _ => Pause();
        //userControls.XRIRightHandInteraction.Activate.performed += _ => Pause();
        userControls.KeyboardInteractions.Space.performed += _ => Pause();
    }

    void OnEnable()
    {
        userControls.XRIRightHandInteraction.Enable();
        userControls.XRILeftHandInteraction.Enable();
        userControls.KeyboardInteractions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Debug.Log("This is a test");
    }

    public void Pause()
    {

        Screens = GetComponent<ExperimentControl>().Screens;

        for (int i = 0; i <= Screens.Length; i++)
        {
            if (Screens[i].GetComponent<ScreenActive>().isActive) {
            

                if (Screens[i].GetComponent<VideoPlayer>().isPaused)
                {
                    Screens[i].GetComponent<VideoPlayer>().Play();
                    Debug.Log("Video Unpaused");
                }
                else
                {
                    Screens[i].GetComponent<VideoPlayer>().Pause();
                    Debug.Log("Video Paused");
                }
            }
        }

    }

}
