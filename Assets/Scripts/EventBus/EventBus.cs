using System.Reflection;
using System;
using System.Collections.Generic;

using SubscriberList = System.Collections.Generic.List<object>;

public class EventBus {

    // Singleton

    private static EventBus instance;

    public static EventBus Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventBus();
            }
            return instance;
        }
    }

    private const string MethodName = "OnEvent";
    private Dictionary<Type, SubscriberList> eventMap;

    private EventBus()
    {
        eventMap = new Dictionary<Type, SubscriberList>();
    }

    public void Register (object eventSubscriber)
    {
        List<SubscriberList> subscriberLists = FindSubscriberLists(eventSubscriber);

        foreach (SubscriberList subscriberList in subscriberLists)
        {
            if (subscriberList.Exists(element => element.Equals(eventSubscriber)) == false)
            {
                subscriberList.Add(eventSubscriber);
            }
        }
    }

    public void Unregister (object eventSubscriber)
    {
        List<SubscriberList> subscriberLists = FindSubscriberLists(eventSubscriber);

        foreach (SubscriberList subscriberList in subscriberLists)
        {
            subscriberList.Remove(eventSubscriber);
        }
    }

    /// <summary>
    /// Finds subscriber list with a parameter type of eventSubscriber's OnEvent.
    /// </summary>
    /// <param name="eventSubscriber"></param>
    /// <returns></returns>
    private List<SubscriberList> FindSubscriberLists (object eventSubscriber)
    {
        List<SubscriberList> lists = new List<SubscriberList>();
        MethodInfo[] methodInfoes = eventSubscriber.GetType().GetMethods();

        foreach (MethodInfo methodInfo in methodInfoes)
        {
            if (methodInfo.Name.CompareTo(MethodName) == 0 && methodInfo.IsPublic) 
            {
                ParameterInfo[] paramInfo = methodInfo.GetParameters();

                if (paramInfo.Length == 1)
                {
                    Type paramType = paramInfo[0].ParameterType;

                    if (eventMap.ContainsKey(paramType) == false)
                    {
                        eventMap.Add(paramType, new SubscriberList());
                    }

                    lists.Add(eventMap[paramType]);
                }
            }
        }

        return lists;
    }

    public void Post (object eventObject)
    {
        Type eventType = eventObject.GetType();
        object[] param = { eventObject };

        if (eventMap.ContainsKey(eventType))
        {
            SubscriberList subscriberList = eventMap[eventType];

            foreach (object eventSubscriber in subscriberList)
            {
                MethodInfo[] methodInfoes = eventSubscriber.GetType().GetMethods();

                // Find proper method, Invoke.
                foreach (MethodInfo info in methodInfoes)
                {
                    ParameterInfo[] paramInfo = info.GetParameters();

                    if (info.IsPublic
                        && paramInfo.Length == 1 
                        && paramInfo[0].ParameterType == eventType)
                    {
                        info.Invoke(eventSubscriber, param);
                        break;
                    }
                }
            }
        }
    }

    public void Clear ()
    {
        this.eventMap.Clear();
    }

}
