using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;
    [SerializeField] private GameObject playerPrefab;

	void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn player", OnSpawn);
	}

    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("Player connected!");
        socket.Emit("playerhere");
    }

    void OnSpawn(SocketIOEvent e)
    {
        Debug.Log("Player spawned!");
        Instantiate(playerPrefab);
    }
}
