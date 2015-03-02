using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desarc.Balderdash.Server
{
    public class Player
    {
        private int m_score = 0;

        public Player(string connectionId, string playerName)
        {
            PlayerName = playerName;
            ConnectionId = connectionId;
        }

        public string PlayerName { get; private set; }

        public string ConnectionId { get; private set; }

        public void AddToScore(int score)
        {
            m_score += score;
        }

        public void ResetScore()
        {
            m_score = 0;
        }

        public int GetCurrentScore()
        {
            return m_score;
        }

    }
}