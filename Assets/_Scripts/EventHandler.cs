using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
	public PlayerClass pcBuffer;
	public List<PlayerClass> ListPC;
	public string url = "https://citmalumnes.upc.es/~sergicf4/";
	public string phpUrl = "AddPlayer.php";
	public string dataUrl = "";

	private void OnEnable()
	{
		Simulator.OnNewPlayer += NewPlayer;
	}

	private void NewPlayer(string arg1, string arg2, DateTime arg3)
	{
		pcBuffer.name = arg1;
		pcBuffer.country = arg2;
		pcBuffer.dateTime = arg3;
		ListPC.Add(pcBuffer);
		Player2PHP(pcBuffer);
	}
	IEnumerator Player2PHP(PlayerClass pc)
	{
		dataUrl = url + phpUrl + pc.GetData();

		using (WWW www = new WWW(dataUrl))
		{
			yield return www;
		}
	}
}
