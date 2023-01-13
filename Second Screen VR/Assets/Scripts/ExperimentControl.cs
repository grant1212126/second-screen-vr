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

    VideoControl videoController;

    int videoNumber;
    string videoUrl; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {

        videoUrl = Application.dataPath + "/Videos/video_1.mp4";

        Screens = new GameObject[4];


        Screens[0] = mainScreen;
        Screens[1] = sideScreen;
        Screens[2] = monitorScreen;
        Screens[3] = phoneScreen;

        videoNumber = 1;

        enableScreen(mainScreen);

        disableScreen(monitorScreen);
        disableScreen(phoneScreen);

        startSection(sideScreen, 1);

        startSection(monitorScreen, 1);

        startSection(phoneScreen, 1);

        //startSection(monitorScreen, 2);
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

        length = PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), videoUrl);
        PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoUrl);

        StartCoroutine(waiter(length, Secondscreen));

    }

    IEnumerator waiter(double len, GameObject Secondscreen)
    {
        yield return new WaitForSeconds((int) len);

        endSection(Secondscreen);
    }

    public double PlayVideo(VideoPlayer player, string videoName)
    {
        player.url = videoName;

        player.Play();

        Debug.Log("Playing video..");

        double len = 7; // placeholder

        Debug.Log(len);

        return (len);
    }

    public string parseVideoNumberIntoPath(string videoUrl, int videoNumber)
    {
        videoUrl = videoUrl.Remove(videoUrl.Length -5, 1);

        videoUrl = videoUrl + videoNumber + ".mp4";

        return (videoUrl);
    }

    public void endSection(GameObject screen)
    {
        videoNumber += 1;

        videoUrl = parseVideoNumberIntoPath(videoUrl, videoNumber);

        disableScreen(screen);
    }

}
