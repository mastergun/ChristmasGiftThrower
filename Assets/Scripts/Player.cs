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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1)* force);
            if(this.transform.rotation.z < 0.5)this.transform.Rotate(new Vector3(0,0,1),GetComponent<Rigidbody2D>().velocity.y/5);
            Debug.Log(this.transform.rotation.z);
        }
        else
        {
            if (this.transform.rotation.z > -0.3) this.transform.Rotate(new Vector3(0,0,1), GetComponent<Rigidbody2D>().velocity.y/10);
            Debug.Log(this.transform.rotation.z);
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
        //if (activatedCollisions)
        //{
        //    if (col.gameObject.tag == "Shooter" && !flying)
        //    {
        //        source.PlayOneShot(hitChihuahua, 1.0f);
        //        this.GetComponent<SpriteRenderer>().sprite = frames[1];

        //        Vector2 dir = Vector2.zero;
        //        for (int i = 0; i < col.contacts.Length; i++)
        //        {
        //            if (col.contacts[i].point != null)
        //            {
        //                dir = col.contacts[i].point;
        //                break;
        //            }
        //        }
        //        dir = new Vector2(this.transform.position.x, this.transform.position.y) - dir;
        //        this.GetComponent<Rigidbody2D>().AddForce(dir * GetShotForce());
        //        this.GetComponent<Rigidbody2D>().AddTorque(-200);
        //        ActivateVelocimeter(false);
        //        gameControlRef.GetComponent<ScoreManager>().parseScore = true;
        //        gameControlRef.AttachCamera();
        //        Velocimeter.GetComponentInParent<DeactivateButton>().activateSelf(false);
        //        gameControlRef.foot.deactivateInput = false;
        //        flying = true;
        //    }
        //    else 
        //}
        if (col.gameObject.tag == "floor")
        {
            Debug.Log("floor detected");
            this.transform.position = initPos;
            this.transform.rotation = Quaternion.identity;
            //Vector2 dir = new Vector2(-1, 1).normalized;
            //source.PlayOneShot(trampolin, 1.0f);
            ////this.GetComponent<Rigidbody2D>().AddForce(dir * enemiesForceBase);
            //this.GetComponent<Rigidbody2D>().AddForce(dir * this.GetComponent<Rigidbody2D>().velocity.magnitude * enemiesForceBase);
            //this.GetComponent<Rigidbody2D>().AddTorque(5.0f);
        }
    }
}
