using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI checkpointsText;
    [SerializeField] public TextMeshProUGUI itemScoreText;
    [SerializeField] public TextMeshProUGUI destroyedScoreText;

    private List<string> checkedPoint = new List<string>();
    private int itemScore = 0;
    private int destroyedScore = 0;

    private int totalCheckPoints = 0;
    private int totalItems = 0;

    private void Start()
    {
        totalCheckPoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    void Update()
    {
        checkpointsText.text = $"Checkpoints: {checkedPoint.Count}/{totalCheckPoints}";
        itemScoreText.text = $"Items: {itemScore}/{totalItems}";
        destroyedScoreText.text = $"Destroyed: {destroyedScore}";
    }
    public void GetCheckPoint(string name)
    {
        Debug.Log($"GetCheckPoint");
        Debug.Log(name);
        if (!checkedPoint.Contains(name)) checkedPoint.Add(name);
    }
    public void GetItemScore(int score)
    {
        itemScore += score;
    }
    public void GetDestroyedScore(int score)
    {
        destroyedScore += score;
    }
}
