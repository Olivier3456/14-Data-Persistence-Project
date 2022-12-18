using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallOfFameMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _firstScoreText;
    [SerializeField] TextMeshProUGUI _secondScoreText;
    [SerializeField] TextMeshProUGUI _thirdScoreText;



    class SaveData          // La classe qui va contenir nos variables à charger depuis la sauvegarde json.
    {
        public string[] BestPlayers = new string[2];
        public int[] BestScores = new int[2];
    }



    public void Start()     // C'est en partie une copie de la fonction Start() de la classe StartMenu. Je pourrais l'ajouter au MainManager et en faire un objet qui survit entre les scènes.
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData dataLoaded = JsonUtility.FromJson<SaveData>(json);

            int[] bestScores = dataLoaded.BestScores;
            string[] bestPlayers = dataLoaded.BestPlayers;

            if (bestScores[0] != 0) _firstScoreText.text = "1st - " + bestPlayers[0] + " - " + bestScores[0];
            if (bestScores[1] != 0) _secondScoreText.text = "2nd - " + bestPlayers[1] + " - " + bestScores[1];
            if (bestScores[2] != 0) _thirdScoreText.text = "3rd - " + bestPlayers[2] + " - " + bestScores[2];
        }
    }





    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
