using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public struct Score
    {
        public float points;
        public float time;
    }

    Score currentScore;
    Score maxScore;

    public Text inGameScoreText;
    public Text mainMenuScoreText;
    public Text RestartMenuScoreText;
    public GameObject HighScoreBG;

    bool firstTimeGame = true;
    public bool parseScore = false;
    public float pointsPerGift = 1;
    float deltaTimeScore = 0;

    // Use this for initialization

    void Start()
    {
        LoadGame();
        ResetCurrentScore();
        if (firstTimeGame)
        {
            SetMaxScore(0, 0);
        }
        //parseScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (parseScore)
        {
            currentScore.time += Time.deltaTime;
            deltaTimeScore += Time.deltaTime;
            //inGameTimeScore.text = currentScore.time.ToString("F1") + "s/ " + currentMaxTime + "s";
        }
    }

    void SetMaxScore(float score, float time)
    {
        Score s;
        s.points = score;
        s.time = time;
        firstTimeGame = false;
        //SaveGame();
    }

    public void SetInGameScoreText()
    {
        inGameScoreText.text = currentScore.points.ToString();
    }

    public void SetMainMenuScoreText()
    {
        mainMenuScoreText.text = maxScore.points.ToString();
    }

    public void SetRestartMenuScoreText()
    {
        RestartMenuScoreText.text = currentScore.points.ToString();
    }

    public bool CompareScore()
    {
        SetRestartMenuScoreText();
        if (currentScore.points > maxScore.points)
        {
            maxScore.points = currentScore.points;
            maxScore.time = currentScore.time;
            SaveGame();
            SetMainMenuScoreText();
            HighScoreBG.SetActive(true);
        }
        return false;
    }

    public void ResetGame()
    {
        HighScoreBG.SetActive(false);
        ResetCurrentScore();
        SetInGameScoreText();
        parseScore = true;
    }

    public void ResetCurrentScore()
    {
        currentScore.points = 0;
        currentScore.time = 0;
    }

    public void AddScore(bool add)
    {
        if (add)currentScore.points += pointsPerGift;
        SetInGameScoreText();
    }

    public void SetLoseScore()
    {
        parseScore = false;
        currentScore.points = 0;
    }

    Score SetInitScore()
    {
        Score score;
        score.points = 0;
        score.time = 0;
        return score;
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        ScoreData score = new ScoreData();
        score.maxScorePoints = maxScore.points;
        score.bestTime = maxScore.time;
        save.score = score;
        save.firstTimeGame = firstTimeGame;
        return save;
    }

    private void LoadScoreFromSaveObject(Save save)
    {
        Score s;
        s.points = save.score.maxScorePoints;

        s.time = save.score.bestTime;
        maxScore = s;

        firstTimeGame = save.firstTimeGame;
    }

    public void SaveGame()
    {
        // create a save data
        Save save = CreateSaveGameObject();

        // create save file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        //reset variable if you need

    }

    public void LoadGame()
    {
        // search if exist a save file
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // load file data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // set variables
            LoadScoreFromSaveObject(save);
            SetMainMenuScoreText();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public float maxScorePoints;
    public float bestTime;
}
