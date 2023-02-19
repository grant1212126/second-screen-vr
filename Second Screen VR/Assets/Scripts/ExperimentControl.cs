using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using TMPro;

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

    public int[] videoOrder;

    public AudioSource audioSource;

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

        enableObject(mainScreen);

        disableObject(monitorScreen);
        disableObject(phoneScreen);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableObject(GameObject item)
    {
        item.SetActive(true);
    }

    public void disableObject(GameObject item)
    {
        item.SetActive(false);
    }

    public void startExperiment()
    {
        if (GameObject.Find("Key Pad Input").GetComponent<TMP_InputField>().text == "" || GameObject.Find("Video Order Input").GetComponent<TMP_InputField>().text == "")
        {
            return;
        }

        string videoOrderNumber = GameObject.Find("Video Order Input").GetComponent<TMP_InputField>().text;

        videoOrder = new int[4];

        switch(videoOrderNumber)
        {
            case "1":
                videoOrder[0] = 0;
                videoOrder[1] = 1;
                videoOrder[2] = 2;
                videoOrder[3] = 3;
                break;

            case "2":
                videoOrder[0] = 3;
                videoOrder[1] = 0;
                videoOrder[2] = 1;
                videoOrder[3] = 2;
                break;

            case "3":
                videoOrder[0] = 2;
                videoOrder[1] = 3;
                videoOrder[2] = 0;
                videoOrder[3] = 1;
                break;

            case "4":
                videoOrder[0] = 1;
                videoOrder[1] = 2;
                videoOrder[2] = 3;
                videoOrder[3] = 0;
                break;

        }

        disableObject(GameObject.Find("Canvas"));

        StartCoroutine(startSection(videoOrder[0]));

        StartCoroutine(startSection(Screens[0], videoOrder[1]));

        StartCoroutine(startSection(Screens[1], videoOrder[2]));

        StartCoroutine(startSection(Screens[2], videoOrder[3]));
    }

    IEnumerator startSection(GameObject Secondscreen, int videoNumber)
    {

        //disableObject(Canvas);

        enableObject(Secondscreen);

        int length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);
        PlayVideo(Secondscreen.GetComponent<VideoPlayer>(), factClips[videoNumber]);

        yield return new WaitForSeconds(length);

        disableObject(Secondscreen);

        yield return new WaitForSeconds(10);

        //enableObject(Canvas);

    }

    IEnumerator startSection(int videoNumber)
    {

        int length = PlayVideo(mainScreen.GetComponent<VideoPlayer>(), videoClips[videoNumber]);

        yield return new WaitForSeconds(length);

    }

    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public int PlayVideo(VideoPlayer player, VideoClip clip)
    {
        player.clip = clip;

        player.Play();

        return ((int)player.clip.length);
    }

}
 