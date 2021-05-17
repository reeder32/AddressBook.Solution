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
  }
}