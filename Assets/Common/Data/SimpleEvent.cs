using Common.Components;

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    // --------------------------------------------------
    // GameEvent.cs
    // --------------------------------------------------

    [CreateAssetMenu(fileName = "Event", menuName = "Common/Simple Event", order = 1)]

    public class SimpleEvent : ScriptableObject
    {
        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        private List<EventListener> eventListeners = new List<EventListener>();

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void TRIGGER()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].CALL_RESPONSE(this);
            }
        }

        public void REGISTER(EventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UNREGISTER(EventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }

/*

    [CustomEditor(typeof(SimpleEvent))]
    [CanEditMultipleObjects]

    public class SimpleEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("RISE EVENT"))
            {
                SimpleEvent gameEvent = (SimpleEvent)target;

                gameEvent.TRIGGER();
            }
        }
    }*/
