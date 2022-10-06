using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
	public string name, country;
	public DateTime dateTime;
	public PlayerClass(string name, string country, DateTime dateTime)
	{
		this.name = name;
		this.country = country;
		this.dateTime = dateTime;
	}
	public string GetData()
	{
		string data = "name=" + name + "&country=" + country + "&dateTime=" + dateTime.ToString("yyyy-MM-dd HH:mm:ss");
		return data;
	}

}
