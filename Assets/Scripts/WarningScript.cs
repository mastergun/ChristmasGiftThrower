using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour {

    public Enemy enemyRef;
    public float lifeTime = 2;
    private float dt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dt += Time.deltaTime;
        if (dt > lifeTime)
        {

            enemyRef.enemyState = Enemy.STATE.WARNING;
            GetComponent<AutoDestroy>().SelfDestruction();
        }
	}


}
