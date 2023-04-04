using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Video;
using UnityEngine;
using TMPro;

/*
 * Methods and variables associated with the main experiment controller object. Controls the objects associated 
 * with the experiment and calls the methods to run through the experiment.
 */

public class ExperimentControl : MonoBehaviour
{
    /*
     * Instantiation of variables, these need to be allocated values and objects using the unity editor
     */

    public ViewRaycastController raycastController;

    public GameObject mainScreen;
    public GameObject sideScreen;
    public GameObject monitorScreen;
    public GameObject phoneScreen;

    public GameObject backgroundPlanes;

    public GameObject IntroCanvas;
    public GameObject PostClipCanvas;

    public GameObject[] Screens;

    public VideoClip[] videoClips;
    public VideoClip[] factClips;
    public VideoClip[] factClipsPhone;

    public int[] factTimes1;
    public int[] factTimes2;
    public int[] factTimes3;
    public int[] factTimes4;

    public int[][] factTimes;

    public XRController controller;

    int videoCount;

    void Awake()
    {
        IntroCanvas = GameObject.Find("Introduction Canvas");
        PostClipCanvas = GameObject.Find("Post Clip Canvas");

        Screens = new GameObject[4];
        factTimes = new int[4][];

        videoCount = 0;
        
        enableObject(mainScreen);
        enableObject(IntroCanvas);

        disableMainScreenBoundingBox();

        disableObject(backgroundPlanes);
        disableObject(monitorScreen);
        disableObject(sideScreen);
        disableObject(PostClipCanvas);

    }

    public void enableObject(GameObject item)
    {
        item.SetActive(true);
    }

    public void disableObject(GameObject item)
    {
        item.SetActive(false);
    }

    // Enables the raycast component for the VR controller

    IEnumerator enableController(XRController item, int length)
    {
        yield return new WaitForSeconds(length);
        item.GetComponent<XRInteractorLineVisual>().enabled = true;
    }

    // Disables raycast component for the VR controller 

