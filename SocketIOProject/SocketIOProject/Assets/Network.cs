using UnityEngine;
using SocketIO;
using System.Collections.Generic;

public class Network : MonoBehaviour
{
    static SocketIOComponent socket;
    [SerializeField] private GameObject playerPrefab;

    Dictionary<string, GameObject> players;

	void Start ()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn player", OnSpawned);
        socket.On("disconnected", OnDisconnected);
        players = new Dictionary<string, GameObject>();
    }

    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("Player connected!");
        socket.Emit("playerhere");
    }

    void OnSpawned(SocketIOEvent e)
    {
        Debug.Log("Player spawned! (ID: " + e.data + ")");
        GameObject player = Instantiate(playerPrefab);
        players.Add(e.data["id"].ToString(), player);
        Debug.Log("Count: " + players.Count);
    }

    void OnDisconnected(SocketIOEvent e)
    {
        Debug.Log("Player disconnected. (ID: " + e.data + ")");

        var id = e.data["id"].ToString();

        var player = players["id"];
        Destroy(player);
        players.Remove(e.data["id"].ToString());
    }
}
