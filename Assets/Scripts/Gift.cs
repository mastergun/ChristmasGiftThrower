﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour {

    public ScoreManager smRef;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if(col.gameObject.tag == "Chimenea")
        {
            smRef.AddScore(true);
            GetComponent<AutoDestroy>().SelfDestruction();
            smRef.GetComponent<AudioManager>().PlayGameEffect(2);
        }
    }
}
