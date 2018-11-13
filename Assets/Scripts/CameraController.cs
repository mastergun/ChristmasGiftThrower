using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject playerRef;
    
    Vector3 InitPos;
    bool attached = false;
	// Use this for initialization
	void Start () {
        InitPos = this.transform.position;
        ConnectCamera();
       
    }
	
	// Update is called once per frame
	void Update () {
        if (attached)
        {
            if ((playerRef.transform.position.y) > InitPos.y && 
                playerRef.transform.position.y < playerRef.GetComponent<Player>().maxHeight)
            {
                Vector3 newPos = playerRef.transform.position;
                newPos.y -= 1f;
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
