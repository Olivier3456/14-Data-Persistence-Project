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
    private int[] _bestScores = new int[] { 0, 0, 0 };

    private string _bestPlayer;
    private string[] _bestPlayers = new string[] { "", "", "" };


    void Start()
    {
        if (SaveNameOfPlayer.BestScores != null) _bestScores = SaveNameOfPlayer.BestScores;
        if (SaveNameOfPlayer.BestPlayers != null) _bestPlayers = SaveNameOfPlayer.BestPlayers;

        _bestScore = _bestScores[0];
        _bestPlayer = _bestPlayers[0];


        ScoreText1.text = "Best Score : " + _bestPlayer + " : " + _bestScore;

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
            if (m_Points >= _bestScore) ScoreText1.text = "Best Score : " + _bestPlayer + " : " + _bestScore;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveBestScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void SaveBestScore()
    {
        if (m_Points >= _bestScores[_bestScores.Length - 1])     // Si le score est plus élevé que le dernier du tableau des meilleurs scores...
        {
            for (int i = 0; i < _bestScores.Length; i++)         // On part du début du tableau des meilleurs scores et on le descend...
            {
                if (m_Points > _bestScores[i])                             // Si le score de la partie est plus grand que le meilleur score du tableau (puis le 2e meilleur, etc.)...
                {
                    for (int j = _bestScores.Length - 1; j > i; j--)       // On décale tous les scores et noms vers la fin du tableau, en partant de la fin pour ne pas écraser les valeurs.
                    {                                                      // Seule la dernière valeur est écrasée : elle sort du tableau des scores. On s'arrête à l'index juste avant i.
                        _bestScores[j] = _bestScores[j - 1];
                        _bestPlayers[j] = _bestPlayers[j - 1];
                    }
                    _bestScores[i] = m_Points;                             // Enfin, on remplace les valeurs au rang i par le score actuel et le nom du joueur.
                    _bestPlayers[i] = _bestPlayer;

                    break;                                                 // Et on sort de la boucle.
                }
            }

            SaveData dataToSave = new SaveData();
            dataToSave.BestPlayers = _bestPlayers;
            dataToSave.BestScores = _bestScores;
            string json = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
        }
    }


    void AddPoint(int point)
    {
        m_Points += point;
        if (m_Points > _bestScore)
        {
            _bestScore = m_Points;                           // Si le score actuel du joueur est supérieur à _bestcore, il devient le nouveau _bestcore,
            _bestPlayer = SaveNameOfPlayer.PlayerName;       // et le nom du meilleur joueur devient celui du joueur actuel.
        }

        ScoreText.text = "Score : " + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
