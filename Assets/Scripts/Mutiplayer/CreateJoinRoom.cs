using UnityEngine;
using Photon.Pun;
using TMPro;

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField usernameIp, createRoomIp, joinRoomIp;

    public void CreateRoom()
    {
        if(usernameIp.text != null && usernameIp.text != "")
        {
            if (createRoomIp.text != null && createRoomIp.text != "")
                PhotonNetwork.CreateRoom(createRoomIp.text);
            else
                createRoomIp.Select();
        }
        else
        {
            createRoomIp.text = null;
            usernameIp.Select();
        }
    }

    public void JoinRoom()
    {
        if (usernameIp.text != null && usernameIp.text != "")
        {
            if (joinRoomIp.text != null && joinRoomIp.text != "")
                PhotonNetwork.JoinRoom(joinRoomIp.text);
            else
                joinRoomIp.Select();
        }
        else
        {
            usernameIp.Select();
            joinRoomIp.text = null;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Playground");
    }
}