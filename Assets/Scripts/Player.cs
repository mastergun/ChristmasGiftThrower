using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum PlayerState
    {
        PREPARING,
        FLYING,
        FALLING,
        HIT,
        DIYING,
        WAITING
    }
    public float force = 10;
    public float maxHeight = 30;
    public float timeBrDye = 2;
    public float gravityForce = 2;
    public float upSpeedFactor = 0.1f;
    public float downSpeedFactor = 0.15f;
    public GamePlayManager gameplayRef;

    public Vector3 initPos = Vector3.zero;
    PlayerState playerState = PlayerState.WAITING;
    public bool activateInput = false;
    float dt = 0;
	// Use this for initialization
	void Start () {
        //initPos = this.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 newpos = Vector3.zero;
        Quaternion newRotation = Quaternion.identity;
        switch (playerState)
        {
            case PlayerState.PREPARING:
                ResetPlayerPos();
                activateInput = true;
                GetComponent<AudioSource>().volume = gameplayRef.GetComponent<AudioManager>().effectsVol;
                GetComponent<AudioSource>().Play();
                gameplayRef.gameState = GamePlayManager.GameState.GAMELOOP;
                playerState = PlayerState.FALLING;
                break;

            case PlayerState.FLYING:
                //if ((this.transform.position.y < maxHeight / 3.5) || this.GetComponent<Rigidbody2D>().velocity.y < 0) GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * force);
                //else this.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, 0.1f);
                if((this.transform.position.y < maxHeight))
                {
                    newpos.y = upSpeedFactor * gravityForce;
                    this.transform.position += newpos;
                    if(this.transform.rotation.z < 30f)
                    {
                        newRotation.z = this.transform.rotation.z + upSpeedFactor/10;
                        this.transform.rotation = newRotation;
                    }
                }
                break;

            case PlayerState.FALLING:
                newpos.y = downSpeedFactor * gravityForce;
                this.transform.position -= newpos;
                if (this.transform.rotation.z > -15f)
                {
                    newRotation.z = this.transform.rotation.z - downSpeedFactor/20;
                    this.transform.rotation = newRotation;
                }

                break;

            case PlayerState.HIT:
                gameplayRef.gameState = GamePlayManager.GameState.ENDING;
                activateInput = false;
                GetComponent<Rigidbody2D>().gravityScale = 5f;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 1) * 4000);
                gameplayRef.GetComponent<AudioManager>().PlayGameEffect(3);
                playerState = PlayerState.DIYING;
                break;

            case PlayerState.DIYING:
                dt += Time.deltaTime;
                if (dt > timeBrDye)
                {
                    gameplayRef.gameState = GamePlayManager.GameState.RESET;
                    ResetPlayerPos();
                    playerState = PlayerState.WAITING;
                    dt = 0;
                    GetComponent<AudioSource>().Stop();
                }
                break;

            case PlayerState.WAITING:
                //WAITING for orders
                activateInput = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                break;
        }
    }

    public void StartFly()
    {
        if (activateInput)playerState = PlayerState.FLYING;
    }

    public void StopFly()
    { 
        if (activateInput) playerState = PlayerState.FALLING;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!gameplayRef.endedGame)
        {
            if (playerState != PlayerState.HIT || playerState != PlayerState.DIYING) playerState = PlayerState.HIT;
        }
        //if (col.gameObject.tag == "floor" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Border" ) //ResetPlayerPos();
        //{
            
        //    //animation of dead
        //    //ResetPlayerPos();
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && !gameplayRef.endedGame)
        {
            if (playerState != PlayerState.HIT || playerState != PlayerState.DIYING) playerState = PlayerState.HIT;
        }
    }

    void ResetPlayerPos()
    {
        dt = 0;
        this.transform.position = initPos;
        this.transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        //Debug.Log("you Lose");
    }

    public void SetState(PlayerState state) { playerState = state; }
}
