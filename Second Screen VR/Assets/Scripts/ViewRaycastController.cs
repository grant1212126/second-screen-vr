using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Methods used to control the raycast from the users gaze to gather information on what they are
 * looking at to be recorded to a datalog. 
 */

public class ViewRaycastController : MonoBehaviour
{
    public TimeSpan totalMainScreenTime;
    public TimeSpan totalSecondScreenTime;
    public TimeSpan totalBackgroundTime;

    public TimeSpan totalMainScreenSectionTime;
    public TimeSpan totalSecondScreenSectionTime;
    public TimeSpan totalBackgroundSectionTime;

    public Stopwatch mainScreenStopwatch;
    public Stopwatch secondScreenStopwatch;
    public Stopwatch backgroundStopwatch;

    // Start is called before the first frame update
    void Awake()
    {
        mainScreenStopwatch = new Stopwatch();
        secondScreenStopwatch = new Stopwatch();
        backgroundStopwatch = new Stopwatch();

        totalMainScreenSectionTime = System.TimeSpan.Zero;
        totalSecondScreenSectionTime = System.TimeSpan.Zero;
        totalBackgroundSectionTime = System.TimeSpan.Zero;

        totalMainScreenTime = System.TimeSpan.Zero;
        totalSecondScreenTime = System.TimeSpan.Zero;
        totalBackgroundTime = System.TimeSpan.Zero;

    }

    // Writes the total viewing times for all of the screens, background and main screen at the end of the experiment

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

    // Writes the total viewing times for each screen (if applicable), the background and main screen for a given section

    public void writeTotalSectionViewingTimes()
    {

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            totalMainScreenSectionTime.Hours, totalMainScreenSectionTime.Minutes, totalMainScreenSectionTime.Seconds,
            totalMainScreenSectionTime.Milliseconds / 10);

        DataLogControler.Instance.WriteToFile("Total time viewing main screen for this section: " + elapsedTime);

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            totalBackgroundSectionTime.Hours, totalBackgroundSectionTime.Minutes, totalBackgroundSectionTime.Seconds,
            totalBackgroundSectionTime.Milliseconds / 10);

        DataLogControler.Instance.WriteToFile("Total time viewing background for this section: " + elapsedTime);

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            totalSecondScreenSectionTime.Hours, totalSecondScreenSectionTime.Minutes, totalSecondScreenSectionTime.Seconds,
            totalSecondScreenSectionTime.Milliseconds / 10);

        DataLogControler.Instance.WriteToFile("Total time viewing second screen for this section: " + elapsedTime);
        resetSectionTotals();
    }


    public void resetSectionTotals()
    {
        totalMainScreenSectionTime = System.TimeSpan.Zero;
        totalSecondScreenSectionTime = System.TimeSpan.Zero;
        totalBackgroundSectionTime = System.TimeSpan.Zero;
        
    }

    // Starts recording time for when user enters main screen bounding box

    public void enterMainScreen()
    {
        mainScreenStopwatch.Start();

        DataLogControler.Instance.WriteToFile("User has entered main screen bounding box" + " - " + DateTime.Now);
    }

    /*
     * 
     * Stops recording time for when user exits main screen bounding box, adds the time to the total viewing time.
     * Writes the time that the user was looking at the bounding box to the data log.
     */

    public void exitMainScreen()
    {
        mainScreenStopwatch.Stop();
        TimeSpan ts = mainScreenStopwatch.Elapsed;
        mainScreenStopwatch.Reset();

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        totalMainScreenTime = totalMainScreenTime + ts;
        totalMainScreenSectionTime = totalMainScreenSectionTime + ts;

        DataLogControler.Instance.WriteToFile("User has exited the main screen bounding box, they were in there for: " + elapsedTime + " - " + DateTime.Now);
        DataLogControler.Instance.WriteToFile(System.Environment.NewLine);
    }

    public void enterBackground()
    {
        backgroundStopwatch.Start();

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

        totalBackgroundTime = totalBackgroundTime + ts;
        totalBackgroundSectionTime = totalBackgroundSectionTime + ts;

        DataLogControler.Instance.WriteToFile("User has exited the background bounding box, they were in there for: " + elapsedTime + " - " + DateTime.Now);
        DataLogControler.Instance.WriteToFile(System.Environment.NewLine);
    }


    public void enterSecondScreen()
    {
        secondScreenStopwatch.Start();

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

        totalSecondScreenTime = totalSecondScreenTime + ts;
        totalSecondScreenSectionTime = totalSecondScreenSectionTime + ts;

        DataLogControler.Instance.WriteToFile("User has exited one of the secondary screen bounding boxes, they were in there for: " + elapsedTime + " - " + DateTime.Now);
        DataLogControler.Instance.WriteToFile(System.Environment.NewLine);
    }

} 
