using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    #region Public Variables

    // The message manager instance 
    public static MessageManager messageManagerInstance;

    public enum MessageReceiver
    {
        EventManger = 0,
        InputManager,
        AudioManager,
    }

    public struct Message
    {
        public MessageReceiver Receiver;
        public string message;
    }

    #endregion

    #region Private Variables

    Queue<Message> messageQueue;

    #endregion

    #region Protected Variables



    #endregion

    #region Public Functions


    /// <summary>
    /// Sends off a message to be added to the messageQueue in the MessageManager
    /// </summary>
    /// <param _message="Message sent to MessageManager"></param>
    public void SendMessage(Message _message)
    {
        Debug.Log("Message Manager: Message Sent to MessageManager and added to Queue");

        messageQueue.Enqueue(_message);
    }

    #endregion

    #region Private Functions
    void Start()
    {
        if (messageManagerInstance == null)
        {
            messageManagerInstance = new MessageManager();
        }
        else
        {
            return;
        }

        messageQueue = new Queue<Message>();

    }

    void Update()
    {
        ProcessQueue();
    }

    private void ProcessQueue()
    {
        Debug.Log("Message Manager: Processing message queue");

        while(messageQueue.Count > 0)
        {
            Message currMessage =  messageQueue.Peek();

            switch(currMessage.Receiver)
            {
                case MessageReceiver.AudioManager:
                    Debug.Log("Message Manager: Message Sent to AudioManager");
                    break;
                case MessageReceiver.EventManger:
                    Debug.Log("Message Manager: Message Sent to EventManger");
                    break;
                case MessageReceiver.InputManager:
                    Debug.Log("Message Manager: Message Sent to InputManager");
                    break;
            }
        }

        Debug.Log("Message Manager: Processing message queue Completed");
    }

    #endregion

    #region Protected Functions
    #endregion

}
