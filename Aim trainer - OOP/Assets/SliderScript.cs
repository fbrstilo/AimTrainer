using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;   

    int CreateConfig(string path)
    {
        if(!File.Exists(path))
        {
            File.WriteAllText(path, "1.00");    //default to 1.00
            return 1;
        }
        return 0;
    }

    float ReadConfig(string path)
    {
        if(CreateConfig(path) == 1) return 1.0f;
        float returnValue = float.Parse(File.ReadAllText(path));
        if(returnValue >= 0.5f && returnValue <= 10.0f) return returnValue;
        return 1.0f;
    }

    void StoreConfig(string path, float value)
    {
        File.WriteAllText(path, "" + value);
    }

    void Start()
    {
        string path = Application.dataPath + "/config.txt";
        slider.value = ReadConfig(path);
        sliderText.text = slider.value.ToString("0.00");

        slider.onValueChanged.AddListener((v)=>
        {
            sliderText.text = v.ToString("0.00");
            PlayerController.mouseSensitivity = v;
            StoreConfig(path, v);
        });
    }
}
