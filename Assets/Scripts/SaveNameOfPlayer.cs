using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// J'ai choisi d'attacher ce script au canvas de l'UI, comme le script StartMenu.

public class SaveNameOfPlayer : MonoBehaviour
{
    public static string PlayerName = "Type your name";

    public static int[] BestScores = new int[] {0, 0, 0};
    public static string[] BestPlayers = new string[] {"", "", ""};
}
