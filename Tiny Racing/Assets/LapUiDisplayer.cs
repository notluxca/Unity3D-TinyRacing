using System;
using TMPro;
using UnityEngine;

public class LapUiDisplayer : MonoBehaviour
{
    int currentLaps = 0;
    TextMeshProUGUI lapText;

    void OnEnable()
    {
        lapText = GetComponent<TextMeshProUGUI>();
        lapText.text = $"{currentLaps} Laps";
        LapsCounter.OnLapCompleted += HandleLapCompleted;
    }

    private void HandleLapCompleted()
    {
        currentLaps++;
        lapText.text = $"{currentLaps} Laps";
    }
}

