using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameEventListener
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;

    public void Register()
    {
        gameEvent?.RegisterListener(this);
    }

    public void Unregister()
    {
        gameEvent?.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}

[Serializable]
public class GameEventListener<T>
{
    [SerializeField] private GameEvent<T> gameEvent;
    [SerializeField] private UnityEvent<T> response;

    public void Register()
    {
        gameEvent?.RegisterListener(this);
    }

    public void Unregister()
    {
        gameEvent?.UnregisterListener(this);
    }

    public void OnEventRaised(T t)
    {
        response?.Invoke(t);
    }
}
