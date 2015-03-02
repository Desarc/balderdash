using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Desarc.Balderdash.Server
{
    public class GameHub : Hub
    {
        private const string PlayersGroupName = "players";

        private static List<Player> m_players = new List<Player>();

        private static string m_gameBoardConnectionId;
        private static bool m_gameRunning = false;

        public GameHub()
        {
            
        }

        public static Game Game { get; private set; }

        public void PlayerLogon(string playerName)
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine("Logon from {0}, playerName: {1}", Context.ConnectionId, playerName);

            if (!m_players.Exists(p => p.ConnectionId == connectionId)) 
            {
                m_players.Add(new Player(connectionId, playerName));
                Groups.Add(connectionId, PlayersGroupName);
                //Clients.Client(m_gameBoardConnectionId).NewPlayer(playerName);
            }
        }

        public void StartGame()
        {
            //if (m_gameRunning)
            //{
            //    return;
            //}

            Console.WriteLine("{0} has started a new game!", Context.ConnectionId);

            m_gameRunning = true;
            Game = new Game();
            PublishQuestion(Game.GetRandomQuestion().QuestionText);
        }

        public void SubmitAnswer()
        {

        }

        public void ChooseAnswer()
        {

        }

        public void UpvoteAnswer()
        {

        }

        public void ContinuePlaying()
        {
            ResetGame();
        }

        public void ChangePlayers()
        {
            ResetPlayers();
            ResetGame();
        }

        public void GameBoardConnected()
        {
            m_gameBoardConnectionId = Context.ConnectionId;
            ResetPlayers();
            ResetGame();
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Connection from {0}", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("OnDisconnected {0}, from {1}", stopCalled, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("OnReconnected from {0}", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void ResetGame()
        {
            foreach (var player in m_players)
            {
                player.ResetScore();
            }
        }

        private void ResetPlayers()
        {
            m_players = new List<Player>();
        }

        private void PublishGameStarted()
        {
            Clients.All.GameStarted();
        }

        private void PublishGameEnded()
        {
            Clients.All.GameEnded();
        }

        private void PublishQuestion(string question)
        {
            Clients.All.newQuestion(question);
        }

        private void PublishAnswers(List<string> answers)
        {
            Clients.All.publishAnswers(answers.ToArray());
        }

        private void TimerTick(string question)
        {
            Clients.Client(m_gameBoardConnectionId).TimerTick();
        }
    }
}