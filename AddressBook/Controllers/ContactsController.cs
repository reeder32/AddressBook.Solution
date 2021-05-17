using Microsoft.AspNetCore.Mvc;
using AddressBook.Models;
using System.Collections.Generic;
using System;

namespace AddressBook.Controllers
{
  public class ContactsController : Controller
  {
    [HttpGet("/contacts")]
    public ActionResult Index()
    {
      List<Contact> allContacts = Contact.GetAll();
      return View(allContacts);
    }

    [HttpGet("/contacts/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpPost("/contacts")]
    public ActionResult Create(string firstName, string lastName)
    {
      Contact justName = new Contact(firstName, lastName);
      justName.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/contacts/{id}")]
    public ActionResult Show(int id)
    {
      Contact currentContact = Contact.Find(id);
      return View(currentContact);
    }

  }
}