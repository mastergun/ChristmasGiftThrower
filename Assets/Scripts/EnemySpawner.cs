using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    public struct EnemyPrefs
    {
        public Vector2 velocity;
        public Vector2 MinMaxYdist;
        public bool oscilate;
        public bool shoter;
        public float time;
    }

    enum EnemyType
    {
        PLAIN,
        METEORITE,
        MILITAR,
        BIRD
    }

    public List<EnemyPrefs> enemiesData;
    public GameObject enemyPrefab;
    public GameObject warningPrefab;
    public GameObject playerRef;

    public GameObject warningRootHUD;
    public GameObject initWarningPos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject SpawnEnemy(Vector3 rootPos)
    {
        int randomNumber = Random.Range(0, 4);
        GameObject e = InicializeEnemy(rootPos, (EnemyType)randomNumber);
        InicializeWarningAlert(e.GetComponent<Enemy>());
        return e;
    }

    GameObject InicializeEnemy(Vector3 rootPos, EnemyType type)
    {
        GameObject e;
        Vector2 mmyd = enemiesData[(int)type].MinMaxYdist;
        if(mmyd.y > -4f)rootPos.y = playerRef.transform.position.y + Random.Range(mmyd.x, mmyd.y);
        e = (GameObject)Instantiate(enemyPrefab, rootPos, transform.rotation);
        e.GetComponent<AutoMovement>().speed= enemiesData[(int)type].velocity;
        e.GetComponent<Enemy>().oscilate = enemiesData[(int)type].oscilate;
        e.GetComponent<Enemy>().shoter = enemiesData[(int)type].shoter;
        e.GetComponent<AutoDestroy>().lifeTime = enemiesData[(int)type].time;
        e.GetComponent<Enemy>().enemyState = Enemy.STATE.PREPARING;
        return e;
    }

    public void InicializeWarningAlert(Enemy enemyRef)
    {
        GameObject a;
        a = (GameObject)Instantiate(warningPrefab, Vector3.zero, transform.rotation);
        a.transform.SetParent(warningRootHUD.transform, false);
        a.transform.position = new Vector3(initWarningPos.transform.position.x, initWarningPos.transform.position.y, 0);
        a.GetComponent<WarningScript>().enemyRef = enemyRef;
        //set enemy ref in the warning code
    }
}
