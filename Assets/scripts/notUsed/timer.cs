using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text TimerText;
    private float startTime;

    private bool finnished = false;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (finnished)
        {
            return;
        }

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        TimerText.text = minutes + ":" + seconds;
    }

    public void Finnish()
    {
        finnished = true;
    }
}