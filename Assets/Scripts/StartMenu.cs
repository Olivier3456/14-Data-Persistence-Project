using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


class SaveData          // La classe qui va contenir nos variables à charger depuis la sauvegarde json.
{
    public string[] BestPlayers = new string[2];
    public int[] BestScores = new int[2];
}



// J'ai choisi d'attacher ce script au canvas de l'UI.

public class StartMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TextMeshProUGUI _errorText;
    [SerializeField] TextMeshProUGUI _bestScoreText;

    private string _nameMoinsUnCaractere = "";



    public void Start()
    {
        _inputField.text = SaveNameOfPlayer.PlayerName;        

        if (SaveNameOfPlayer.BestScores != null) _bestScoreText.text = "Best Score : " + SaveNameOfPlayer.BestPlayers[0] + " : " + SaveNameOfPlayer.BestScores[0];
        _bestScoreText.gameObject.SetActive(true);

        LoadSavedData();
    }


    public void LoadSavedData()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData dataLoaded = JsonUtility.FromJson<SaveData>(json);

            SaveNameOfPlayer.BestScores = dataLoaded.BestScores;        // Nous stockons dans les tableaux static de la classe SaveNameOfPlayer ceux de la sauvegarde json.
            SaveNameOfPlayer.BestPlayers = dataLoaded.BestPlayers;
        }
        else
        {
            SaveData dataToSave = new SaveData();                       // Si pas de sauvegarde trouvée, on en crée une avec des scores à zéro et des chaînes vides pour le nom des joueurs.
            dataToSave.BestPlayers = new string[] { "", "", "" };
            dataToSave.BestScores = new int[] { 0, 0, 0 };
            string json = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);

            SaveNameOfPlayer.BestScores = new int[] { 0, 0, 0 };                    // Et on initialise les variables static de SaveNameOfPlayer avec des 0 et des champs vides.
            SaveNameOfPlayer.BestPlayers = new string[] { "", "", "" };
        }
    }


    public void LimitLengthOfName()     // (Cette méthode est appelée par l'événement On Value Changed de notre Input Field.)
    {
        if (_inputField.text.Length > 14) _inputField.text = _nameMoinsUnCaractere;          // Si le nom fait déjà plus de 14 caractères, stoppe la possibilité d'ajouter des caractères.
        else _nameMoinsUnCaractere = _inputField.text;
    }

    public void StartGame()     // (Méthode appelée par le bouton Start.)
    {
        if (_inputField.text == "Type your name" || _inputField.text.Length == 0)     // Si le joueur n'a pas entré son nom ou a laissé le champ vide,
        {                                                                             // le jeu ne se charge pas et un message apparaît à la place.
            _errorText.text = "Please type your name.";
            _errorText.gameObject.SetActive(true);
        }
        else
        {
            SaveNameOfPlayer.PlayerName = _inputField.text;         // Sinon, PlayerName devient égal au texte entré par le joueur, et le jeu se charge.
            SceneManager.LoadScene(2);
        }
    }

    public void LoadBestScoresScene()
    {
        if (_inputField.text != "Type your name" || _inputField.text.Length > 0)       // Si le joueur n'a entré son nom, le jeu le sauvegarde.
        {
            SaveNameOfPlayer.PlayerName = _inputField.text;
        }

        SceneManager.LoadScene(1);
    }
}
