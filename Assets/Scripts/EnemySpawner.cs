using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    enum EnemyType
    {
        PLAIN,
        METEORITE,
        MILITAR,
        BIRD
    }

    public List<Vector2> MinMaxYdist;
    public List<GameObject> enemyPrefab;
    public GameObject warningPrefab;
    public GameObject playerRef;

    public GameObject warningRootHUD;
    public List<GameObject> initWarningsPos;

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
        InicializeWarningAlert(e.GetComponent<Enemy>(), randomNumber);
        return e;
    }

    GameObject InicializeEnemy(Vector3 rootPos, EnemyType type)
    {
        GameObject e;
        Vector2 mmyd = MinMaxYdist[(int)type];
        if(mmyd.y > -4f)rootPos.y = playerRef.transform.position.y + Random.Range(mmyd.x, mmyd.y);
        e = (GameObject)Instantiate(enemyPrefab[(int)type], rootPos, transform.rotation);
        if (type == EnemyType.METEORITE) e.GetComponent<AutoMovement>().SetToPlayerDir(playerRef.transform.position);
        return e;
    }

    public void InicializeWarningAlert(Enemy enemyRef, int type)
    {
        GameObject a;
        a = (GameObject)Instantiate(warningPrefab, Vector3.zero, transform.rotation);
        a.transform.SetParent(warningRootHUD.transform, false);
        a.transform.position = new Vector3(initWarningsPos[type%3].transform.position.x, initWarningsPos[type % 3].transform.position.y, 0);
        a.GetComponent<WarningScript>().enemyRef = enemyRef;
        //set enemy ref in the warning code
    }
}
