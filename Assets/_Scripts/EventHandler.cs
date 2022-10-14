using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
	public PlayerClass pcBuffer;
	public uint pcId;
	//public List<PlayerClass> ListPC;
	public SessionClass sBuffer;
	public ItemClass iBuffer;
	public uint sId;
	public uint eId;

	public string url = "https://citmalumnes.upc.es/~sergicf4/";
	public string pcUrl = "AddPlayer.php";
	public string sUrl = "AddSession.php";
	public string bUrl = "AddBuy.php";
	public string eUrl = "EndSession.php";
	public string dataUrl = "";

	private void OnEnable()
	{
		Simulator.OnNewPlayer += NewPlayer;
		Simulator.OnNewSession += NewSession;
		Simulator.OnBuyItem += NewPurchase;
		Simulator.OnEndSession += EndSession;
	}

    private void NewPlayer(string name, string country, DateTime dateTime)
	{
		pcBuffer = new PlayerClass(name, country, dateTime);
		//ListPC.Add(pcBuffer);
		StartCoroutine(Player2PHP(pcBuffer));
	}
	IEnumerator Player2PHP(PlayerClass pc)
	{
		dataUrl = url + pcUrl + "?" + pc.GetData();
		Debug.Log(dataUrl);
		WWW www = new WWW(dataUrl);
		
		yield return www;

		if(www.error == null)
		{
			Debug.Log(www.text);
			pcId = uint.Parse(www.text);
			CallbackEvents.OnAddPlayerCallback?.Invoke(pcId);
		}
		else
		{
			Debug.Log(www.error);
		}
		//TODO: Save UID in Unity
		//Simulator.OnAddPlayerCallback
		//www.text
	}

	private void NewSession(DateTime dateTime)
	{
		sBuffer = new SessionClass(dateTime, pcId);
		StartCoroutine(SessionStart2PHP(sBuffer));
	}

	IEnumerator SessionStart2PHP(SessionClass s)
	{
		dataUrl = url + sUrl + "?" + s.GetData();
		Debug.Log(dataUrl);
		WWW www = new WWW(dataUrl);

		yield return www;

		if (www.error == null)
		{
			Debug.Log(www.text);
            sId = uint.Parse(www.text);
            CallbackEvents.OnNewSessionCallback?.Invoke(sId);
        }
		else
		{
			Debug.Log(www.error);
		}
		//TODO: Save UID in Unity
		//Simulator.OnAddPlayerCallback
		//www.text
	}

	private void NewPurchase(int itemId, DateTime buyTime)
	{
		iBuffer = new ItemClass(itemId, buyTime);
		StartCoroutine(Item2PHP(iBuffer));
	}

	IEnumerator Item2PHP(ItemClass it)
	{
		bUrl = "AddBuy.php";
		dataUrl = url + bUrl + "?playerId=" + pcId + "&" + it.GetData();
		Debug.Log(dataUrl);
		WWW www = new WWW(dataUrl);

		yield return www;

		if (www.error == null)
		{
			Debug.Log(www.text);
			CallbackEvents.OnItemBuyCallback?.Invoke();
		}
		else
		{
			Debug.Log(www.error);
		}
	}

	private void EndSession(DateTime dateTime)
	{
		sBuffer = new SessionClass(dateTime, sId);
		StartCoroutine(SessionEnd2PHP(sBuffer));
	}

	IEnumerator SessionEnd2PHP(SessionClass s)
	{
		dataUrl = url + eUrl + "?" + s.GetData();
		Debug.Log(dataUrl);
		WWW www = new WWW(dataUrl);

		yield return www;

		if (www.error == null)
		{
			Debug.Log(www.text);
            eId = uint.Parse(www.text);
            CallbackEvents.OnEndSessionCallback?.Invoke(eId);
        }
		else
		{
			Debug.Log(www.error);
		}

		//TODO: Save UID in Unity
		//Simulator.OnAddPlayerCallback
		//www.text
	}
}
