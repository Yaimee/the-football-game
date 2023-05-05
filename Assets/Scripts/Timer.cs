using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public float currentTime;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        int hours = (int)(currentTime / 3600);
        int minutes = (int)((currentTime % 3600) / 60);
        int seconds = (int)(currentTime % 60);
        int milliseconds = (int)(currentTime * 1000 % 1000) / 10;

        timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}:{milliseconds:D2}";
    }
}
