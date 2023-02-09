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
    public VideoClip[] factClips;

    VideoControl videoController;

    string videoUrl;
    string factsUrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {

        Screens = new GameObject[4];

        Screens[0] = mainScreen;
        Screens[1] = sideScreen;
        Screens[2] = monitorScreen;
        Screens[3] = phoneScreen;

        enableScreen(mainScreen);

        disableScreen(monitorScreen);
        disableScreen(phoneScreen);


        startSection(sideScreen, 0);

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

        enableScreen(Secondscreen);

        PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);
        PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factClips[videoNumber]);

    }

    public void PlayVideo(VideoPlayer player, VideoClip clip)
    {
        player.clip = clip;

        player.Play();

        Debug.Log(player.clip.length);
    }

}
