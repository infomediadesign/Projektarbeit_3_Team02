using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private Dictionary<string, Delegate> eventDictionary = new Dictionary<string, Delegate>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartListening<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            eventDictionary[eventName] = existingEvent as Action<T> + listener;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    public void StopListening<T>(string eventName, Action<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var existingEvent))
        {
            var newEvent = existingEvent as Action<T> - listener;
            if (newEvent == null)
            {
                eventDictionary.Remove(eventName);
            }
            else
            {
                eventDictionary[eventName] = newEvent;
            }
        }
    }

    public void TriggerEvent<T>(string eventName, T param)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            if (thisEvent is Action<T> action)
            {
                action.Invoke(param);
            }
            else
            {
                Debug.LogError($"Event {eventName} does not match the expected parameter type.");
            }
        }
        else
        {
            Debug.LogError($"No event found for {eventName}.");
        }
    }
}

