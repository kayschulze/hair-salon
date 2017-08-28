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

        [HttpGet("/stylists/{stylistid}")]
        public ActionResult StylistDetails(int stylistid)
        {
            List<Client> StylistClients = Client.GetAllStylistClients(stylistid);
            return View(StylistClients);
        }

        [HttpGet("/clients")]
        public ActionResult Clients()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpPost("/clients/add/{stylistid}")]
        public ActionResult AddClient(int stylistid)
        {
            string clientname = Request.Form["new-client-name"];
            string clientaddress = Request.Form["new-client-address"];
            string clientphonenumber = Request.Form["new-client-phonenumber"];

            Client newClient = new Client(clientname, clientaddress, clientphonenumber, stylistid);
            newClient.Save();
            return View(newClient);
        }

        [HttpGet("/clients/new/{stylistid}")]
        public ActionResult ClientForm(int stylistid)
        {
            return View(stylistid);
        }

        [HttpGet("/clients/{clientid}")]
        public ActionResult ClientDetails(int clientid)
        {
            Client selectedClient = Client.Find(clientid);
            return View(selectedClient);
        }

        [HttpGet("/clients/update/{stylistid}/{clientid}")]
        public ActionResult UpdateClient(int clientid)
        {
            Client clientToUpdate = Client.Find(clientid);
            return View(clientToUpdate);
        }

        [HttpPost("/clients/update/{stylistid}/{clientid}")]
        public ActionResult ClientUpdated(int stylistid, int clientid)
        {
            string updatedclientname = Request.Form["updated-client-name"];
            string updatedclientaddress = Request.Form["updated-client-address"];
            string updatedclientphonenumber = Request.Form["updated-client-phonenumber"];

            Client updatedClient = new Client("", "", "", stylistid, clientid);
            updatedClient.UpdateClient(updatedclientname, updatedclientaddress, updatedclientphonenumber, stylistid);
            return View(updatedClient);
        }

        [HttpGet("/clients/delete/{clientid}")]
        public ActionResult DeleteClient(int clientid)
        {
            Client.DeleteClient(clientid);
            return View();
        }
    }
}
