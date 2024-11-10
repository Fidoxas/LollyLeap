using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public static class ProductionDebug
    {
        [SerializeField]
        public static bool Enabled = true;
        private static List<string> messages;

        public static void Init()
        {
            messages = new List<string>();
        }

        public static void Log(string message)
        {
            if (messages == null)
            {
                messages = new List<string>();
            }

            messages.Add(message);
        }

        public static List<string> GetMessages()
        {
            return messages;
        }
    }
}