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
    public VideoPlayer videoPlayer;
    GameObject button;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        Debug.Log("Awake");

        userControls = new XRIDefaultInputActions();

        userControls.XRILeftHandInteraction.Activate.performed += _ => Pause();
        userControls.XRIRightHandInteraction.Activate.performed += _ => Pause();
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
        if (videoPlayer.isPaused)
        {
            videoPlayer.Play();
            Debug.Log("Video Unpaused");
        }
        else
        {
            videoPlayer.Pause();
            Debug.Log("Video Paused");
        }
    }
}
