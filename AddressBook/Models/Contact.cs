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

  }
}