using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SendToServer : MonoBehaviour
{
    public const string serverUrl = "https://citmalumnes.upc.es/~paums3/connectToDatabase.php";

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

    private void OnNewPlayerSend(string arg1, string arg2, int arg3, float arg4, DateTime time)
    {
        Debug.Log("Custom new player recieved");
        var data = new
        {
            type = "new_player",
            arg1,
            arg2,
            arg3,
            arg4,
            time = time.ToString("o")
        };
        StartCoroutine(SendDataToServer(JsonUtility.ToJson(data)));
    }

    private void OnNewSessionSend(DateTime time, uint id)
    {
        Debug.Log("Custom new session recieved");
        var data = new
        {
            type = "new_session",
            time = time.ToString("o"),
            id
        };
        StartCoroutine(SendDataToServer(JsonUtility.ToJson(data)));
    }

    private void OnEndSessionSend(DateTime time, uint id)
    {
        Debug.Log("Custom end session recieved");
        var data = new
        {
            type = "end_session",
            time = time.ToString("o"),
            id
        };
        StartCoroutine(SendDataToServer(JsonUtility.ToJson(data)));
    }

    private void OnItemBoughtSend(int itemId, DateTime time, uint id)
    {
        Debug.Log("Custom item bought recieved");
        var data = new
        {
            type = "item_bought",
            itemId,
            time = time.ToString("o"),
            id
        };
        StartCoroutine(SendDataToServer(JsonUtility.ToJson(data)));
    }

    private IEnumerator SendDataToServer(string jsonData)
    {
        using (UnityWebRequest www = new UnityWebRequest(serverUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Datos enviados correctamente al servidor: { www.downloadHandler.text}");
                
            }
            else
            {
                Debug.LogError($"Error al enviar datos: {www.error}");
            }
        }
    }
}
