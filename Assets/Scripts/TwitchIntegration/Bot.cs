using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DefaultNamespace;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Polls.CreatePoll;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using TwitchLib.Unity;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : MonoBehaviour
{
		[SerializeField] private bool _debug = false;
		[SerializeField] private string _channelToConnectTo = "carlboisvertdev";
		private string clientId = "zcu594r9230f6b6on4igl7lrvmq24u";
		private string clientSecret = "jx43ydlmedhrqd1nfmdkm7s95ut60t";
    
		private Api API;
    	private Client _client;
        private Token token;
    
    	async private void Start()
    	{
	        token = await GetToken();
	        token.access_token = "wmtm0f5nas8r543ym22whbkovsg14v";
	        // To keep the Unity application active in the background, you can enable "Run In Background" in the player settings:
    		// Unity Editor --> Edit --> Project Settings --> Player --> Resolution and Presentation --> Resolution --> Run In Background
    		// This option seems to be enabled by default in more recent versions of Unity. An aditional, less recommended option is to set it in code:
    		// Application.runInBackground = true;
    
    		//Create Credentials instance
    		ConnectionCredentials credentials = new ConnectionCredentials(_channelToConnectTo, token.access_token);
			
    		// Create new instance of Chat Client
            _client = new Client();
    
    		// Initialize the client with the credentials instance, and setting a default channel to connect to.
    		_client.Initialize(credentials, _channelToConnectTo);
            
            // Bind callbacks to events
    		_client.OnConnected += OnConnected;
            _client.OnError += OnError;
            _client.OnLog += OnLog;
    		_client.OnJoinedChannel += OnJoinedChannel;
    		_client.OnMessageReceived += OnMessageReceived;

            // Connect
    		bool connected = _client.Connect();
            Debug.Log($"Connected: {connected}");
        }

        private void OnLog(object sender, OnLogArgs e)
        {
	        if (_debug)
	        {
		        Debug.Log(e.Data);
	        }
        }

        private void OnError(object sender, OnErrorEventArgs e)
        {
	        Debug.LogError(e.Exception.Message);
        }

        private void OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
    	{
    		Debug.Log($"The bot {e.BotUsername} succesfully connected to Twitch.");
    
    		if (!string.IsNullOrWhiteSpace(e.AutoJoinChannel))
    			Debug.Log($"The bot will now attempt to automatically join the channel provided when the Initialize method was called: {e.AutoJoinChannel}");
    	}
    
    	private void OnJoinedChannel(object sender, TwitchLib.Client.Events.OnJoinedChannelArgs e)
    	{
	        _client.SendMessage(e.Channel, "Let's get this party going chat! PogChamp");
    	}
    
    	private void OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    	{
	        BotEvents.OnBotCommand(e.ChatMessage);
        }
    
    	private void Update()
    	{
    		// Don't call the client send message on every Update,
    		// this is sample on how to call the client,
    		// not an example on how to code.
    		if (Input.GetKeyDown(KeyCode.Space))
    		{
    			_client.SendMessage(_channelToConnectTo, "I pressed the space key within Unity.");
    		}
    	}

        async private Task<Token> GetToken()
        {
	        HttpClient client = new HttpClient();
	        
	        Dictionary<string, string> values = new Dictionary<string, string>
	        {
		        { "client_id", clientId },
		        { "client_secret", clientSecret },
		        { "grant_type", "client_credentials" }
	        };
	        HttpContent content = new FormUrlEncodedContent(values);
	        
	        HttpResponseMessage response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);
	        string responseString = await response.Content.ReadAsStringAsync();

			Token token = JsonUtility.FromJson<Token>(responseString);

			return token;
        }

        /*async private Task<bool> CreatePoll()
        {
	        HttpClient client = new HttpClient();
	        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
	        client.DefaultRequestHeaders.Add("Client-Id",clientId);
	        client.DefaultRequestHeaders.Add("Content-Type","application/json");
	        
	        Dictionary<string, string> values = new Dictionary<string, string>
	        {
		        { "client_id", clientId },
		        { "client_secret", clientSecret },
		        { "grant_type", "client_credentials" }
	        };
	        HttpContent content = new FormUrlEncodedContent(values);

	        HttpResponseMessage response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);
	        string responseString = await response.Content.ReadAsStringAsync();
        }*/

        public struct Token
        {
	        public string access_token;
        }
}
