using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class LogsUpdater : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI textMeshElement;

        // Update is called once per frame
        void Update()
        {
            textMeshElement.text = string.Join("\n", ProductionDebug.GetMessages());
        }
    }
}