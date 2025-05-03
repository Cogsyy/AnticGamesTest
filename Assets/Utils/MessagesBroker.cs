using System.Collections.Generic;
using UnityEngine;
using System;

public enum MessagingType
{
    None = 0,
    GameStarted,
    ResetGame,
    EnemyReachedFlag,
    UnitDied,
}

public class MessagesBroker : MonoSingleton<MessagesBroker>
{
    private static Dictionary<MessagingType, Delegate> _messageList = new Dictionary<MessagingType, Delegate>();

    public void AddListener(MessagingType message, Action action)
    {
        AddListenerGeneric(message, action);
    }

    public void AddListener<T>(MessagingType message, Action<T> action)
    {
        AddListenerGeneric(message, action);
    }

    private void AddListenerGeneric(MessagingType message, Delegate action)
    {
        if (_messageList.ContainsKey(message))
        {
            _messageList[message] = Delegate.Combine(_messageList[message], action);
        }
        else
            _messageList.Add(message, action);
    }

    public void RemoveListener(MessagingType message, Action action)
    {
        RemoveListenerDelegate(message, action);
    }

    public void RemoveListener<T>(MessagingType message, Action<T> action)
    {
        RemoveListenerDelegate(message, action);
    }

    private void RemoveListenerDelegate(MessagingType message, Delegate action)
    {
        bool success = false;

        success |= TryRemoveListener(message, action);

        if (!success)
        {
            Debug.LogError("Trying to remove a listener that has not been added to the broker: " + message);
        }
    }

    private bool TryRemoveListener(MessagingType messageType, Delegate handler)
    {
        if (!_messageList.ContainsKey(messageType))
        {
            return false;
        }

        _messageList[messageType] = Delegate.Remove(_messageList[messageType], handler);

        if (_messageList[messageType] == null)
        {
            _messageList.Remove(messageType);
        }

        return true;
    }

    public bool CheckList(MessagingType messageType)
    {
        if (_messageList.ContainsKey(messageType))
        {
            return true;
        }
        else return false;
    }

    public void SendMessage(MessagingType message)
    {
        if (!_messageList.ContainsKey(message))
        {
            Debug.LogWarning("MessagesBroker does not contain message: " + message + ", this might not be a problem if no one is listening for the message.");
            return;
        }

        (_messageList[message] as Action)?.Invoke();
    }

    public void SendMessage<T>(MessagingType message, T param)
    {
        if (!_messageList.ContainsKey(message))
        {
            Debug.LogError("MessagesBroker does not contain message: " + message);
            return;
        }

        (_messageList[message] as Action<T>)?.Invoke(param);
    }
}
