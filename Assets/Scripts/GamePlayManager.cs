using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {

    public enum GameState
    {
        START,
        GAMELOOP,
        ENDING,
        RESET,
        WAITING
    }
    public GameState gameState;
    public GameObject housesRoot;
    public List<GameObject> housePrefab;
    public GameObject houseLocatorPrefab;

    public Player playerRef;
    public float timeBtwHouses = 5;
    public float minTimeBtwEnemies = 1;
    public float maxTimeBtwEnemies = 2;

    float currentTimeBtwEnemies;
    private List<GameObject> enemiesInGame;
    private List<GameObject> housesInGame;
    private float dth = 0;
    private float dte = 0;
    int publiCounter = 0;
    public bool endedGame = false;
    // Use this for initialization
    void Start () {
        enemiesInGame = new List<GameObject>();
        housesInGame = new List<GameObject>();
        currentTimeBtwEnemies = maxTimeBtwEnemies;
        gameState = GameState.WAITING;
    }
	
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case GameState.START:
                if (publiCounter % 5 == 4) GetComponent<InicializerScript>().PrepareInterstitial();
                GetComponent<ScoreManager>().ResetGame();
                playerRef.SetState(Player.PlayerState.PREPARING);
                gameState = GameState.WAITING;
                break;

            case GameState.GAMELOOP:
                dth += Time.deltaTime;
                dte += Time.deltaTime;

                if (dth > timeBtwHouses)
                {
                    InicializeHouse();
                    dth = Random.Range(0, timeBtwHouses/2);
                }
                if (dte > currentTimeBtwEnemies)
                {
                    enemiesInGame.Add(GetComponent<EnemySpawner>().SpawnEnemy(housesRoot.transform.position));
                    dte = 0;
                    if (currentTimeBtwEnemies > minTimeBtwEnemies) currentTimeBtwEnemies -= 0.1f;
                }
                break;

            case GameState.ENDING:
                //freezeEnemies();
                freeze();
                endedGame = true;
                gameState = GameState.WAITING;
                break;

            case GameState.RESET:
                ResetGame();
                GetComponent<ScoreManager>().CompareScore();
                GetComponent<ScoreManager>().parseScore = false;
                GetComponent<InterfaceController>().SetRestartMenu();
                if (publiCounter % 5 == 4) GetComponent<InicializerScript>().ShowInterstitial();
                publiCounter++;
                gameState = GameState.WAITING;
                break;

            case GameState.WAITING:
                break;
        }
        
    }

    void InicializeHouse()
    {
        GameObject g;
        Vector3 position = housesRoot.transform.position;
        g = (GameObject)Instantiate(housePrefab[Random.Range(0,housePrefab.Count)], position, transform.rotation);
        InicializeHouseAlert(g);
        housesInGame.Add(g);

    }

    public void InicializeHouseAlert(GameObject houseRef)
    {
        GameObject a;
        a = (GameObject)Instantiate(houseLocatorPrefab, Vector3.zero, transform.rotation);
        a.transform.SetParent(GetComponent<EnemySpawner>().warningRootHUD.transform, false);
        a.GetComponent<HouseSymbolScript>().houseRef = houseRef;
        a.GetComponent<HouseSymbolScript>().playerRef = playerRef.gameObject;
        a.GetComponent<HouseSymbolScript>().SetAltitude(GetComponent<EnemySpawner>().initWarningsPos[2].transform.position.y);
        
        //set enemy ref in the warning code
    }
    public void freeze()
    {
        if (housesInGame != null)
        {
            for (int i = 0; i < housesInGame.Count; i++)
            {
                if(housesInGame[i] != null) housesInGame[i].GetComponent<AutoMovement>().Freeze();
            }
        }
    }

    public void RemoveEnemiesInGame()
    {
        if(enemiesInGame != null){
            for (int i = 0; i < enemiesInGame.Count; i++)
            {
                if (enemiesInGame[i] != null) enemiesInGame[i].GetComponent<AutoDestroy>().SelfDestruction();
            }
            enemiesInGame.Clear();
        }
    }

    public void RemoveHousesInGame()
    {
        if (housesInGame != null)
        {
            for (int i = 0; i < housesInGame.Count; i++)
            {
                if (housesInGame[i] != null) housesInGame[i].GetComponent<AutoDestroy>().SelfDestruction();
            }
            housesInGame.Clear();
        }
    }

    public void ResetGame()
    {
        RemoveEnemiesInGame();
        RemoveHousesInGame();
        currentTimeBtwEnemies = maxTimeBtwEnemies;
        endedGame = false;
    }
}
