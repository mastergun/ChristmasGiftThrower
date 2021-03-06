﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum STATE
    {
        PREPARING,
        WARNING,
        ACTIVATING,
        WAITING
    }

    public GameObject missilePrefab;
    public bool oscilate = false;
    public bool shoter = false;
    public STATE enemyState = 0;
    public float mf = 1000;

    bool freezed = false;
    private float oscilaterCoutner = 0;
    private float dt = 0;

	// Use this for initialization
	void Start () {
        enemyState = STATE.PREPARING;
    }
	
	// Update is called once per frame
	void Update () {
        switch (enemyState) { 
            case STATE.PREPARING:
                //aqui va tot el que s'hagi d'inicialitzar de l'enemic
                enemyState = STATE.WAITING;
                break;
            case STATE.WARNING:

                if (freezed)
                {
                    GetComponent<AutoMovement>().UnFreeze();
                    freezed = false;
                }
                GetComponent<AudioSource>().Play();
                enemyState = STATE.ACTIVATING;
                break;
            case STATE.ACTIVATING:

                dt += Time.deltaTime;
                if (oscilate && dt > 0.01)
                {
                    oscilaterCoutner += 0.1f;
                    GetComponent<AutoMovement>().dir.y = Mathf.Sin(oscilaterCoutner) / 10;
                    dt = 0;
                }

                if (shoter && dt > 0.7f)
                {
                    GameObject missile = ShotMisile();
                    missile.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.7f, 1f) * (mf + Random.Range(500, 1500)));
                    shoter = false;
                }
                break;
            case STATE.WAITING:
                if (!freezed)
                {
                    GetComponent<AutoMovement>().Freeze();
                    freezed = true;
                }

                break;
        }
	}

    GameObject ShotMisile()
    {
        GameObject e;
        e = (GameObject)Instantiate(missilePrefab, this.transform.position, transform.rotation);
        return e;
    }
}
