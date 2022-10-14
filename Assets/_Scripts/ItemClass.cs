using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass
{
	public int itemId;
	public DateTime buyTime;
	public ItemClass(int id, DateTime buyTime)
	{
		this.itemId = id;
		this.buyTime = buyTime;
	}
	public string GetData()
	{
		string data = "itemId=" + itemId + "&buyTime=" + buyTime.ToString("yyyy-MM-dd HH:mm:ss");
		return data;
	}

}