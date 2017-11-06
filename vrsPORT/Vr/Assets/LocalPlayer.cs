using UnityEngine;
using System.Collections;
using Photon;

public class LocalPlayer : PunBehaviour {

    public Transform head;
    public Transform left;
    public Transform right;


    public Vector3 headPosition;
    public Quaternion headRotation;

    public Vector3 leftHandPosition;
    public Quaternion leftHandRotation;

    public Vector3 rightHandPosition;
    public Quaternion rightHandRotation;

    public float height;

    public Player input;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<PhotonView>().isMine || !PhotonNetwork.connected)
        {
            input = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            input.body = this;

            //this.head.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<PhotonView>().isMine || !PhotonNetwork.inRoom)
        {
            // Prepare Data to Serialization

            head.position = input.head.position;
            head.rotation = input.head.rotation;

            left.position = input.left.position;
            left.rotation = input.left.rotation;

            right.position = input.right.position;
            right.rotation = input.right.rotation;

            headPosition = head.position;
            headRotation = head.rotation;

            leftHandPosition = left.position;
            leftHandRotation = left.rotation;

            rightHandPosition = right.position;
            rightHandRotation = right.rotation;

            height = head.position.y - input.transform.position.y;
        }
        else
        {

            head.position = transform.position;
            head.rotation = Quaternion.Lerp(head.rotation, headRotation, 0.1f);

            left.position = Vector3.Lerp(left.position, leftHandPosition, 0.1f);
            left.rotation = Quaternion.Lerp(left.rotation, leftHandRotation, 0.1f);

            right.position = Vector3.Lerp(right.position, rightHandPosition, 0.1f);
            right.rotation = Quaternion.Lerp(right.rotation, rightHandRotation, 0.1f);
        }
    }


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data

            stream.SendNext(headPosition);
            stream.SendNext(headRotation);

            stream.SendNext(leftHandPosition);
            stream.SendNext(leftHandRotation);

            stream.SendNext(rightHandPosition);
            stream.SendNext(rightHandRotation);

            stream.SendNext(height);
        }
        else
        {
            // Network player, receive data
            headPosition = (Vector3)stream.ReceiveNext();
            headRotation = (Quaternion)stream.ReceiveNext();

            leftHandPosition = (Vector3)stream.ReceiveNext();
            leftHandRotation = (Quaternion)stream.ReceiveNext();

            rightHandPosition = (Vector3)stream.ReceiveNext();
            rightHandRotation = (Quaternion)stream.ReceiveNext();

            height = (float)stream.ReceiveNext();
        }
    }
}
