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


    class SaveData          // La classe qui va contenir nos variables � charger depuis la sauvegarde json.
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


    public void LimitLengthOfName()     // (Cette m�thode est appel�e par l'�v�nement On Value Changed de notre Input Field.)
    {
        if (_inputField.text.Length > 14) _inputField.text = _nameMoinsUnCaractere;          // Si le nom fait d�j� plus de 14 caract�res, stoppe la possibilit� d'ajouter des caract�res.
        else _nameMoinsUnCaractere = _inputField.text;
    }

    public void StartGame()     // (M�thode appel�e par le bouton Start.)
    {
        if (_inputField.text == "Type your name" || _inputField.text.Length == 0)     // Si le joueur n'a pas entr� son nom ou a laiss� le champ vide,
        {                                                                             // le jeu ne se charge pas et un message appara�t � la place.
            _errorText.text = "Please type your name.";
            _errorText.gameObject.SetActive(true);
        }
        else
        {
            SaveNameOfPlayer.PlayerName = _inputField.text;         // Sinon, PlayerName devient �gal au texte entr� par le joueur, le jeu se charge.
            SceneManager.LoadScene(1);           
        }
    }
}
