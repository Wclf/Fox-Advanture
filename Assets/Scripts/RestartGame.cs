using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public static void Restart()
    {
        ScoreSave.CherryCount = 0;
        ScoreSave.GemCount = 0;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }


}
