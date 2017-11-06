using UnityEngine;
using Photon;
using System.IO;

public class Matchmaker : Photon.PunBehaviour {

    public Role role;
    public Player player;
    public Camera spectator;

    public enum Role {
        PLAYER,
        SPECTATOR
    }

    // Use this for initialization
    void Start()
    {
        StreamReader f = System.IO.File.OpenText("Settings.ini");

        string address = f.ReadLine();
        string role = f.ReadLine();

        if (role.ToLower() == "player" || role.ToLower() == "p")
        {
            this.role = Role.PLAYER;
        }
        else
        {
            this.role = Role.SPECTATOR;
        }

        switch (this.role)
        {
            case Role.PLAYER:
                player.gameObject.SetActive(true);
                spectator.gameObject.SetActive(false);
                break;
            case Role.SPECTATOR:
                player.gameObject.SetActive(false);
                spectator.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        PhotonNetwork.ConnectToMaster(address.Split(new[] { ':' })[0], int.Parse(address.Split(new[] { ':' })[1]), "fb840cca-e410-488e-b0fe-30361e6f7d17", "");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() + "\nPing: " + PhotonNetwork.GetPing() + "ms");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }


    [PunRPC]
    void SendPlayerAmount(int amt)
    {
        if (role == Role.PLAYER)
        {
            player.SendPlayerAmount(amt);
        }
    }

    public override void OnJoinedRoom()
    {
        OnPhotonPlayerConnected(PhotonNetwork.player);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("SendPlayerAmount", other, GameObject.FindGameObjectsWithTag("LocalPlayer").Length);
        }
    }
}
