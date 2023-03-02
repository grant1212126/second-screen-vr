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

        Screens = new GameObject[3];

        Screens[0] = sideScreen;
        Screens[1] = monitorScreen;
        Screens[2] = phoneScreen;

        IntroCanvas = GameObject.Find("Introduction Canvas");
        PostClipCanvas = GameObject.Find("Post Clip Canvas");

        videoCount = 0;
        
        enableObject(mainScreen);
        enableObject(IntroCanvas);

        enableBackgroundBoundingBoxes();

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

    public void enableBackgroundBoundingBoxes()
    {
        mainScreen.GetComponent<XRSimpleInteractable>().enabled = true;
    }

    public void disableBackgroundBoundingBoxes()
    {
        mainScreen.GetComponent<XRSimpleInteractable>().enabled = false;
    }

    public void disableObject(GameObject item)
    {
        item.SetActive(false);
    }

    public void organiseVideoOrder(string videoOrderNumber)
    {

        int mod = 0;

        VideoClip[] videoOrder = new VideoClip[4];
        VideoClip[] factOrder = new VideoClip[4];
        VideoClip[] factOrderPhone = new VideoClip[4];

        videoClips.CopyTo(videoOrder, 0);
        factClips.CopyTo(factOrder, 0);
        factClipsPhone.CopyTo(factOrderPhone, 0);

        switch (videoOrderNumber)
        {
            case "1":
                mod = 0;
                break;
            case "2":
                mod = 3;
                break;
            case "3":
                mod = 2;
                break;
            case "4":
                mod = 1;
                break;

        }

        for (int i = 0; i < 4; i = i +1)
        {
            videoClips[i] = videoOrder[mod];
            factClips[i] = factOrder[mod];
            factClipsPhone[i] = factOrderPhone[mod];
            mod = (mod + 1) % 4;
            Debug.Log(mod);
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

        organiseVideoOrder(videoOrderNumber);
        
        disableObject(IntroCanvas);

        enableObject(PostClipCanvas);

    }

    IEnumerator startSection(GameObject Secondscreen, int videoNumber)
    {
        DataLogControler.Instance.WriteBreakInFile();

        DataLogControler.Instance.WriteToFile("User has started video number: " + videoNumber + " " + DateTime.Now);

        enableBackgroundBoundingBoxes();

        enableObject(Secondscreen);
        enableObject(backgroundPlanes);

        int length = 0;

        if (videoNumber == 3)
        {
            length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);
            PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factClipsPhone[videoNumber]);
        } else
        {
            length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);
            PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factClips[videoNumber]);
        }

        yield return new WaitForSeconds(10);

        videoCount = videoCount + 1;

        disableObject(Secondscreen);
        disableObject(backgroundPlanes);
        disableBackgroundBoundingBoxes();

        DataLogControler.Instance.WriteToFile("User has finished video number: " + videoNumber + " " + DateTime.Now);

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

        enableBackgroundBoundingBoxes();

        enableObject(backgroundPlanes);

        int length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);

        yield return new WaitForSeconds(10);

        videoCount = videoCount + 1;

        disableBackgroundBoundingBoxes();
        disableObject(backgroundPlanes);

        enableObject(PostClipCanvas);

    }

    public void continueExperiment()
    {
        disableObject(PostClipCanvas);

        if (videoCount == 0)
        {
            StartCoroutine(startSection(videoCount));
        } else
        { 
        StartCoroutine(startSection(Screens[videoCount - 1], videoCount));
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
 