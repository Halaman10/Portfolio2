using System;
using System.Collections.Generic;

namespace CTC.Game
{
    public class GameEvent
    {
    }

    public static class EventManager
    {
        // Dictionary used to find the action delegate containing the list of event handlers subscribed to a specific game event
        // The event handlers determine what action is taken in response to the game event
        static readonly Dictionary<Type, Action<GameEvent>> ActionsPerEvent = new Dictionary<Type, Action<GameEvent>>();

        // Dictionary used to find an event handler by delegate type
        // The type of a delegate is defined by the name of the delegate
        static readonly Dictionary<Delegate, Action<GameEvent>> HandlersByName = new Dictionary<Delegate, Action<GameEvent>>();

        // Adds a listner to watch for a specific game event to occur
        public static void AddEventListener<T>(Action<T> eventHandler) where T : GameEvent
        {
            if (!HandlersByName.ContainsKey(eventHandler))
            {
                Action<GameEvent> newHandler = (evt) => eventHandler((T) evt);
                HandlersByName[eventHandler] = newHandler;

                if (ActionsPerEvent.TryGetValue(typeof(T), out Action<GameEvent> actionDelegate))
                    ActionsPerEvent[typeof(T)] = actionDelegate += newHandler;
                else
                    ActionsPerEvent[typeof(T)] = newHandler;
            }
        }

        // Removes a game event listener
        public static void RemoveEventListener<T>(Action<T> eventHandler) where T : GameEvent
        {
            // Find the event handler
            if (HandlersByName.TryGetValue(eventHandler, out var actionDelegate))
            {
                // Find the action delegate containing the event handler
                if (ActionsPerEvent.TryGetValue(typeof (T), out var tempActionDelegate))
                {
                    // Remove the event handler from the action delegate list
                    tempActionDelegate -= actionDelegate;
                    // If there are no more event handlers subscribed to the event,
                    // remove the event from the dictionary
                    if (tempActionDelegate == null)
                        ActionsPerEvent.Remove(typeof(T));
                    else
                        ActionsPerEvent[typeof(T)] = tempActionDelegate;
                }
                // Remove the event handler from the dictionary
                HandlersByName.Remove(eventHandler);
            }
        }

        // Notifies the subscribed event handlers of the event and invokes them synchronously
        public static void Broadcast(GameEvent evt)
        {
            if (ActionsPerEvent.TryGetValue(evt.GetType(), out var actionDelegate))
                actionDelegate.Invoke(evt);
        }

        // Clears the dictionaries
        public static void Clear()
        {
            HandlersByName.Clear();
            ActionsPerEvent.Clear();
        }
    }
}
