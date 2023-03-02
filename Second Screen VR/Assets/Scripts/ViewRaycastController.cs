using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRaycastController : MonoBehaviour
{
    public TimeSpan totalMainScreenTime;
    public TimeSpan totalSecondScreenTime;
    public TimeSpan totalBackgroundTime;

    public Stopwatch mainScreenStopwatch;
    public Stopwatch secondScreenStopwatch;
    public Stopwatch backgroundStopwatch;

    public 

    // Start is called before the first frame update
    void Awake()
    {
        mainScreenStopwatch = new Stopwatch();
        secondScreenStopwatch = new Stopwatch();
        backgroundStopwatch = new Stopwatch();

    }

    public void writeTotalViewingTimes()
    {
        
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            totalMainScreenTime.Hours, totalMainScreenTime.Minutes, totalMainScreenTime.Seconds,
            totalMainScreenTime.Milliseconds / 10);

        DataLogControler.Instance.WriteToFile("Total time viewing main screen: " +  elapsedTime);

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            totalBackgroundTime.Hours, totalBackgroundTime.Minutes, totalBackgroundTime.Seconds,
            totalBackgroundTime.Milliseconds / 10);

        DataLogControler.Instance.WriteToFile("Total time viewing background: " + elapsedTime);

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            totalSecondScreenTime.Hours, totalSecondScreenTime.Minutes, totalSecondScreenTime.Seconds,
            totalSecondScreenTime.Milliseconds / 10);

        DataLogControler.Instance.WriteToFile("Total time viewing second Screens: " + elapsedTime);

        DataLogControler.Instance.WriteToFile("Application quit. " + DateTime.Now);
    }

    public void test(int tet)
    {
        UnityEngine.Debug.Log("Test");
    }

    public void enterMainScreen()
    {
        mainScreenStopwatch.Start();
        UnityEngine.Debug.Log("User has entered main screen bounding box");

        DataLogControler.Instance.WriteToFile("User has entered main screen bounding box" + " - " + DateTime.Now);
    }

    public void exitMainScreen()
    {
        mainScreenStopwatch.Stop();
        TimeSpan ts = mainScreenStopwatch.Elapsed;
        mainScreenStopwatch.Reset();

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        UnityEngine.Debug.Log("User has exited the main screen bounding box, they were in there for: " + elapsedTime);

        totalMainScreenTime = totalMainScreenTime + ts;

        DataLogControler.Instance.WriteToFile("User has exited the main screen bounding box, they were in there for: " + elapsedTime + " - " + DateTime.Now);
        DataLogControler.Instance.WriteToFile(System.Environment.NewLine);
    }

    public void enterBackground()
    {
        backgroundStopwatch.Start();
        UnityEngine.Debug.Log("User has entered background bounding box");

        DataLogControler.Instance.WriteToFile("User has entered background bounding box" + " - " + DateTime.Now);


    }

    public void exitBackground()
    {
        backgroundStopwatch.Stop();
        TimeSpan ts =backgroundStopwatch.Elapsed;
        backgroundStopwatch.Reset();

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        UnityEngine.Debug.Log("User has exited the background bounding box, they were in there for: " + elapsedTime);

        totalBackgroundTime = totalBackgroundTime + ts;

        DataLogControler.Instance.WriteToFile("User has exited the background bounding box, they were in there for: " + elapsedTime + " - " + DateTime.Now);
        DataLogControler.Instance.WriteToFile(System.Environment.NewLine);
    }


    public void enterSecondScreen()
    {
        secondScreenStopwatch.Start();
        UnityEngine.Debug.Log("User has entered one of the second screen bounding boxes");

        DataLogControler.Instance.WriteToFile("User has entered one of the screen bounding boxes" + " - " + DateTime.Now);

    }

    public void exitSecondScreen()
    {
        secondScreenStopwatch.Stop();
        TimeSpan ts = secondScreenStopwatch.Elapsed;
        secondScreenStopwatch.Reset();

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        UnityEngine.Debug.Log("User has exited one of the second screen bounding boxes, they were in there for: " + elapsedTime);

        totalSecondScreenTime = totalSecondScreenTime + ts;

        DataLogControler.Instance.WriteToFile("User has exited one of the secondary screen bounding boxes, they were in there for: " + elapsedTime + " - " + DateTime.Now);
        DataLogControler.Instance.WriteToFile(System.Environment.NewLine);
    }

} 
