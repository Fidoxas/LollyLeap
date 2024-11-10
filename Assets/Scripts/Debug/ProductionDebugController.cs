using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class ProductionDebugController : MonoBehaviour
    {
        private int clicks = 0;

        // Use this for initialization
        void Start()
        {
            ProductionDebug.Init();
        }

        public void OnEnablerClick()
        {
            clicks++;

            if (clicks > 5)
            {
                ProductionDebug.Enabled = true;
            }
        }
    }
}