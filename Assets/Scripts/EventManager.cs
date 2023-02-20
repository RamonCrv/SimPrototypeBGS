using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{  
    public static EventManager Instance { get; private set; }
    [SerializeField] private bool callOnStart = false;
    [SerializeField] private List<GameEvent> EventSequence;
    private int currentEventIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (callOnStart == true)
        {
            CallNextEvent();
        }
        
    }

    public void CallNextEvent()
    {
        if (currentEventIndex < EventSequence.Count)
        {      
            StartCoroutine(CounterToCallNextEvent());
            
        }
    }

    private IEnumerator CounterToCallNextEvent()
    {
        GameEvent currentEvent = EventSequence[currentEventIndex];
        yield return new WaitForSeconds(currentEvent.timeToRunEvent);
        currentEventIndex++;
        currentEvent.eventFunction.Invoke();
        if (currentEvent.callNextEventAuto == true)
        {
            CallNextEvent();
        }

    }
}

[System.Serializable]
public class GameEvent
{
    [SerializeField] public string eventName;
    [SerializeField] public UnityEvent eventFunction;
    [SerializeField] public float timeToRunEvent;
    [SerializeField] public bool callNextEventAuto = false;

}


