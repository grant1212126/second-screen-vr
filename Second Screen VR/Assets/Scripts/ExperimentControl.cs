using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class ExperimentControl : MonoBehaviour
{

    public GameObject mainScreen;
    public GameObject sideScreen;
    public GameObject monitorScreen;
    public GameObject phoneScreen;

    public GameObject[] Screens;
    public VideoClip[] videoClips;

    VideoControl videoController;

    string videoUrl;
    string factsUrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {

        videoUrl = Application.dataPath + "/Videos/video_1.mp4";
        factsUrl = Application.dataPath + "/Videos/facts_1.mp4";

        Screens = new GameObject[4];

        Screens[0] = mainScreen;
        Screens[1] = sideScreen;
        Screens[2] = monitorScreen;
        Screens[3] = phoneScreen;

        enableScreen(mainScreen);

        disableScreen(monitorScreen);
        disableScreen(phoneScreen);


        startSection(sideScreen, 1);

        //startSection(monitorScreen, 2);

        //startSection(phoneScreen, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableScreen(GameObject screen)
    {
        screen.SetActive(true);
    }

    public void disableScreen(GameObject screen)
    {
        screen.SetActive(false);
    }


    public void startSection(GameObject Secondscreen, int videoNumber)
    {
        double length;

        enableScreen(Secondscreen);

        videoUrl = parseVideoNumberIntoPath(videoUrl, videoNumber);
        factsUrl = parseVideoNumberIntoPath(factsUrl, videoNumber);

        length =

        PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factsUrl);
        length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoUrl);

    }

    public double PlayVideo(VideoPlayer player, string videoName)
    {
        player.url = videoName;

        player.Play();

        Debug.Log("Playing video..");

        double len = 100; // placeholder

        return (len);
    }

    public string parseVideoNumberIntoPath(string videoUrl, int videoNumber)
    {
        videoUrl = videoUrl.Remove(videoUrl.Length - 5);

        videoUrl = videoUrl + videoNumber + ".mp4";

        return (videoUrl);
    }

}
