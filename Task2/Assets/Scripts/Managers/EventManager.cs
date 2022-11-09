using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private readonly Dictionary<int, Dictionary<ActionTypes, object>> _events = new Dictionary<int, Dictionary<ActionTypes, object>>();

    void Awake()
    {
        Instance = this;
    }

    public void Subscribe<T>(ActionTypes type, Action<T> action, GameObject context) where T : new()
    {
        var g = new GameAction<T>();
        g.Action = action;

        var guid = Math.Abs(context.GetInstanceID());

        if (_events.ContainsKey(guid))
        {
            if (_events[guid] == null)
            {
                _events[guid] = new Dictionary<ActionTypes, object>();
            }
        }
        else
        {
            _events.Add(guid, new Dictionary<ActionTypes, object>());
        }

        _events[guid].Add(type, g);
    }

    public void UnSubscribe(GameObject context)
    {
        var guid = Math.Abs(context.GetInstanceID());
        if (_events.ContainsKey(guid))
        {
            _events.Remove(guid);
        }
    }

    public void Fire<T>(ActionTypes type, object data) where T : new()
    {
        var events = (from e in _events from r in e.Value where r.Value != null && r.Key == type select r.Value).ToList();

        foreach (GameAction<T> e in events)
        {
            if (data != null)
            {
                var str = JsonConvert.SerializeObject(data);
                var o = JsonConvert.DeserializeObject<T>(str);

                e.Action?.Invoke(o);
            }
            else
            {
                e.Action?.Invoke(default);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
