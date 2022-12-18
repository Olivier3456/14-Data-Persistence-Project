using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// J'ai choisi d'attacher ce script au canvas de l'UI.

public class StartMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TextMeshProUGUI _errorText;
    [SerializeField] TextMeshProUGUI _bestScoreText;

    private string _nameMoinsUnCaractere = "";


    class SaveData          // La classe qui va contenir nos variables à charger depuis la sauvegarde json.
    {
        public string NameOfBestPlayer;
        public int BestScore;
    }


    public void Start()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {    
            string json = File.ReadAllText(path);
            SaveData dataLoaded = JsonUtility.FromJson<SaveData>(json);

            int _bestScore = dataLoaded.BestScore;
            string _nameOfPlayerBestScore = dataLoaded.NameOfBestPlayer;
            _bestScoreText.text = "Best Score : " + _nameOfPlayerBestScore + " : " + _bestScore;
            _bestScoreText.gameObject.SetActive(true);
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
            SaveNameOfPlayer.PlayerName = _inputField.text;         // Sinon, PlayerName devient égal au texte entré par le joueur, le jeu se charge.
            SceneManager.LoadScene(1);           
        }
    }
}
