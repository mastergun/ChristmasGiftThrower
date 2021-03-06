﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    public float lifeTime;
    float deltaTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(lifeTime >= 0)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > lifeTime)
            {
                SelfDestruction();
            }
        }
	}

    public void SelfDestruction()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Border") SelfDestruction();
    }

}
