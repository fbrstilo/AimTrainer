using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;

    void Update()
    {
        int score = Target.hits;
        ScoreText.text = "Your score is\n" + score.ToString();
    }
}
