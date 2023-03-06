using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Video;
using UnityEngine;
using TMPro;


public class ExperimentControl : MonoBehaviour
{
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

    int videoCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {

        IntroCanvas = GameObject.Find("Introduction Canvas");
        PostClipCanvas = GameObject.Find("Post Clip Canvas");

        Screens = new GameObject[4];

        videoCount = 0;
        
        enableObject(mainScreen);
        enableObject(IntroCanvas);

        disableMainScreenBoundingBox();

        disableObject(backgroundPlanes);
        disableObject(monitorScreen);
        disableObject(sideScreen);
        disableObject(PostClipCanvas);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableObject(GameObject item)
    {
        item.SetActive(true);
    }

    public void enableMainScreenBoundingBox()
    {
        mainScreen.GetComponent<XRSimpleInteractable>().enabled = true;
    }

    public void disableMainScreenBoundingBox()
    {
        mainScreen.GetComponent<XRSimpleInteractable>().enabled = false;
    }

    public void disableObject(GameObject item)
    {
        item.SetActive(false);
    }

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

                factClips[0] = factOrderPhone[2];
                factClips[1] = factOrder[3];
                factClips[2] = factOrder[0];
                factClips[3] = factOrder[1];
                break;

        }
    }

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

    IEnumerator startSection(GameObject Secondscreen, int videoNumber)
    {
        DataLogControler.Instance.WriteBreakInFile();

        DataLogControler.Instance.WriteToFile("User has started video number: " + videoNumber + " " + DateTime.Now);

        enableMainScreenBoundingBox();

        enableObject(Secondscreen);
        enableObject(backgroundPlanes);

        int length = 0;

        
        length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);
        PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factClips[videoNumber]);
        

        yield return new WaitForSeconds(length);

        videoCount = videoCount + 1;

        disableObject(Secondscreen);
        disableObject(backgroundPlanes);
        disableMainScreenBoundingBox();

        DataLogControler.Instance.WriteToFile("User has finished video number: " + videoNumber + " " + DateTime.Now);

        raycastController.writeTotalSectionViewingTimes();

        enableObject(PostClipCanvas);

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

    IEnumerator startSection(int videoNumber)
    {

        DataLogControler.Instance.WriteBreakInFile();

        DataLogControler.Instance.WriteToFile("User has started video number: " + videoNumber + " " + DateTime.Now);

        enableMainScreenBoundingBox();

        enableObject(backgroundPlanes);

        int length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);

        yield return new WaitForSeconds(length);

        videoCount = videoCount + 1;

        disableMainScreenBoundingBox();
        disableObject(backgroundPlanes);

        DataLogControler.Instance.WriteToFile("User has finished video number: " + videoNumber + " " + DateTime.Now);

        raycastController.writeTotalSectionViewingTimes();


        enableObject(PostClipCanvas);

    }

    public void continueExperiment()
    {
        disableObject(PostClipCanvas);

        if (Screens[videoCount] == null)
        {
            StartCoroutine(startSection(videoCount));
        } else
        { 
        StartCoroutine(startSection(Screens[videoCount], videoCount));
        }
    }

    public int PlayVideo(VideoPlayer player, VideoClip clip)
    {
        player.clip = clip;

        player.Play();

        return ((int)player.clip.length);
    }

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
 