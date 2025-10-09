using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SendToServer : MonoBehaviour
{
    public const string newPlayerSend = "https://citmalumnes.upc.es/~paums3/connectToDatabase.php";
    private string newSessionSend = "https://citmalumnes.upc.es/~paums3/SessionServerRequest.php";

    private void OnEnable()
    {
        Simulator.OnNewPlayer += OnNewPlayerSend;
        Simulator.OnNewSession += OnNewSessionSend;
        Simulator.OnEndSession += OnEndSessionSend;
        Simulator.OnBuyItem += OnItemBoughtSend;
    }

    private void OnDisable()
    {
        Simulator.OnNewPlayer -= OnNewPlayerSend;
        Simulator.OnNewSession -= OnNewSessionSend;
        Simulator.OnEndSession -= OnEndSessionSend;
        Simulator.OnBuyItem -= OnItemBoughtSend;
    }

    private void OnNewPlayerSend(string userId, string age, int gender, float country, DateTime time)
    {
        Debug.Log("Custom new player recieved");
        var fields = new Dictionary<string, string>
    {
        { "UserID", "0" },
        { "Age", age },
        { "Gender", gender.ToString() },
        { "Country", country.ToString("G", System.Globalization.CultureInfo.InvariantCulture) }
    };
        StartCoroutine(SendDataToServer(fields, newPlayerSend));
    }

    private void OnNewSessionSend(DateTime time, uint id)
    {
        Debug.Log("Custom new session recieved");
        var fields = new Dictionary<string, string>
               {
                   { "UserID", id.ToString() },
                   { "SessionID", "0" },
                   { "LengthOfSession", "23" },
                   { "Time", time.ToString("yyyy-MM-dd HH:mm:ss.fff") }
               };
        StartCoroutine(SendDataToServer(fields, newSessionSend));
    }

    private void OnEndSessionSend(DateTime time, uint id)
    {
        Debug.Log("Custom end session recieved");
        var fields = new Dictionary<string, string>
    {
        { "Type", "end_session" },
        { "Time", time.ToString("yyyy-MM-dd HH:mm:ss.fff") },
        { "UserID", id.ToString() }
    };
        StartCoroutine(SendDataToServer(fields, newPlayerSend));
    }

    private void OnItemBoughtSend(int itemId, DateTime time, uint id)
    {
        Debug.Log("Custom item bought recieved");
        var fields = new Dictionary<string, string>
    {
        { "Type", "item_bought" },
        { "ItemID", itemId.ToString() },
        { "Time", time.ToString("yyyy-MM-dd HH:mm:ss.fff") },
        { "UserID", id.ToString() }
    };
        StartCoroutine(SendDataToServer(fields, newPlayerSend));
    }

    private IEnumerator SendDataToServer(Dictionary<string,string> fields, string serverUrl)
    {
        WWWForm form = new WWWForm();
        foreach (var field in fields)
        {
            form.AddField(field.Key, field.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(serverUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending data to server: " + www.error);
        }
        else
        {
            Debug.Log("Data successfully sent to server: " + www.downloadHandler.text);
        }
    }
}
