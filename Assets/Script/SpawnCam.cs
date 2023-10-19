using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnCam : MonoBehaviour
{
    public GameObject camSeeBoard;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(camSeeBoard.name,transform.position,transform.rotation);
    }

}
