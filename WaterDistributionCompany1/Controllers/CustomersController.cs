using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Web.Mvc;

namespace WaterDistributionCompany1.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            // Get a reference to the XML DOM
            XmlDocument xdCustomers = new XmlDocument();
            // This is the name and path of the XML file that contains the customers records
            string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

            // If a file that contains the customers records was previously created, ...
            if (System.IO.File.Exists(strFileCustomers))
            {
                // ... open it
                using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    // and store the records in the DOM
                    xdCustomers.Load(fsCustomers);
                }

                /* If the Customers records exist, send them to the view.
                 * If there is no file for the customers, indicate that the DOM is null. */
                ViewBag.Customers = xdCustomers.DocumentElement.ChildNodes.Count > 0 ? xdCustomers.DocumentElement.ChildNodes : null;
            }

            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            XmlDocument xdCustomers = new XmlDocument();
            string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

            if (System.IO.File.Exists(strFileCustomers))
            {
                using (FileStream fsWaterMeters = new FileStream(strFileCustomers, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdCustomers.Load(fsWaterMeters);

                    XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("account-id");

                    foreach (XmlNode xnCustomer in xnlCustomers)
                    {
                        if (xnCustomer.InnerText == id.ToString())
                        {
                            ViewBag.AccountID = xnCustomer.InnerText;
                            ViewBag.AccountNumber = xnCustomer.NextSibling.InnerText;
                            ViewBag.MeterNumber = xnCustomer.NextSibling.NextSibling.InnerText;
                            ViewBag.FirstName = xnCustomer.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.LastName = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.Address = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.City = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.County = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.State = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.ZIPCode = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int account_id = -1;
                bool meterNumberIsValid = false;
                XmlDocument xdWaterMeters = new XmlDocument();
                XmlDocument xdCustomersAccounts = new XmlDocument();
                string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");
                string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

                // Make sure the user provides an account number, ...
                if (!string.IsNullOrEmpty(collection["AccountNumber"]))
                {
                    // If the user provided an account number, to start, find out if a file for water meters was created already.
                    if (System.IO.File.Exists(strFileWaterMeters))
                    {
                        // If a file for water meters exists, open it
                        using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open,
                                                                                             FileAccess.Read,
                                                                                             FileShare.Read))
                        {
                            // Store the list of water meters in an XML document
                            xdWaterMeters.Load(fsWaterMeters);

                            // Create a list of child nodes of the root node
                            XmlNodeList xnlWaterMeters = xdWaterMeters.DocumentElement.ChildNodes;

                            // Visit each node of the list of elements
                            foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                            {
                                // When you get to a list of (child) nodes of a water-meter node, visit each child node
                                foreach (XmlNode xnMeterNumber in xnWaterMeter.ChildNodes)
                                {
                                    // If you find a meter number that is the same as the meter number from the form, ...
                                    if (xnMeterNumber.InnerText == collection["MeterNumber"])
                                    {
                                        // ... make a note
                                        meterNumberIsValid = true;
                                    }
                                }
                            }
                        }
                    }

                    // If either the user didn't provide a meter number or provided a meter number that doesn't exist, ...
                    if (meterNumberIsValid == false)
                    {
                        // ... create a message that will display to the user
                        ViewBag.ErrorMessage = "You must provide a valid meter number";
                    }
                    else
                    {
                        // It appears that the user provided both an account number and a valid meter number.

                        // If an XML file for customers accounts was previously created, ...
                        if (System.IO.File.Exists(strFileCustomers))
                        {
                            // ... open it ...
                            using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.OpenOrCreate,
                                                                                             FileAccess.ReadWrite,
                                                                                             FileShare.ReadWrite))
                            {
                                // ... and put the records in the DOM
                                xdCustomersAccounts.Load(fsCustomers);

                                XmlNodeList xnlCustomers = xdCustomersAccounts.GetElementsByTagName("account-id");

                                foreach (XmlNode xnCustomer in xnlCustomers)
                                {
                                    account_id = int.Parse(xnCustomer.InnerText);
                                }
                            }
                        }
                        else
                        {
                            // If there is no XML file yet for the customers, create skeleton code for an XML document
                            xdCustomersAccounts.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                                        "<customers></customers>");
                            account_id = 0;
                        }

                        // Get ready to create an XML element named customer
                        XmlElement xeCustomer = xdCustomersAccounts.CreateElement("customer");

                        account_id++;

                        // Create the markup of the XML customer
                        string strCustomer = "<account-id>" + account_id + "</account-id>" +
                                             "<account-number>" + collection["AccountNumber"] + "</account-number>" +
                                             "<meter-number>" + collection["MeterNumber"] + "</meter-number>" +
                                             "<first-name>" + collection["FirstName"] + "</first-name>" +
                                             "<last-name>" + collection["LastName"] + "</last-name>" +
                                             "<address>" + collection["Address"] + "</address>" +
                                             "<city>" + collection["City"] + "</city>" +
                                             "<county>" + collection["County"] + "</county>" +
                                             "<state>" + collection["State"] + "</state>" +
                                             "<zip-code>" + collection["ZIPCode"] + "</zip-code>";

                        // Specify the markup of the new element
                        xeCustomer.InnerXml = strCustomer;

                        // Add the new node to the root
                        xdCustomersAccounts.DocumentElement.AppendChild(xeCustomer);

                        // Save the (new version of the) XML file
                        using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            xdCustomersAccounts.Save(fsCustomers);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            XmlDocument xdCustomers = new XmlDocument();
            string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

            if (System.IO.File.Exists(strFileCustomers))
            {
                using (FileStream fsWaterMeters = new FileStream(strFileCustomers, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdCustomers.Load(fsWaterMeters);

                    XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("account-id");

                    foreach (XmlNode xnCustomer in xnlCustomers)
                    {
                        if (xnCustomer.InnerText == id.ToString())
                        {
                            ViewBag.AccountID = xnCustomer.InnerText;
                            ViewBag.AccountNumber = xnCustomer.NextSibling.InnerText;
                            ViewBag.MeterNumber = xnCustomer.NextSibling.NextSibling.InnerText;
                            ViewBag.FirstName = xnCustomer.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.LastName = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.Address = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.City = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.County = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.State = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.ZIPCode = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                XmlDocument xdCustomers = new XmlDocument();
                string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

                XmlElement xeCustomer = xdCustomers.CreateElement("customer");

                // Create the markup of a customer
                string strCustomer = "<account-id>" + id + "</account-id>" +
                                     "<account-number>" + collection["AccountNumber"] + "</account-number>" +
                                     "<meter-number>" + collection["MeterNumber"] + "</meter-number>" +
                                     "<first-name>" + collection["FirstName"] + "</first-name>" +
                                     "<last-name>" + collection["LastName"] + "</last-name>" +
                                     "<address>" + collection["Address"] + "</address>" +
                                     "<city>" + collection["City"] + "</city>" +
                                     "<county>" + collection["County"] + "</county>" +
                                     "<state>" + collection["State"] + "</state>" +
                                     "<zip-code>" + collection["ZIPCode"] + "</zip-code>";

                xeCustomer.InnerXml = strCustomer;

                if (System.IO.File.Exists(strFileCustomers))
                {
                    using (FileStream fsWaterMeters = new FileStream(strFileCustomers, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        xdCustomers.Load(fsWaterMeters);
                    }

                    using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("customer");

                        foreach (XmlNode xnCustomer in xnlCustomers)
                        {
                            if (xnCustomer.FirstChild.InnerText == id.ToString())
                            {
                                xnCustomer.ParentNode.ReplaceChild(xeCustomer, xnCustomer);
                                xdCustomers.Save(fsCustomers);
                                break;
                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            XmlDocument xdCustomers = new XmlDocument();
            string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

            if (System.IO.File.Exists(strFileCustomers))
            {
                using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdCustomers.Load(fsCustomers);

                    XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("account-id");

                    foreach (XmlNode xnCustomer in xnlCustomers)
                    {
                        if (xnCustomer.InnerText == id.ToString())
                        {
                            ViewBag.AccountID = xnCustomer.InnerText;
                            ViewBag.AccountNumber = xnCustomer.NextSibling.InnerText;
                            ViewBag.MeterNumber = xnCustomer.NextSibling.NextSibling.InnerText;
                            ViewBag.FirstName = xnCustomer.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.LastName = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.Address = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.City = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.County = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.State = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewBag.ZIPCode = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                XmlDocument xdCustomers = new XmlDocument();
                string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

                /* Open the XML file that contains customers accounts so you can fill the DOM with records */
                using (FileStream fsWaterMeters = new FileStream(strFileCustomers, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdCustomers.Load(fsWaterMeters);
                }

                // Get ready to change something on the file
                using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.Truncate, FileAccess.Write, FileShare.Write))
                {
                    // Get a collection of customer nodes
                    XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("customer");

                    // Check each node
                    foreach (XmlNode xnCustomer in xnlCustomers)
                    {
                        /* If you find a customer record whose customer-id is
                         * the same as the id of the record the user clicked, ... */
                        if (xnCustomer.FirstChild.InnerText == id.ToString())
                        {
                            // ... ask its parent to delete that record
                            xnCustomer.ParentNode.RemoveChild(xnCustomer);
                            // Now that the record has been deleted, save the XML file
                            xdCustomers.Save(fsCustomers);
                            break;
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
