using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

using Common.Data;

namespace Common.Components
{
    // --------------------------------------------------
    // EventListener.cs
    // --------------------------------------------------

    public class EventListener : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------

        public List<EventAndResponse> eventsAndResponses = new List<EventAndResponse>();

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        private void OnEnable()
        {
            if (eventsAndResponses.Count > 0)
            {
                foreach (EventAndResponse EAR in eventsAndResponses)
                {
                    EAR.gameEvent.REGISTER(this);
                }
            }
        }

        private void OnDisable()
        {
            if (eventsAndResponses.Count > 0)
            {
                foreach (EventAndResponse EAR in eventsAndResponses)
                {
                    EAR.gameEvent.UNREGISTER(this);
                }
            }
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void CALL_RESPONSE(SimpleEvent gameEvent)
        {
            for (int i = eventsAndResponses.Count - 1; i >= 0; i--)
            {
                if (gameEvent == eventsAndResponses[i].gameEvent)
                {
                    eventsAndResponses[i].CALL_RESPONSE();
                }
            }
        }
    }

    // --------------------------------------------------
    // EventAndResponse.cs
    // --------------------------------------------------

    [System.Serializable]

    public class EventAndResponse
    {
        public string name;
        public SimpleEvent gameEvent;
        public UnityEvent response;

        public void CALL_RESPONSE()
        {
            if (response.GetPersistentEventCount() > 0)
            {
                response.Invoke();
            }
        }
    }
}
