using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text ScoreText1;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private int _bestScore;
    private string _nameOfPlayerBestScore;

    [Serializable]
    class SaveData          // Notre classe sérializable qui va contenir nos variables à sauvegarder en json.
    {
        public string NameOfBestPlayer;
        public int BestScore;
    }


    void Start()
    {
        LoadBestScore();

        ScoreText.text = "Score : " + m_Points;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            // Si le record a été battu, affiche le nouveau meilleur score et le nom du nouveau meilleur joueur : 
            if (m_Points >= _bestScore) ScoreText1.text = "Best Score : " + _nameOfPlayerBestScore + " : " + _bestScore;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveBestScore();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            SaveData dataLoaded = JsonUtility.FromJson<SaveData>(json);

            _bestScore = dataLoaded.BestScore;
            _nameOfPlayerBestScore = dataLoaded.NameOfBestPlayer;
            ScoreText1.text = "Best Score : " + _nameOfPlayerBestScore + " : " + _bestScore;
        }
    }

    public void SaveBestScore()
    {
        if (m_Points >= _bestScore)     // Ne sauvegarde que si le score actuel est supérieur ou égal au meilleur score.
        {
            SaveData dataToSave = new SaveData();
            dataToSave.NameOfBestPlayer = SaveNameOfPlayer.PlayerName;
            dataToSave.BestScore = m_Points;
            string json = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
        }
    }


    void AddPoint(int point)
    {
        m_Points += point;
        if (m_Points > _bestScore)
        {
            _bestScore = m_Points;                                      // Si le score actuel du joueur est supérieur à _bestcore, il devient le nouveau _bestcore,
            _nameOfPlayerBestScore = SaveNameOfPlayer.PlayerName;       // et le nom du meilleur joueur devient celui du joueur actuel.
        }

        ScoreText.text = "Score : " + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
