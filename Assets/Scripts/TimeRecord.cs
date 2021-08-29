using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeRecord : MonoBehaviour
{
    public GameObject EventManager;
    public Text timeText;

    private int minutesSinceLoad;
    private double timeSinceLoad;
    double seconds;
    private string scene;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {  
            timeSinceLoad = Time.timeSinceLevelLoadAsDouble;
            seconds = timeSinceLoad % 60;
            minutesSinceLoad = (int)(timeSinceLoad / 60);
            if (seconds < 10){timeText.text = string.Format("{0}:0{1:N2}", minutesSinceLoad, timeSinceLoad % 60);}
            else { timeText.text = string.Format("{0}:{1:N2}", minutesSinceLoad, timeSinceLoad % 60); }
        }
        if(SceneManager.GetActiveScene().name == "DeathScene")
        {
            if (minutesSinceLoad < 1 && minutesSinceLoad > 0) { GameObject.Find("timeSurvived").GetComponent<Text>().text = string.Format("You survived for {0} minute and {1:N2} seconds!", minutesSinceLoad, seconds); }
            else { GameObject.Find("timeSurvived").GetComponent<Text>().text = string.Format("You survived for {0} minutes and {1:N2} seconds!", minutesSinceLoad, seconds); }
        }
    }
}
