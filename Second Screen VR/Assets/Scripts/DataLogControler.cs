using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

public class DataLogControler : MonoBehaviour
{
    private static DataLogControler instance = null;

    public string filePath;

    public static DataLogControler Instance
    {
        get
        {
            return instance;
        }
    }

    private FileStream curFile;

    void Awake()
    {
        instance = this;
    }

    public bool CreateLogFolder()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "ParticipantLogs"))) {
            return true;
        }
        else
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "ParticipantLogs"));
            return false;
        }
    }

    public bool CreateNewLog(string participantNumber)
    {

        CreateLogFolder();

        filePath = Path.Combine(Application.persistentDataPath, "ParticipantLogs", participantNumber);

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            Debug.Log("File already exists at path: " + filePath);
            return false;
        } else
        {
            File.CreateText(filePath).Dispose();
        }

        return true;
    }

    public bool WriteToFile(string message)
    {

        if (!message.EndsWith(System.Environment.NewLine))
        {
            message += (System.Environment.NewLine);
        }

        try
        {
            UTF8Encoding utf8 = new UTF8Encoding();

            byte[] result = utf8.GetBytes(message);

            using (FileStream fs = File.Open(filePath, FileMode.Append))
            {
                fs.Write(result, 0, result.Length);
                
            }
        }
        catch (Exception e)
        {
            Debug.Log("DataLogger error: " + e.Message);
            return false;
        }

        return true;
    }

    public bool WriteBreakInFile()
    {
        WriteToFile(System.Environment.NewLine);
        WriteToFile(" ---------------------- ");
        return(WriteToFile(System.Environment.NewLine));
    }

}
