using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/stylists")]
        public ActionResult Stylists()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpPost("/stylists")]
        public ActionResult AddStylist()
        {
            string stylistname = Request.Form["new_stylist_name"];
            Stylist newStylist = new Stylist(stylistname);
            newStylist.Save();
            return View(newStylist);
        }

        [HttpGet("/stylists/new")]
        public ActionResult StylistForm()
        {
            return View();
        }

        [HttpGet("/stylist/{stylistid}")]
        public ActionResult StylistDetails(int stylistid)
        {
            Stylist selectedStylist = Stylist.Find(stylistid);
            return View(selectedStylist);
        }

        [HttpGet("/clients")]
        public ActionResult Clients()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpPost("/clients/{stylistid}/{clientid}")]
        public ActionResult AddClient()
        {
            string clientname = Request.Form["new_client_name"];
            string clientaddress = Request.Form["new_client_address"];
            string clientphonenumber = Request.Form["new_client_phonenumber"];

            Find(stylistId);

            Client newClient = new Client(clientname, clientaddress, clientphonenumber, stylistid);
            newClient.Save();
            return View(newClient);
        }

        [HttpGet("/clients/new")]
        public ActionResult ClientForm()
        {
            return View();
        }

        [HttpGet("/clients/{stylistid}/{clientid}")]
        public ActionResult ClientDetails(int clientid)
        {
            Client selectedClient = Client.Find(clientid);
            return View(selectedClient);
        }
    }
}
