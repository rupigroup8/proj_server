using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace next_mole_server.Models
{
    public class Network
    {

        int networkId;
        DateTime creationDate;
        DateTime dateUpdated;
        string subject;
        string description;
        string image;
        double density;
        double avgPathLegnth;
        int hubs;
        int isolatedNodes;

        public int NetworkId { get => networkId; set => networkId = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Description { get => description; set => description = value; }
        public string Image { get => image; set => image = value; }
        public double Density { get => density; set => density = value; }
        public double AvgPathLegnth { get => avgPathLegnth; set => avgPathLegnth = value; }
        public int Hubs { get => hubs; set => hubs = value; }
        public int IsolatedNodes { get => isolatedNodes; set => isolatedNodes = value; }
    }
}