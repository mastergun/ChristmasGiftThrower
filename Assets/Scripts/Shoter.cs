using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoter : MonoBehaviour {

    public GameObject giftPrefab;
    public GameObject playerRef;
    public float shoterForce;
    public Vector2 shoterDir;

    public float couldown = 2.0f;
    float dt = 0;
    bool giftThrowed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (giftThrowed)
        {
            dt += Time.deltaTime;
            if (dt > couldown)
            {
                giftThrowed = false;
                dt = 0;
            }
        } 
	}

    public void SpawnGift()
    {
        if (!giftThrowed) {
            InicializeGift();
            giftThrowed = true;
        }        
    }

    GameObject InicializeGift()
    {
        GameObject g;
        Vector3 position = playerRef.transform.position;
        g = (GameObject)Instantiate(giftPrefab, position, transform.rotation);
        g.GetComponent<Gift>().smRef = GetComponent<ScoreManager>();
        g.GetComponent<Rigidbody2D>().AddForce(shoterDir * shoterForce);
        return g;
    }
}
