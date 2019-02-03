using System;
using System.IO;
using System.Xml;
using System.Web.Mvc;
using System.Collections.Generic;

namespace WaterDistributionCompany1.Controllers
{
    public class WaterBillsController : Controller
    {
        // GET: WaterBills
        public ActionResult Index()
        {
            XmlDocument xdWaterBills = new XmlDocument();
            string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

            if (System.IO.File.Exists(strFileWaterBills))
            {
                using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterBills.Load(fsWaterBills);
                }

                if (xdWaterBills.DocumentElement.ChildNodes.Count > 0)
                {
                    ViewData["Invoices"] = xdWaterBills.DocumentElement.ChildNodes;
                }
                else
                {
                     ViewData["Invoices"] = null;
                }
            }

            return View();
        }

        // GET: WaterBills/Details/5
        public ActionResult Details(int id)
        {
            XmlDocument xdWaterBills = new XmlDocument();
            string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

            if (System.IO.File.Exists(strFileWaterBills))
            {
                using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterBills.Load(fsWaterBills);

                    XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("water-bill-id");

                    foreach (XmlNode xnWaterBill in xnlWaterBills)
                    {
                        if (xnWaterBill.InnerText == id.ToString())
                        {
                             ViewData["WaterBillID"]           = xnWaterBill.InnerText;
                             ViewData["InvoiceNumber"]         = xnWaterBill.NextSibling.InnerText;
                             ViewData["AccountNumber"]         = xnWaterBill.NextSibling.NextSibling.InnerText;
                             ViewData["MeterReadingStartDate"] = xnWaterBill.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["MeterReadingEndDate"]   = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["BillingDays"]           = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["CounterReadingStart"]   = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["CounterReadingEnd"]     = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalHCF"]              = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalGallons"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["First15HCF"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["Next10HCF"]             = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["RemainingHCF"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["SewerCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["StormCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["WaterUsageCharges"]     = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LocalTaxes"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["StateTaxes"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentDueDate"]        = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["AmountDue"]             = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LatePaymentDueDate"]    = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LateAmountDue"]         = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // GET: WaterBills/Create
        public ActionResult Create()
        {
            int water_bill_id = 0;
            XmlDocument xdWaterBills = new XmlDocument();
            string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

            if (System.IO.File.Exists(strFileWaterBills))
            {
                using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterBills.Load(fsWaterBills);

                    XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("water-bill-id");

                    foreach (XmlNode xnWaterBill in xnlWaterBills)
                    {
                        water_bill_id = int.Parse(xnWaterBill.InnerText);
                    }
                }
            }

            ViewData["WaterBillID"] = (water_bill_id + 1);

            Random rndNumber = new Random();
            ViewData["InvoiceNumber"] = rndNumber.Next(100001, 999999).ToString();

            return View();
        }

        // POST: WaterBills/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int bill_id = -1;
                bool customerIsValid = false;
                string meterNumber = string.Empty;
                XmlDocument xdCustomers = new XmlDocument();
                XmlDocument xdWaterBills = new XmlDocument();
                string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");
                string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

                // Make sure the user provided an account number for the customer, ...
                if (!string.IsNullOrEmpty(collection["AccountNumber"]))
                {
                    // If the user provided an account number, find out if an XML file for customers was already created.
                    if (System.IO.File.Exists(strFileCustomers))
                    {
                        // If a file for customers exists, open it
                        using (FileStream fsCustomers = new FileStream(strFileCustomers, FileMode.Open,
                                                                                         FileAccess.Read,
                                                                                         FileShare.Read))
                        {
                            // Store the list of customers in an XML document
                            xdCustomers.Load(fsCustomers);

                            // Create a list of customers nodes that use the account number provided by the user
                            XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("account-number");

                            // Visit each node of the list of elements
                            foreach (XmlNode xnCustomer in xnlCustomers)
                            {
                                // If you find a customer that is the same as the account number from the form, ...
                                if (xnCustomer.InnerText == collection["AccountNumber"])
                                {
                                    // ... make a note
                                    customerIsValid = true;
                                    // and get the meter number used by that customer
                                    meterNumber = xnCustomer.NextSibling.InnerText;
                                }
                            }
                        }
                    }
                }

                if (customerIsValid == true)
                {
                    // It appears that the user provided a valid customer account number.
                    // If an XML file for water bills was previously created, ...
                    if (System.IO.File.Exists(strFileWaterBills))
                    {
                        // ... open it ...
                        using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.OpenOrCreate,
                                                                                           FileAccess.ReadWrite,
                                                                                           FileShare.ReadWrite))
                        {
                            // ... and put the records in the DOM
                            xdWaterBills.Load(fsWaterBills);

                            XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("water-bill-id");

                            foreach (XmlNode xnWaterBill in xnlWaterBills)
                            {
                                bill_id = int.Parse(xnWaterBill.InnerText);
                            }
                        }
                    }
                    else
                    {
                        // If there is no XML file yet for the customers, create skeleton code for an XML document
                        xdWaterBills.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                             "<water-bills></water-bills>");
                        bill_id = 0;
                    }

                    // Get ready to create an XML element named water-bill
                    XmlElement xeWaterBill = xdWaterBills.CreateElement("invoice");

                    bill_id++;

                    // Create the markup of the XML water bill
                    xeWaterBill.InnerXml = "<water-bill-id>"            + bill_id + "</water-bill-id>"        +
                                           "<invoice-number>"           + collection["InvoiceNumber"]         + "</invoice-number>"           +
                                           "<account-number>"           + collection["AccountNumber"]         + "</account-number>"           +
                                           "<meter-reading-start-date>" + collection["MeterReadingStartDate"] + "</meter-reading-start-date>" +
                                           "<meter-reading-end-date>"   + collection["MeterReadingEndDate"]   + "</meter-reading-end-date>"   +
                                           "<billing-days>"             + collection["BillingDays"]           + "</billing-days>"             +
                                           "<counter-reading-start>"    + collection["CounterReadingStart"]   + "</counter-reading-start>"    +
                                           "<counter-reading-end>"      + collection["CounterReadingEnd"]     + "</counter-reading-end>"      +
                                           "<total-hcf>"                + collection["TotalHCF"]              + "</total-hcf>"                +
                                           "<total-gallons>"            + collection["TotalGallons"]          + "</total-gallons>"            +
                                           "<first-15-hcf>"             + collection["First15HCF"]            + "</first-15-hcf>"             +
                                           "<next-10-hcf>"              + collection["Next10HCF"]             + "</next-10-hcf>"              +
                                           "<remaining-hcf>"            + collection["RemainingHCF"]          + "</remaining-hcf>"            +
                                           "<sewer-charges>"            + collection["SewerCharges"]          + "</sewer-charges>"            +
                                           "<storm-charges>"            + collection["StormCharges"]          + "</storm-charges>"            + 
                                           "<water-usage-charges>"      + collection["WaterUsageCharges"]     + "</water-usage-charges>"      +
                                           "<total-charges>"            + collection["TotalCharges"]          + "</total-charges>"            +
                                           "<local-taxes>"              + collection["LocalTaxes"]            + "</local-taxes>"              +
                                           "<state-taxes>"              + collection["StateTaxes"]            + "</state-taxes>"              +
                                           "<payment-due-date>"         + collection["PaymentDueDate"]        + "</payment-due-date>"         +
                                           "<amount-due>"               + collection["AmountDue"]             + "</amount-due>"               +
                                           "<late-payment-due-date>"    + collection["LatePaymentDueDate"]    + "</late-payment-due-date>"    +
                                           "<late-amount-due>"          + collection["LateAmountDue"]         + "</late-amount-due>";

                    // Add the new node to the root
                    xdWaterBills.DocumentElement.AppendChild(xeWaterBill);

                    // Save the (new version of the) XML file
                    using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        xdWaterBills.Save(fsWaterBills);
                    }

                    // We also want to update the counter value on the water meter with the new Counter Reading End value
                    XmlDocument xdWaterMeters = new XmlDocument();
                    string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

                    if (System.IO.File.Exists(strFileWaterMeters))
                    {
                        using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            xdWaterMeters.Load(fsWaterMeters);
                        }

                        XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("meter-id");

                        using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                            {
                                if (xnWaterMeter.NextSibling.InnerText == meterNumber)
                                {
                                    xnWaterMeter.ParentNode.InnerXml = "<meter-id>"         + xnWaterMeter.InnerText + "</meter-id>" +
                                                                       "<meter-number>"     + meterNumber + "</meter-number>" +
                                                                       "<make>"             + xnWaterMeter.NextSibling.NextSibling.InnerText + "</make>" +
                                                                       "<model>"            + xnWaterMeter.NextSibling.NextSibling.NextSibling.InnerText + "</model>" +
                                                                       "<meter-size>"       + xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.InnerText + "</meter-size>" +
                                                                       "<date-last-update>" + xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText + "</date-last-update>" +
                                                                       "<counter-value>"    + collection["CounterReadingEnd"] + "</counter-value>";
                                    xdWaterMeters.Save(fsWaterMeters);
                                    break;
                                }
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

        // GET: WaterBills/Edit/5
        public ActionResult Edit(int id)
        {
            XmlDocument xdWaterBills = new XmlDocument();
            string strAccountNumber = string.Empty, strMeterNumber = string.Empty;
            string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

            if (System.IO.File.Exists(strFileWaterBills))
            {
                using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterBills.Load(fsWaterBills);

                    XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("water-bill-id");

                    foreach (XmlNode xnWaterBill in xnlWaterBills)
                    {
                        if (xnWaterBill.InnerText == id.ToString())
                        {
                             ViewData["WaterBillID"]           = xnWaterBill.InnerText;
                             ViewData["InvoiceNumber"]         = xnWaterBill.NextSibling.InnerText;
                             ViewData["AccountNumber"]         = xnWaterBill.NextSibling.NextSibling.InnerText;
                             ViewData["MeterReadingStartDate"] = xnWaterBill.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["MeterReadingEndDate"]   = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["BillingDays"]           = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["CounterReadingStart"]   = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["CounterReadingEnd"]     = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalHCF"]              = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalGallons"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["First15HCF"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["Next10HCF"]             = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["RemainingHCF"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["SewerCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["StormCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["WaterUsageCharges"]     = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LocalTaxes"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["StateTaxes"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentDueDate"]        = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["AmountDue"]             = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LatePaymentDueDate"]    = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LateAmountDue"]         = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;

                            strAccountNumber = xnWaterBill.NextSibling.NextSibling.InnerText;
                        }
                    }
                }

                XmlDocument xdCustomers = new XmlDocument();
                string strFileCustomers = Server.MapPath("/WaterDistribution/Customers.xml");

                if (System.IO.File.Exists(strFileCustomers))
                {
                    using (FileStream fsWaterMeters = new FileStream(strFileCustomers, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xdCustomers.Load(fsWaterMeters);

                        XmlNodeList xnlCustomers = xdCustomers.GetElementsByTagName("account-number");

                        foreach (XmlNode xnCustomer in xnlCustomers)
                        {
                            if (xnCustomer.InnerText == strAccountNumber)
                            {
                                 ViewData["AccountNumber"]   = xnCustomer.InnerText;
                                 ViewData["MeterNumber"]     = xnCustomer.NextSibling.InnerText;
                                 ViewData["CustomerName"]    = xnCustomer.NextSibling.NextSibling.InnerText + " " +
                                                               xnCustomer.NextSibling.NextSibling.NextSibling.InnerText;
                                 ViewData["CustomerAddress"] = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                                 ViewData["CustomerCity"]    = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                                 ViewData["CustomerCounty"]  = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                                 ViewData["CustomerState"]   = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                                 ViewData["CustomerZIPCode"] = xnCustomer.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;

                                strMeterNumber = xnCustomer.NextSibling.InnerText;
                            }
                        }
                    }
                }

                XmlDocument xdWaterMeters = new XmlDocument();
                string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

                if (System.IO.File.Exists(strFileWaterMeters))
                {
                    using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xdWaterMeters.Load(fsWaterMeters);

                        XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("meter-number");

                        foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                        {
                            if (xnWaterMeter.InnerText == strMeterNumber)
                            {
                                 ViewData["MeterDetails"] = xnWaterMeter.InnerText + " " +
                                                       xnWaterMeter.NextSibling.InnerText + " (" +
                                                       xnWaterMeter.NextSibling.NextSibling.NextSibling.InnerText + ")";
                            }
                        }
                    }
                }
            }

            return View();
        }

        // POST: WaterBills/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                XmlDocument xdWaterBills = new XmlDocument();
                string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

                if (System.IO.File.Exists(strFileWaterBills))
                {
                    using (FileStream fsWaterMeters = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        xdWaterBills.Load(fsWaterMeters);
                    }

                    XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("water-bill-id");

                    using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        foreach (XmlNode xnWaterBill in xnlWaterBills)
                        {
                            if (xnWaterBill.InnerText == id.ToString())
                            {
                                xnWaterBill.ParentNode.InnerXml = "<water-bill-id>" + id + "</water-bill-id>" +
                                                                  "<invoice-number>" + collection["InvoiceNumber"] + "</invoice-number>" +
                                                                  "<account-number>" + collection["AccountNumber"] + "</account-number>" +
                                                                  "<meter-reading-start-date>" + collection["MeterReadingStartDate"] + "</meter-reading-start-date>" +
                                                                  "<meter-reading-end-date>" + collection["MeterReadingEndDate"] + "</meter-reading-end-date>" +
                                                                  "<billing-days>" + collection["BillingDays"] + "</billing-days>" +
                                                                  "<counter-reading-start>" + collection["CounterReadingStart"] + "</counter-reading-start>" +
                                                                  "<counter-reading-end>" + collection["CounterReadingEnd"] + "</counter-reading-end>" +
                                                                  "<total-hcf>" + collection["TotalHCF"] + "</total-hcf>" +
                                                                  "<total-gallons>" + collection["TotalGallons"] + "</total-gallons>" +
                                                                  "<first-15-hcf>" + collection["First15HCF"] + "</first-15-hcf>" +
                                                                  "<next-10-hcf>" + collection["Next10HCF"] + "</next-10-hcf>" +
                                                                  "<remaining-hcf>" + collection["RemainingHCF"] + "</remaining-hcf>" +
                                                                  "<sewer-charges>" + collection["SewerCharges"] + "</sewer-charges>" +
                                                                  "<storm-charges>" + collection["StormCharges"] + "</storm-charges>" +
                                                                  "<water-usage-charges>" + collection["WaterUsageCharges"] + "</water-usage-charges>" +
                                                                  "<total-charges>" + collection["TotalCharges"] + "</total-charges>" +
                                                                  "<local-taxes>" + collection["LocalTaxes"] + "</local-taxes>" +
                                                                  "<state-taxes>" + collection["StateTaxes"] + "</state-taxes>" +
                                                                  "<payment-due-date>" + collection["PaymentDueDate"] + "</payment-due-date>" +
                                                                  "<amount-due>" + collection["AmountDue"] + "</amount-due>" +
                                                                 "<late-payment-due-date>" + collection["LatePaymentDueDate"] + "</late-payment-due-date>" +
                                                                 "<late-amount-due>" + collection["LateAmountDue"] + "</late-amount-due>";

                                xdWaterBills.Save(fsWaterBills);
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

        // GET: WaterBills/Delete/5
        public ActionResult Delete(int id)
        {
            XmlDocument xdWaterBills = new XmlDocument();
            string strAccountNumber = string.Empty, strMeterNumber = string.Empty;
            string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

            if (System.IO.File.Exists(strFileWaterBills))
            {
                using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterBills.Load(fsWaterBills);

                    XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("water-bill-id");

                    foreach (XmlNode xnWaterBill in xnlWaterBills)
                    {
                        if (xnWaterBill.InnerText == id.ToString())
                        {
                             ViewData["WaterBillID"]           = xnWaterBill.InnerText;
                             ViewData["InvoiceNumber"]         = xnWaterBill.NextSibling.InnerText;
                             ViewData["AccountNumber"]         = xnWaterBill.NextSibling.NextSibling.InnerText;
                             ViewData["MeterReadingStartDate"] = xnWaterBill.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["MeterReadingEndDate"]   = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["BillingDays"]           = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["CounterReadingStart"]   = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["CounterReadingEnd"]     = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalHCF"]              = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalGallons"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["First15HCF"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["Next10HCF"]             = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["RemainingHCF"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["SewerCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["StormCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["WaterUsageCharges"]     = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["TotalCharges"]          = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LocalTaxes"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["StateTaxes"]            = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentDueDate"]        = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["AmountDue"]             = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LatePaymentDueDate"]    = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["LateAmountDue"]         = xnWaterBill.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;

                            strAccountNumber = xnWaterBill.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: WaterBills/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                XmlDocument xdWaterBills = new XmlDocument();
                string strFileWaterBills = Server.MapPath("/WaterDistribution/WaterBills.xml");

                if (System.IO.File.Exists(strFileWaterBills))
                {
                    using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xdWaterBills.Load(fsWaterBills);
                    }

                    using (FileStream fsWaterBills = new FileStream(strFileWaterBills, FileMode.Truncate, FileAccess.Write, FileShare.Write))
                    {
                        XmlNodeList xnlWaterBills = xdWaterBills.GetElementsByTagName("invoice");

                        foreach (XmlNode xnWaterBill in xnlWaterBills)
                        {
                            if (xnWaterBill.FirstChild.InnerText == id.ToString())
                            {
                                xnWaterBill.ParentNode.RemoveChild(xnWaterBill);
                                xdWaterBills.Save(fsWaterBills);
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
    }
}
