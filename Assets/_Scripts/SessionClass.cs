using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionClass : MonoBehaviour
{
    public DateTime dateTime;
    public uint id;

    public SessionClass(DateTime dateTime, uint id)
    {
        this.dateTime = dateTime;
        this.id = id;
    }

    public string GetData()
    {
        string data = "dateTime=" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "&id=" + id;
        return data;
    }
}
