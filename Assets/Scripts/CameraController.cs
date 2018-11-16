using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject playerRef;
    public float minAltitude = 0;
    public Vector3 InitPos;
    bool attached = false;
	// Use this for initialization
	void Start () {
        ConnectCamera();
        minAltitude = playerRef.GetComponent<Player>().initPos.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (attached)
        {
            if ((playerRef.transform.position.y - 2) >= InitPos.y &&
                playerRef.transform.position.y < playerRef.GetComponent<Player>().maxHeight)
            {
                Vector3 newPos = playerRef.transform.position;
                //Vector3 newPos = Vector3.zero;
                newPos.y -= 2f;
                newPos.z = InitPos.z;
                this.transform.position = newPos;
            }
        }
	}

    public void ConnectCamera()
    {
        attached = true;
    }

    public void DisconectCamera()
    {
        attached = false;
        this.transform.position = InitPos;
    }
}
