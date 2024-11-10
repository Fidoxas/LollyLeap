using System.Collections;
using TMPro;
using UnityEngine;

public class CountWriterShop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp_Bubbles;
    [SerializeField] TextMeshProUGUI tmp_Shields;
    [SerializeField] TextMeshProUGUI tmp_StopWatches;

    private void Start()
    {
        // Wczytaj wartości z PlayerPrefs w chwili uruchomienia
        UpdateTextFields();
    }

    private void OnEnable()
    {
        // Subskrybuj do eventów, gdy obiekt staje się aktywny
        Buy.OnBubblesChanged += UpdateBubblesText;
        Buy.OnShieldsChanged += UpdateShieldsText;
        Buy.OnStopWatchesChanged += UpdateStopWatchesText;
    }

    private void OnDisable()
    {
        // Odsubskrybuj od eventów, gdy obiekt staje się nieaktywny
        Buy.OnBubblesChanged -= UpdateBubblesText;
        Buy.OnShieldsChanged -= UpdateShieldsText;
        Buy.OnStopWatchesChanged -= UpdateStopWatchesText;
    }

    private void UpdateBubblesText(int newValue)
    {
        // Aktualizuj tekst dla Bubbles
        tmp_Bubbles.text = newValue.ToString();

        // Zapisz nową wartość w PlayerPrefs
        PlayerPrefs.SetInt("Bubbles", newValue);
        PlayerPrefs.Save();  // Zapisz zmiany
    }

    private void UpdateShieldsText(int newValue)
    {
        // Aktualizuj tekst dla Shields
        tmp_Shields.text = newValue.ToString();

        // Zapisz nową wartość w PlayerPrefs
        PlayerPrefs.SetInt("Shields", newValue);
        PlayerPrefs.Save();  // Zapisz zmiany
    }

    private void UpdateStopWatchesText(int newValue)
    {
        // Aktualizuj tekst dla StopWatches
        tmp_StopWatches.text = newValue.ToString();

        // Zapisz nową wartość w PlayerPrefs
        PlayerPrefs.SetInt("StopWatches", newValue);
        PlayerPrefs.Save();  // Zapisz zmiany
    }

    private void UpdateTextFields()
    {
        // Wczytaj wartości z PlayerPrefs i zaktualizuj pola tekstowe
        tmp_Bubbles.text = PlayerPrefs.GetInt("Bubbles", 0).ToString();
        tmp_Shields.text = PlayerPrefs.GetInt("Shields", 3).ToString();
        tmp_StopWatches.text = PlayerPrefs.GetInt("StopWatches", 3).ToString();
    }
}
