using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {

    public GameObject housesRoot;
    public GameObject housePrefab;
    
    
    public int timeBtwHouses = 5;
    public int timeBtwEnemies = 2;

    private List<GameObject> enemiesInGame;
    private float dth = 0;
    private float dte = 0;
    // Use this for initialization
    void Start () {
        enemiesInGame = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
        dth += Time.deltaTime;
        dte += Time.deltaTime;

        if (dth > timeBtwHouses)
        {
            InicializeHouse();
            dth = 0;
        }
        if (dte > timeBtwEnemies)
        {
            enemiesInGame.Add(GetComponent<EnemySpawner>().SpawnEnemy(housesRoot.transform.position));
            dte = 0;
        }
    }

    void InicializeHouse()
    {
        GameObject g;
        Vector3 position = housesRoot.transform.position;
        g = (GameObject)Instantiate(housePrefab, position, transform.rotation);

    }

    public void RemoveEnemiesInGame()
    {
        for (int i=0;i<enemiesInGame.Count;i++)
        {
            enemiesInGame[i].GetComponent<AutoDestroy>().SelfDestruction();
        }
        enemiesInGame.Clear();
    }

}
