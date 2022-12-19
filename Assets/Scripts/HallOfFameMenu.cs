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



    public void Start()     // C'est en partie une copie de la fonction Start() de la classe StartMenu. Je pourrais l'ajouter au MainManager et en faire un objet qui survit entre les scènes.
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (SaveNameOfPlayer.BestScores[0] > 0)
        {               
            _firstScoreText.text = "1st - " + SaveNameOfPlayer.BestPlayers[0] + " - " + SaveNameOfPlayer.BestScores[0];
            _secondScoreText.text = "2nd - " + SaveNameOfPlayer.BestPlayers[1] + " - " + SaveNameOfPlayer.BestScores[1];
            _thirdScoreText.text = "3rd - " + SaveNameOfPlayer.BestPlayers[2] + " - " + SaveNameOfPlayer.BestScores[2];
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
