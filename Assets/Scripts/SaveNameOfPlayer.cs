using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// J'ai choisi d'attacher ce script au canvas de l'UI, comme le script StartMenu.

public class SaveNameOfPlayer : MonoBehaviour
{
    public static string PlayerName = "Type your name";

    public static int[] BestScores;
    public static string[] BestPlayers;
}
