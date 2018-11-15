using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour {

    public float speed = 0.1f;
    public Vector3 dir = Vector3.zero;
    float savedSpeed = 0;
	// Use this for initialization
	void Start () {
        dir = dir.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += (dir * speed);
	}

    public void Freeze()
    {
        savedSpeed = speed;

        speed = 0;
    }

    public void UnFreeze()
    {
        speed = savedSpeed;

        //speed = Vector3.zero;
    }

    public void SetToPlayerDir(Vector3 playerPos)
    {
        dir = (playerPos - this.transform.position).normalized;
    }
}
