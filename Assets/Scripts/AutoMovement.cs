using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour {

    public Vector3 speed = Vector3.zero;
    Vector3 savedSpeed = Vector3.zero;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += speed;
	}

    public void Freeze()
    {
        savedSpeed = speed;
        speed = Vector3.zero;
    }

    public void UnFreeze()
    {
        speed = savedSpeed;
        //speed = Vector3.zero;
    }
}
