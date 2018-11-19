using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InterfaceController : MonoBehaviour {

    public enum MENUTYPE
    {
        MAINMENU,
        TEAMMENU,
        SETTINGSMENU,
        GAMEMENU,
        RESTARTMENU,
        PAUSEMENU
    }

    public GameObject addScorePrefab;

    public List<GameObject> menus;
    bool openingGame = true;
    float dt = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (dt < 4) dt += Time.deltaTime;
        if(SplashScreen.isFinished && openingGame && dt<4)
        {
            openingGame = false;
            SetMainMenu();
        }
    }

    public void SetMainMenu()
    {
        SetMenu(MENUTYPE.MAINMENU);
        GetComponent<GamePlayManager>().RemoveEnemiesInGame();
        GetComponent<GamePlayManager>().gameState = GamePlayManager.GameState.WAITING;
        GetComponent<GamePlayManager>().playerRef.SetState(Player.PlayerState.WAITING);
        GetComponent<ScoreManager>().parseScore = false;
        GetComponent<InicializerScript>().ShowBanner();
        //GetComponent<GameController>().ResetGame();
        //GetComponent<ScoreManager>().ResetCurrentScore();
        //GetComponent<InputController>().ResetInputController();
        //Debug.Log("current menu type is " + MENUTYPE.MAINMENU);
        //this.GetComponent<GaleryController>().ActivateCardsInGalery(false);
        //if (openingGame) menus[(int)MENUTYPE.MAINMENU].GetComponent<MainMenuController>().SetMaxScoreTexts();
        //else openingGame = true;
    }

    public void SetTeamMenu()
    {
        SetMenu(MENUTYPE.TEAMMENU);
    }

    public void SetSettingsMenu()
    {
        SetMenu(MENUTYPE.SETTINGSMENU);
    }

    public void SetGameMenu()
    {
        SetMenu(MENUTYPE.GAMEMENU);
        GetComponent<InicializerScript>().ShowBanner();
        GetComponent<GamePlayManager>().gameState = GamePlayManager.GameState.START;

        ////Debug.Log("current dificult is" + GetComponent<ScoreManager>().GetCurrentDifficult());
    }

    public void SetRestartMenu()
    {
        //Debug.Log("setting restart menu");
        SetMenu(MENUTYPE.RESTARTMENU);
        GetComponent<InicializerScript>().ShowBanner();
        //menus[(int)MENUTYPE.RESTARTMENU].GetComponent<MenuController>().SetCurrentScoreTexts(
        //                                    GetComponent<ScoreManager>().GetCurrentDifficult());
        //GetComponent<GameController>().ResetGame();
    }

    void SetMenu(MENUTYPE type)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (i == (int)type) menus[i].SetActive(true);
            else menus[i].SetActive(false);
        }
    }

    public void InstantiatePlusScore(Vector3 pos)
    {
        GameObject plusScore;
        plusScore = (GameObject)Instantiate(addScorePrefab, Vector3.zero, transform.rotation);
        //plusScore.transform.SetParent(livePanelRoot.transform, false);
        plusScore.transform.position = pos;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
