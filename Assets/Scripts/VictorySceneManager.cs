using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using TMPro;

public class VictorySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button nextLevelButton;
    private LevelManager levelManager;
    public TextMeshProUGUI PlayerStatText;
    public GameObject canvas;

    void Start()
    {
        PlayerStatText = FindObjectOfType<TextMeshProUGUI>();
        PlayerStatText.text += PlayerData.NumberOfSeconds;
        SendAnalytics();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

        canvas = GameObject.FindGameObjectWithTag("MovementControls");
        canvas.SetActive(false);

        if (PlayerData.CurrentLevel == levelManager.levels.Length - 1)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    private static void SendAnalytics()
    {
        AnalyticsSender.SendLevelFinishedEvent(PlayerData.CurrentLevel, PlayerData.NumberOfSeconds);
        AnalyticsSender.SendDegreesUsedInLevelEvent(PlayerData.CurrentLevel, PlayerData.DegreesCameraRotated);
        AnalyticsSender.SendMovesPerLevelEvent(PlayerData.CurrentLevel, PlayerData.NumberOfMoves, PlayerData.NumberOfRotations);
    }

    public void StartNextLevel()
    {
        PlayerData.CurrentLevel += 1;
        levelManager.LoadLevel();
    }
}
