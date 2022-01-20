using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public static Timer Instance;
    void Awake()
    {
        Instance = this;
    }
    public float currentTime = 0f;
    float startingTime = 30f;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if(currentTime > 0.0f)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.00");
        }
    }
}