    public void disableController(XRController item)
    {
        item.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    public void enableMainScreenBoundingBox()
    {
        mainScreen.GetComponent<XRSimpleInteractable>().enabled = true;
    }

    public void disableMainScreenBoundingBox()
    {
        mainScreen.GetComponent<XRSimpleInteractable>().enabled = false;
    }

    /*
     * Method used to organize the order in which users are presented the video content, takes in the experimental order number from 1 - 4
     * and orders each video corresponding to the experimental configuration for that number
     */

    public void organiseVideoAndScreenOrder(string videoOrderNumber)
    {

        VideoClip[] videoOrder = new VideoClip[4];
        VideoClip[] factOrder = new VideoClip[4];
        VideoClip[] factOrderPhone = new VideoClip[4];

        videoClips.CopyTo(videoOrder, 0);
        factClips.CopyTo(factOrder, 0);
        factClipsPhone.CopyTo(factOrderPhone, 0);

        switch (videoOrderNumber)
        {
            case "1": 
                
                Screens[0] = null;
                Screens[1] = sideScreen;
                Screens[2] = monitorScreen;
                Screens[3] = phoneScreen;

                videoClips[0] = videoOrder[0];
                videoClips[1] = videoOrder[1];
                videoClips[2] = videoOrder[2];
                videoClips[3] = videoOrder[3];

                factTimes[0] = factTimes1;
                factTimes[1] = factTimes2;
                factTimes[2] = factTimes3;
                factTimes[3] = factTimes4;

                factClips[0] = factOrder[0];
                factClips[1] = factOrder[1];
                factClips[2] = factOrder[2];
                factClips[3] = factOrderPhone[3];

                break;
            case "2":
                
                Screens[0] = sideScreen;
                Screens[1] = null;
                Screens[2] = phoneScreen;
                Screens[3] = monitorScreen;

                videoClips[0] = videoOrder[3];
                videoClips[1] = videoOrder[2];
                videoClips[2] = videoOrder[1];
                videoClips[3] = videoOrder[0];

                factTimes[0] = factTimes4;
                factTimes[1] = factTimes3;
                factTimes[2] = factTimes2;
                factTimes[3] = factTimes1;

                factClips[0] = factOrder[3];
                factClips[1] = factOrder[2];
                factClips[2] = factOrderPhone[1];
                factClips[3] = factOrder[0];
                break;
            case "3":
                
                Screens[0] = monitorScreen;
                Screens[1] = phoneScreen;
                Screens[2] = null;
                Screens[3] = sideScreen;

                videoClips[0] = videoOrder[1];
                videoClips[1] = videoOrder[0];
                videoClips[2] = videoOrder[3];
                videoClips[3] = videoOrder[2];

                factTimes[0] = factTimes2;
                factTimes[1] = factTimes1;
                factTimes[2] = factTimes4;
                factTimes[3] = factTimes3;

                factClips[0] = factOrder[1];
                factClips[1] = factOrderPhone[0];
                factClips[2] = factOrder[3];
                factClips[3] = factOrder[2];
                break;
            case "4":
                
                Screens[0] = phoneScreen;
                Screens[1] = monitorScreen;
                Screens[2] = sideScreen;
                Screens[3] = null;

                videoClips[0] = videoOrder[2];
                videoClips[1] = videoOrder[3];
                videoClips[2] = videoOrder[0];
                videoClips[3] = videoOrder[1];

                factTimes[0] = factTimes3;
                factTimes[1] = factTimes4;
                factTimes[2] = factTimes1;
                factTimes[3] = factTimes2;

                factClips[0] = factOrderPhone[2];
                factClips[1] = factOrder[3];
                factClips[2] = factOrder[0];
                factClips[3] = factOrder[1];
                break;

        }
    }

    /*
     * Prepares and starts the experiment to start by parsing the users participant number and experimental configuration number
     */

    public void startExperiment()
    {

        string participantNumber = GameObject.Find("Key Pad Input").GetComponent<TMP_InputField>().text;
        string experimentNumber = GameObject.Find("Video Order Input").GetComponent<TMP_InputField>().text;

        if (participantNumber == "" || experimentNumber == "")
        {
            Debug.Log("Error, you are missing either a participant number or a video order number");
            return;
        }


        if (!InitializeLogFile(participantNumber, experimentNumber))
        {
            Debug.Log("Experiment halted, error initializing log file");
            return;
        }

        disableObject(phoneScreen);

        string videoOrderNumber = GameObject.Find("Video Order Input").GetComponent<TMP_InputField>().text;

        organiseVideoAndScreenOrder(videoOrderNumber);
        
        disableObject(IntroCanvas);

        enableObject(PostClipCanvas);

    }

    /*
     * Starts the next section of the experiment, takes in the screen to display the additional information, the video number corresponding
     * to the video to play and the timestamps that the facts appear on the side screen
     */

    IEnumerator startSection(GameObject Secondscreen, int videoNumber, int[] factTime)
    {
        DataLogControler.Instance.WriteBreakInFile();

        DataLogControler.Instance.WriteToFile("User has started video number: " + videoNumber + " " + DateTime.Now);

        enableMainScreenBoundingBox();

        enableObject(Secondscreen);
        enableObject(backgroundPlanes);

        disableController(controller);

        int length = 0;

        
        length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber], null);
        PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factClips[videoNumber], factTimes[videoNumber]);
        

        yield return new WaitForSeconds(length);

        videoCount = videoCount + 1;

        disableObject(Secondscreen);
        disableObject(backgroundPlanes);
        disableMainScreenBoundingBox();

        DataLogControler.Instance.WriteToFile("User has finished video number: " + videoNumber + " " + DateTime.Now);

        raycastController.writeTotalSectionViewingTimes();

        enableObject(PostClipCanvas);

        StartCoroutine(enableController(controller, 20));

        if (videoCount > 3)
        {
            enableObject(PostClipCanvas);

            GameObject.Find("Post Clip Canvas Text").GetComponent<TMP_Text>().text = "The experiment has now finished, thank you for participating! Please now take off the headset to be debriefed";

            disableObject(GameObject.Find("Post Clip Start Button"));

            DataLogControler.Instance.WriteToFile("Experiment finished " + " " + DateTime.Now);

            DataLogControler.Instance.WriteBreakInFile();

            raycastController.writeTotalViewingTimes();

        }

    }

    /*
     * Starts the next section of the experiment, this version of the method is called when no second screen displaying information
     * is present for that section. Enables bounding boxes and writes gaze statistics to datalogger component.
     */

    IEnumerator startSection(int videoNumber)
    {

        DataLogControler.Instance.WriteBreakInFile();

        DataLogControler.Instance.WriteToFile("User has started video number: " + videoNumber + " " + DateTime.Now);

        enableMainScreenBoundingBox();

        enableObject(backgroundPlanes);

        disableController(controller);

        int length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber], null);

        yield return new WaitForSeconds(length);

        videoCount = videoCount + 1;

        disableMainScreenBoundingBox();
        disableObject(backgroundPlanes);

        DataLogControler.Instance.WriteToFile("User has finished video number: " + videoNumber + " " + DateTime.Now);

        raycastController.writeTotalSectionViewingTimes();

        enableObject(PostClipCanvas);

        StartCoroutine(enableController(controller, 20));

    }

    /*
     * Method attached to button on the display shown to the user either at the start of the experiment or when a section has been
     * completed. Disables the canvas and continues the experiment
     */

    public void continueExperiment()
    {
        disableObject(PostClipCanvas);

        if (Screens[videoCount] == null)
        {
            StartCoroutine(startSection(videoCount));
        } else
        { 
        StartCoroutine(startSection(Screens[videoCount], videoCount, factTimes[videoCount]));
        }
    }

    /*
     * Sends a haptic pulse to the VR controller used in the experiment after a number of seconds
     */

    IEnumerator sendHaptic(int factTime)
    {
        yield return new WaitForSeconds(factTime);
        
        controller.GetComponent<XRController>().SendHapticImpulse(0.75f, 2f);
    }

    /*
     * Plays a given video clip on the given screen, if factTime variable is present then the time
     * in the array is used to send a haptic pulse for when additional information is shown on the second screen
     */

    public int PlayVideo(VideoPlayer player, VideoClip clip, int[] factTime)
    {
        if (factTime != null)
        {
            foreach (int i in factTime)
            {
                StartCoroutine(sendHaptic(i));
            }
        } 

        player.clip = clip;

        player.Play();

        return ((int)player.clip.length);
    }

    /*
     * Initializes the log file used to document how long the user looked at each bounding box
     */

    public bool InitializeLogFile(string participantNumber, string experimentNumber)
    {
        if (!DataLogControler.Instance.CreateNewLog(participantNumber + ".txt"))
        {
            Debug.Log("Issue creating new Log File");
            return false;
        } else
        {
            DataLogControler.Instance.WriteToFile("Participant: " + participantNumber + " "  + "Experiment Video Order: " + experimentNumber + " " + DateTime.Now);
        }

        return true;
    }

}
 