using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace AddressBook.Models
{
  public class Contact
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Contact(string firstname, string lastname)
    {
      FirstName = firstname;
      LastName = lastname;
    }

    public Contact(string firstname, string lastname, int id)
    {
      FirstName = firstname;
      LastName = lastname;
      Id = id;
    }
    public static List<Contact> GetAll()
    {
      List<Contact> allContacts = new List<Contact> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Contacts;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int ContactId = rdr.GetInt32(0);
        string ContactFirstName = rdr.GetString(1);
        string ContactLastName = rdr.GetString(2);
        Contact newContact = new Contact(ContactFirstName, ContactLastName, ContactId);
        allContacts.Add(newContact);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allContacts;
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM contacts;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      // Begin new code

      cmd.CommandText = @"INSERT INTO contacts (first_name, last_name) VALUES (@contactFirstName, @contactLastName);";
      MySqlParameter firstName = new MySqlParameter();
      firstName.ParameterName = "@contactFirstName";
      firstName.Value = this.FirstName;
      cmd.Parameters.Add(firstName);

      MySqlParameter lastName = new MySqlParameter();
      lastName.ParameterName = "@contactLastName";
      lastName.Value = this.LastName;
      cmd.Parameters.Add(lastName);
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      // End new code

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Contact Find(int id)
    {
      // We open a connection.
      MySqlConnection conn = DB.Connection();
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM contacts WHERE id = @thisId;";

      // We have to use parameter placeholders (@thisId) and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int contactId = 0;
      string firstName = "";
      string lastName = "";
      while (rdr.Read())
      {
        contactId = rdr.GetInt32(0);
        firstName = rdr.GetString(1);
        lastName = rdr.GetString(2);
      }
      Contact foundItem = new Contact(firstName, lastName, contactId);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }
  }
}