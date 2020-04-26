using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace next_mole_server.Models
{
    public class Game
    {
        int gameNum;
        DateTime startTime;
        DateTime endTime;
        string urlLink;
        DateTime creationDate;
        int amountPlayed;

        public int GameNum { get => gameNum; set => gameNum = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public string UrlLink { get => urlLink; set => urlLink = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public int AmountPlayed { get => amountPlayed; set => amountPlayed = value; }
    }
}