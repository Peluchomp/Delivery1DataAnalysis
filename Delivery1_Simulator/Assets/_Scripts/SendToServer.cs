using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class SendToServer : MonoBehaviour
{

    public string newPlayerUrl = "https://citmalumnes.upc.es/~didacgp/NewPlayerScript.php";
    public string newSessionUrl = "https://citmalumnes.upc.es/~didacgp/NewSessionScript.php";
    public string endSessionUrl = "https://citmalumnes.upc.es/~didacgp/EndSessionScript.php";
    public string buyItemUrl = "https://citmalumnes.upc.es/~didacgp/BuyItemScript.php";

    uint playerID = 0;

    public void OnEnable()
    {
        Simulator.OnNewPlayer += AddNewPlayer;
        Simulator.OnNewSession += AddNewSession;
        Simulator.OnEndSession += EndSession;
        Simulator.OnBuyItem += BuyItem;

    }

    private void AddNewPlayer(string arg1, string arg2, int arg3, float arg4, DateTime time)
    {
        Debug.Log("Add New Player");

        string correctName = arg1.Replace("'", " ");

        var fields = new Dictionary<string, string>
        {
            { "type", "new_player" },
            { "name", correctName },
            { "country", arg2 },
            { "age", arg3.ToString() },
            { "gender", arg4.ToString(CultureInfo.InvariantCulture) },
            { "time", time.ToString("o") }
        };

        StartCoroutine(UploadNewPlayerToServer(fields, newPlayerUrl));
    }

    private void AddNewSession(DateTime time, uint arg2)
    {
        Debug.Log("Add New Session");
       

        var fields = new Dictionary<string, string>
        {
            { "type", "new_session" },
            { "player_id", arg2.ToString() },
            { "time", time.ToString("o") }
        };

        StartCoroutine(UploadNewSessionToServer(fields, newSessionUrl));
    }

    private void EndSession(DateTime time, uint arg2)
    {
        Debug.Log("End Session");

        var fields = new Dictionary<string, string>
        {
            { "type", "end_session" },
            { "session_id", arg2.ToString() },
            { "time", time.ToString("o") }
        };

        StartCoroutine(UploadEndSessionToServer(fields, endSessionUrl));
    }

    private void BuyItem(int arg1, DateTime time, uint arg3)
    {
        Debug.Log("Buy Item");

        var fields = new Dictionary<string, string>
        {
            { "type", "buy_item" },
            { "item_id", arg1.ToString() },
            { "session_id", arg3.ToString() },
            { "time", time.ToString("o") }
        };

        StartCoroutine(UploadBuyItemToServer(fields, buyItemUrl));
    }

    public IEnumerator UploadNewPlayerToServer(Dictionary<string, string> fields, string url)
    {

        WWWForm form = new WWWForm();
        foreach (var kvp in fields)
        {
            form.AddField(kvp.Key, kvp.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            playerID = uint.Parse(www.downloadHandler.text);
            CallbackEvents.OnAddPlayerCallback?.Invoke(playerID);
        }
    }

    public IEnumerator UploadNewSessionToServer(Dictionary<string, string> fields, string url)
    {

        WWWForm form = new WWWForm();
        foreach (var kvp in fields)
        {
            form.AddField(kvp.Key, kvp.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            uint sessionID = uint.Parse(www.downloadHandler.text);
            CallbackEvents.OnNewSessionCallback?.Invoke(sessionID);
        }
    }

    public IEnumerator UploadEndSessionToServer(Dictionary<string, string> fields, string url)
    {

        WWWForm form = new WWWForm();
        foreach (var kvp in fields)
        {
            form.AddField(kvp.Key, kvp.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            CallbackEvents.OnEndSessionCallback?.Invoke(playerID);
        }
    }

    public IEnumerator UploadBuyItemToServer(Dictionary<string, string> fields, string url)
    {

        WWWForm form = new WWWForm();
        foreach (var kvp in fields)
        {
            form.AddField(kvp.Key, kvp.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            uint sessionID = uint.Parse(www.downloadHandler.text);
            CallbackEvents.OnItemBuyCallback?.Invoke(sessionID);
        }
    }

}
