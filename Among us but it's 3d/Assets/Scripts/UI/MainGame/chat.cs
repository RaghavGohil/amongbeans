using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
 
 
namespace Visyde
{
    /// <summary>
    /// Chat System
    /// - manages the chat system itself as well as the chat's UI in one script
    /// </summary>
 
 
    public class chat : Photon.MonoBehaviour, Photon.Chat.IChatClientListener
    {
        [Header("References:")]
        public Text messageDisplay;
        public InputField inputField;
        private bool dis;
		
		[HideInInspector] public ChatClient chatClient;
        public bool HasChatAppID{
			get{
                return !string.IsNullOrEmpty("29296f13-61b8-447e-bc1c-97cbc89328e1");
            }
		}
 
 
        // Internals:
        VerticalLayoutGroup vlg;
 
 
        // Use this for initialization
        void Start()
        {
            dis = false;
            vlg = messageDisplay.transform.parent.GetComponent<VerticalLayoutGroup>();
			//ConnectToChat();
			ExitGames.Client.Photon.ConnectionProtocol connectProtocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
			chatClient = new ChatClient(this , connectProtocol);
			chatClient.Connect("29296f13-61b8-447e-bc1c-97cbc89328e1",  "0.4", new Photon.Chat.AuthenticationValues(PhotonNetwork.playerName));
		}
 
 
        // Update is called once per frame
        void Update()
        {
            
			chatClient.Service();
            // Functionalities:
            if (Input.GetKeyDown(KeyCode.Return)){
                SendChatMessage();
            }

        }
 
 
        public void ConnectToChat(){
            if (HasChatAppID)
            {
                // Only connect if we have a chat ID specified in the PhotonServerSettings:
                
            }
        }
        public void SendChatMessage(){
            if (!string.IsNullOrEmpty(inputField.text))
            {
                chatClient.PublishMessage(PhotonNetwork.room.Name, inputField.text);
                inputField.text = string.Empty;
            }
        }
        public void SendSystemChatMessage(string message){
            DisplayChat(message, "", false, true);
        }
        void DisplayChat(string message, string from, bool ours, bool systemMessage){
 
 
            string finalMessage = systemMessage ? "\n" + message + "" : "\n[" + (ours ? "<color=cyan>" : "<color=white>") + from + "</color>]: " + message;
            messageDisplay.text += finalMessage;
 
 
            // Canvas refresh:
            Canvas.ForceUpdateCanvases();
            vlg.enabled = false;
            vlg.enabled = true;
        }
 
 
        // Photon stuff:
        void OnJoinedRoom()
        {
            messageDisplay.text = "";
            // Connect to the chat server when we join a room:
            ConnectToChat();
        }
        void OnLeftRoom()
        {
            // Disconnect from the chat server when we leave a room:
            chatClient.Disconnect();
        }
        public void OnChatStateChange(ChatState state)
        {
            // use OnConnected() and OnDisconnected()
            // this method might become more useful in the future, when more complex states are being used.
 
 
            //this.StateText.text = state.ToString();
        }
        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            
        }
        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            int last = senders.Length - 1;
            DisplayChat (messages[last].ToString(), senders[last], senders[last] == PhotonNetwork.playerName, false);
        }
        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            // as the ChatClient is buffering the messages for you, this GUI doesn't need to do anything here
            // you also get messages that you sent yourself. in that case, the channelName is determinded by the target of your msg
            //this.InstantiateChannelButton(channelName);
 
 
            byte[] msgBytes = message as byte[];
            if (msgBytes != null)
            {
                Debug.Log("Message with byte[].Length: " + msgBytes.Length);
            }
            /*if (this.selectedChannelName.Equals(channelName))
            {
                ShowChannel(channelName);
            }*/
        }
        public void OnUserSubscribed(string channel, string user)
        {
        }
        public void OnUserUnsubscribed(string channel, string user)
        {
            Debug.LogFormat("OnUserUnsubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
        }
        public void OnConnected()
        {
            chatClient.Subscribe(new string[] { PhotonNetwork.room.Name }, 10); // subscribe to the chat channel once connected to the chat server
        }
        public void OnDisconnected()
        {
            chatClient.Connect("29296f13-61b8-447e-bc1c-97cbc89328e1",  "0.4", new Photon.Chat.AuthenticationValues(PhotonNetwork.playerName));
        }
        public void OnSubscribed(string[] channels, bool[] results)
        {
            
        }
        public void OnUnsubscribed(string[] channels)
        {
            /*foreach (string channelName in channels)
            {
                if (this.channelToggles.ContainsKey(channelName))
                {
                    Toggle t = this.channelToggles[channelName];
                    Destroy(t.gameObject);
 
 
                    this.channelToggles.Remove(channelName);
 
 
                    Debug.Log("Unsubscribed from channel '" + channelName + "'.");
 
 
                    // Showing another channel if the active channel is the one we unsubscribed from before
                    if (channelName == selectedChannelName && channelToggles.Count > 0)
                    {
                        IEnumerator<KeyValuePair<string, Toggle>> firstEntry = channelToggles.GetEnumerator();
                        firstEntry.MoveNext();
 
 
                        ShowChannel(firstEntry.Current.Key);
 
 
                        firstEntry.Current.Value.isOn = true;
                    }
                }
                else
                {
                    Debug.Log("Can't unsubscribe from channel '" + channelName + "' because you are currently not subscribed to it.");
                }
            }*/
        }
        public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
        {
            if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
            {
                UnityEngine.Debug.LogError(message);
            }
            else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
            {
                UnityEngine.Debug.LogWarning(message);
            }
            else
            {
                UnityEngine.Debug.Log(message);
            }
        }
    }
}
