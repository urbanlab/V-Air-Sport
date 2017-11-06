using UnityEngine;
using System.Collections;
using Photon;

public class Ball : PunBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!PhotonNetwork.isMasterClient)
            return;
	}
}
