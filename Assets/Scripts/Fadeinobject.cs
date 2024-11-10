using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fadeinobject : MonoBehaviour
{
    [SerializeField] public float fadeInTime = 2f; // Czas trwania pojawiania si� obiektu

    [SerializeField] GameObject panel;

    private void Start()
    {
        FadeInChildren(panel.transform);
    }

    private void FadeInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                StartCoroutine(FadeInObject(image));
            }

            FadeInChildren(child);
        }
    }

    public IEnumerator FadeInObject(Image image)
    {
        // Pocz�tkowe ustawienie obiektu jako ca�kowicie przezroczysty
        Color startColor = image.color;
        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Stopniowe pojawianie si� obiektu
        float elapsedTime = 0f;
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, startColor.a, elapsedTime / fadeInTime);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // Ko�cowe ustawienie obiektu jako w pe�ni widoczny
        image.color = startColor;
    }
}