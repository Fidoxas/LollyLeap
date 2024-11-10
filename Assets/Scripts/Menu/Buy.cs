using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Buy : MonoBehaviour
{
    [FormerlySerializedAs("Bubbles")] private int bubbles;
    [FormerlySerializedAs("Shields")] private int shields;
    [FormerlySerializedAs("StopWatches")] private int stopWatches;

    // Eventy do powiadamiania o zmianach w walutach
    public static event Action<int> OnBubblesChanged;
    public static event Action<int> OnShieldsChanged;
    public static event Action<int> OnStopWatchesChanged;

    private void Start()
    {
        bubbles = PlayerPrefs.GetInt("Bubbles");
        shields = PlayerPrefs.GetInt("Shields");
        stopWatches = PlayerPrefs.GetInt("StopWatches");
    }

    private void UpdateCurrency(int amount, Action<int> onChangedEvent, ref int currentValue, string currencyName)
    {
        int newValue = currentValue + amount;
        currentValue = newValue;
        onChangedEvent?.Invoke(newValue);

        Debug.Log($"Updated {currencyName}. Old value: {currentValue - amount}, Amount: {amount}, New value: {newValue}");
    }

    public void BuyShield()
    {
        if (bubbles >= 5)
        {
            UpdateCurrency(-5, OnBubblesChanged, ref bubbles, "bubbles");
            UpdateCurrency(1, OnShieldsChanged, ref shields, "shields");
        }
    }

    public void BuyStopWatch()
    {
        if (bubbles >= 5)
        {
            UpdateCurrency(-5, OnBubblesChanged, ref bubbles, "bubbles");
            UpdateCurrency(1, OnStopWatchesChanged, ref stopWatches, "StopWatches");
        }
    }
}