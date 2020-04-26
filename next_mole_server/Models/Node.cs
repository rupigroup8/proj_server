using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using next_mole_server.Models.DAL;

using Newtonsoft.Json;

namespace next_mole_server.Models
{
    public class Node
    {
        int nodeId;
        string nodeNum;
        string nodeDescription;

        public int NodeId { get => nodeId; set => nodeId = value; }
        public string NodeNum { get => nodeNum; set => nodeNum = value; }
        public string NodeDescription { get => nodeDescription; set => nodeDescription = value; }
        public static object Server { get; private set; }

        public static int postNodes(List<Node> nodes)
        {
            DBservices dbs = new DBservices();
            int num = dbs.DBinsertNodes(nodes);
            return num;
        }
        
    }
}