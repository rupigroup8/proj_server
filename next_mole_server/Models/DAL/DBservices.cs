using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;


namespace next_mole_server.Models.DAL
{
    public class DBservices
    {
        public SqlConnection connect(string str)
        {
            // read the connection string from the configuration file with the constring name
            string cStr = WebConfigurationManager.ConnectionStrings[str].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        public SqlCommand command(SqlConnection con, string CommandSTR)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con; // assign the connection to the command object
            cmd.CommandText = CommandSTR;
            return cmd;
        }

        public int DBinsertNodes(List<Node> nodes)
        {
            SqlConnection con;
            SqlCommand cmd;
            int numEffected = 0;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (Node item in nodes)
            {
                String CommandSTR = NodeInsertCommand(item);
                try
                {
                    cmd = command(con, CommandSTR);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    numEffected += cmd.ExecuteNonQuery(); // Execute the command
                }
                catch (Exception ex)
                {
                    return 0;
                    throw (ex);
                }
            }
            if (con != null)
            {
                con.Close();
            }
            return numEffected;
        }
        public string NodeInsertCommand(Node node)
        {
            string str = "IF NOT EXISTS(SELECT * FROM node WHERE nodeNum = '" + node.NodeNum + "')";
            string prefix = "INSERT INTO node(nodeNum, nodeDescription)";
            string cmdValues = string.Format("VALUES('{0}', '{1}')", node.NodeNum, node.NodeDescription);
            string all = str + prefix + cmdValues;

            return all;
        }
        public int DBinsertLinks(List<Link> links)
        {
            SqlConnection con;
            SqlCommand cmd;
            int numEffected = 0;
            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            foreach (Link item in links)
            {
                String CommandSTR = LinkInsertCommand(item);
                try
                {
                    cmd = command(con, CommandSTR);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    numEffected += cmd.ExecuteNonQuery(); // Execute the command
                }
                catch (Exception ex)
                {
                    return 0;
                    throw (ex);
                }

                //finally
                //{
                //    if (con == null)
                //    {
                //        con.Close();        //final step, Close
                //    }
                //}
            }
            if (con != null)
            {
                con.Close();
            }
            return numEffected;
        }
        public string LinkInsertCommand(Link link)
        {
            string prefix = "INSERT INTO link(sourceNode , targetNode , connectionType, connectionWeight  )";
            string cmdValues = string.Format("VALUES('{0}', '{1}', '{2}', '{3}')", link.SourceNode,link.TargetNode, link.ConnectionType, link.ConnectionWeight);
            
            return prefix + cmdValues;
        }
        public List<Link> deleteConnection(string connection)
        {
            List<Link> LinksList = new List<Link>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "DELETE FROM link WHERE id='" + connection + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                return getAllLinks();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public List<Link> getAllLinks()
        {
            List<Link> LinksList = new List<Link>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM link";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Link link = new Link();

                    link.LinkNum = Convert.ToInt32(dr["linkNum"]);
                    link.SourceNode = (string)dr["SourceNode"];
                    link.TargetNode = (string)dr["TargetNode"];
                    link.ConnectionType = (string)dr["ConnectionType"];
                    link.ConnectionWeight = Convert.ToDouble(dr["ConnectionWeight"]);
                    
                    LinksList.Add(link);
                }
                return LinksList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public List<Node> deleteNode(string id)
        {
            List<Node> NodesList = new List<Node>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                string getConnection= "SELECT ConnectionType from node WHERE NodeNum = '"+id+"'";
                deleteConnection(getConnection);
                String selectSTR = "DELETE FROM Node WHERE NodeNum='" + id + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                return getAllNodes();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        public List<Node> getAllNodes()
        {
            List<Node> NodesList = new List<Node>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM node";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Node node = new Node();

                    node.NodeId = Convert.ToInt32(dr["NodeId"]);
                    node.NodeNum = (string)dr["NodeNum"];
                    node.NodeDescription = (string)dr["NodeDescription"];

                    NodesList.Add(node);
                }

                return NodesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        public bool checkLogin(string email, string password)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM userPlayer WHERE userEmail='" + email + "' AND userPassword='" + password + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public bool checkEmail(string email)
        {                                       //valid that there is no other user with this email 
                                                   //that is allready registered
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM userPlayer WHERE userEmail='" + email + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                if (dr.Read())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public int DBinsertUser(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = UserInsertCommand(user);      // helper method to build the insert string

            cmd = command(con, cStr);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
       
        public string UserInsertCommand(User user)
        {
            string prefix = "INSERT INTO userPlayer(userEmail, userPassword, userName, gender)";
            string cmdValues = string.Format("VALUES('{0}', '{1}', '{2}', '{3}')", user.UserEmail, user.UserPassword, user.UserName, user.Gender);
            return prefix + cmdValues;
        }
        public List<User> getAllusers()
        {
            List<User> ManagersList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM userPlayer";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    User m = new User();
                    m.UserEmail = (string)dr["userName"];
                    m.UserPassword = (string)dr["email"];

                    ManagersList.Add(m);
                }

                return ManagersList;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

    }
}