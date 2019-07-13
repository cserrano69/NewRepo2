using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PreFinalOp.Models
{
    public class DBHandler
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["MacProj2"].ToString();
            con = new SqlConnection(constring);
        }

        public bool InsertItem(Person person)
        {
            connection();
            string query = "INSERT INTO Person VALUES('" + person.Name + "','" + person.Amount + "','" + person.MF + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<Person> GetPeople()
        {
            connection();
            List<Person> people = new List<Person>();
            string query = "SELECT * FROM Person";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach(DataRow dr in dt.Rows)
            {
                people.Add(new Person
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Amount = Convert.ToDecimal(dr["Amount"]),
                    MF = Convert.ToInt32(dr["MF"])
                });
            }
            return people;
        }

        //upd

        public bool UpdatePerson(Person person)
        {
            connection();
            string query = "UPDATE Person SET Name = '" + person.Name + "', Amount'" + person.Amount + "', MF'" + person.MF + " WHERE ID = " + person.ID;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeletePerson(int Id)
        {
            connection();
            string query = "DELETE FROM Person WHERE ID = " + Id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool newUser(Login login)
        {
            connection();
            string query = "INSERT INTO Person VALUES('" + login.Username + "','" + login.Password + ")";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<Login> logins()
        {
            connection();
            List<Login> logins = new List<Login>();

            string query = "SELECT * FROM Login";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach(DataRow dr in dt.Rows)
            {
                logins.Add(new Login
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Username = Convert.ToString(dr["Username"]),
                    Password = Convert.ToString(dr["Password"])
                });
            }

            return logins;
        }

        public List<Login> GetLogin()
        {
            connection();
            List<Login> logins = new List<Login>();
            string query = "SELECT * FROM Login";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                logins.Add(new Login
                {
                    ID = Convert.ToInt32(dr["LoginID"]),
                    Username = Convert.ToString(dr["Username"]),
                    Password = Convert.ToString(dr["Password"]),
                    MFID = Convert.ToInt32(dr["MFID"])
                });
            }
            return logins;
        }

        public bool UpdateLogin(Login login)
        {
            connection();
            string query = "UPDATE Login SET Username = '" + login.Username + "', Password'" + login.Password + "', MFID'" + login.MFID + " WHERE ID = " + login.ID;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}