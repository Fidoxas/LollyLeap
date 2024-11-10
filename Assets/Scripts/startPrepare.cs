using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartPrepare : MonoBehaviour
{
    [SerializeField] private float targetAlpha = 0.45f;

    private void OnEnable()
    {
        StartCoroutine(FadeTo(targetAlpha, 5f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public IEnumerator FadeTo(float targetAlpha, float aTime)
    {
        var graphic = GetComponent<Graphic>();
        if (graphic == null)
        {
            Debug.LogError("Komponent Graphic nie został znaleziony na tym obiekcie!");
            yield break;
        }

        float startAlpha = graphic.color.a;
        for (float t = 0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b,
                Mathf.Lerp(startAlpha, targetAlpha, t));
            graphic.color = newColor;
            yield return null;
        }
    }
}