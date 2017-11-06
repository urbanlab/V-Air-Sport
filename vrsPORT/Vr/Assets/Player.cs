using UnityEngine;
using System.Collections;
using Photon;

public class Player : PunBehaviour {

    public Transform firstSpawn;
    public Transform secondSpawn;

    public Transform head;
    public Transform left;
    public Transform right;

    public LocalPlayer body;

    private bool started;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnJoinedRoom()
    {
    }
    
    public void SendPlayerAmount(int amt)
    {
        print("RPC called with " + amt);

        if (started)
            return;

        started = true;

        if (amt > 0)
        {
            transform.position += secondSpawn.position;
            transform.Rotate(secondSpawn.rotation.eulerAngles);
        }
        else
        {
            transform.position += firstSpawn.position;
            transform.Rotate(firstSpawn.rotation.eulerAngles);
        }

        PhotonNetwork.Instantiate("LocalPlayer", transform.position, transform.rotation, 0);
    }
}
