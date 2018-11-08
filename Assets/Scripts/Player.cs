using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float force = 10;
    public float maxHeight = 30;

    Vector3 initPos;
    bool flying = false;
	// Use this for initialization
	void Start () {
        initPos = this.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (flying)
        {
            if(this.transform.position.y < maxHeight/3.5) GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1)* force);
            if (this.transform.rotation.z < 0.5)this.transform.Rotate(new Vector3(0,0,1),GetComponent<Rigidbody2D>().velocity.y/5);
        }
        else
        {
            if (this.transform.rotation.z > -0.3) this.transform.Rotate(new Vector3(0,0,1), GetComponent<Rigidbody2D>().velocity.y/10);
        }
    }

    public void StartFly()
    {
        flying = true;
    }

    public void StopFly()
    {
        flying = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "floor") LoseGame();
        if (col.gameObject.tag == "Enemy")
        {
            //animation of dead
            LoseGame();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy") LoseGame();
    }

    void LoseGame()
    {
        this.transform.position = initPos;
        this.transform.rotation = Quaternion.identity;
        Debug.Log("you Lose");
    }
}
