using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Web.Mvc;

namespace WaterDistributionCompany1.Controllers
{
    public class WaterMetersController : Controller
    {
        // GET: WaterMeters
        public ActionResult Index()
        {
            XmlDocument xdWaterMeters = new XmlDocument();
            string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

            if (System.IO.File.Exists(strFileWaterMeters))
            {
                using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterMeters.Load(fsWaterMeters);
                }

                if (xdWaterMeters.DocumentElement.ChildNodes.Count > 0)
                {
                    ViewData["WaterMeters"] = xdWaterMeters.DocumentElement.ChildNodes;
                }
                else
                {
                    ViewData["WaterMeters"] = null;
                }
            }

            return View();
        }

        // GET: WaterMeters/Details/5
        public ActionResult Details(int id)
        {
            XmlDocument xdWaterMeters = new XmlDocument();
            string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

            if (System.IO.File.Exists(strFileWaterMeters))
            {
                using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterMeters.Load(fsWaterMeters);

                    XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("meter-id");

                    foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                    {
                        if (xnWaterMeter.InnerText == id.ToString())
                        {
                            ViewData["WaterMeterID"] = xnWaterMeter.InnerText;
                            ViewData["MeterNumber"] = xnWaterMeter.NextSibling.InnerText;
                            ViewData["Make"] = xnWaterMeter.NextSibling.NextSibling.InnerText;
                            ViewData["Model"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["MeterSize"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["DateLastUpdate"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["CounterValue"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // GET: WaterMeters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WaterMeters/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                int meter_id = -1;
                XmlDocument xdWaterMeters = new XmlDocument();
                string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

                // Make sure a meter number was provided. If not, don't do nothing
                if (!string.IsNullOrEmpty(collection["MeterNumber"]))
                {
                    // If an XML file for water meters was created already, ...
                    if (System.IO.File.Exists(strFileWaterMeters))
                    {
                        // ... open it ...
                        using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            // ... and put the records in the DOM
                            xdWaterMeters.Load(fsWaterMeters);

                            // We need the meters numbers. Therefore, use XPath to specify their path
                            XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("meter-id");

                            // Check the whole list of meters numbers
                            foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                            {
                                // Every time, assign the current meter number to our meterNumber variable
                                meter_id = int.Parse(xnWaterMeter.InnerText);
                            }
                        }
                    }
                    else
                    {
                        // If there is no XML file yet, create skeleton code for an XML document, ...
                        xdWaterMeters.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                              "<water-meters></water-meters>");
                        // and set our meterNumber variable to 0
                        meter_id = 0;
                    }
                }

                // Get ready to create an XML element named water-meter
                XmlElement xeWaterMeter = xdWaterMeters.CreateElement("water-meter");

                // Increase the meterNumber variable by 1
                meter_id += 1;

                // Create the markup of the XML water meter
                string strWaterMeter = "<meter-id>" + meter_id + "</meter-id>" +
                                       "<meter-number>" + collection["MeterNumber"] + "</meter-number>" +
                                       "<make>" + collection["Make"] + "</make>" +
                                       "<model>" + collection["Model"] + "</model>" +
                                       "<meter-size>" + collection["MeterSize"] + "</meter-size>" +
                                       "<date-last-update>" + collection["DateLastUpdate"] + "</date-last-update>" +
                                       "<counter-value>" + collection["CounterValue"] + "</counter-value>";

                // Specify the markup of the new element
                xeWaterMeter.InnerXml = strWaterMeter;
                // Add the new node to the root
                xdWaterMeters.DocumentElement.AppendChild(xeWaterMeter);

                // Save the (new version of the) XML file
                using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    xdWaterMeters.Save(fsWaterMeters);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WaterMeters/Edit/5
        public ActionResult Edit(int id)
        {
            XmlDocument xdWaterMeters = new XmlDocument();
            string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

            if (System.IO.File.Exists(strFileWaterMeters))
            {
                using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterMeters.Load(fsWaterMeters);

                    XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("meter-id");

                    foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                    {
                        if (xnWaterMeter.InnerText == id.ToString())
                        {
                            ViewData["WaterMeterID"] = xnWaterMeter.InnerText;
                            ViewData["MeterNumber"] = xnWaterMeter.NextSibling.InnerText;
                            ViewData["Make"] = xnWaterMeter.NextSibling.NextSibling.InnerText;
                            ViewData["Model"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["MeterSize"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["DateLastUpdate"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["CounterValue"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: WaterMeters/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
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
                            if (xnWaterMeter.InnerText == id.ToString())
                            {
                                xnWaterMeter.ParentNode.InnerXml = "<meter-id>" + id + "</meter-id>" +
                                                                   "<meter-number>" + collection["MeterNumber"] + "</meter-number>" +
                                                                   "<make>" + collection["Make"] + "</make>" +
                                                                   "<model>" + collection["Model"] + "</model>" +
                                                                   "<meter-size>" + collection["MeterSize"] + "</meter-size>" +
                                                                   "<date-last-update>" + collection["DateLastUpdate"] + "</date-last-update>" +
                                                                   "<counter-value>" + collection["CounterValue"] + "</counter-value>";
                                xdWaterMeters.Save(fsWaterMeters);
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

        // GET: WaterMeters/Delete/5
        public ActionResult Delete(int id)
        {
            XmlDocument xdWaterMeters = new XmlDocument();
            string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

            if (System.IO.File.Exists(strFileWaterMeters))
            {
                using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdWaterMeters.Load(fsWaterMeters);

                    XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("meter-id");

                    foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                    {
                        if (xnWaterMeter.InnerText == id.ToString())
                        {
                            ViewData["WaterMeterID"] = xnWaterMeter.InnerText;
                            ViewData["MeterNumber"] = xnWaterMeter.NextSibling.InnerText;
                            ViewData["Make"] = xnWaterMeter.NextSibling.NextSibling.InnerText;
                            ViewData["Model"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["MeterSize"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["DateLastUpdate"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                            ViewData["CounterValue"] = xnWaterMeter.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: WaterMeters/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                XmlDocument xdWaterMeters = new XmlDocument();
                string strFileWaterMeters = Server.MapPath("/WaterDistribution/WaterMeters.xml");

                /* Make sure an XML file for the water meters was previously created 
                 * (normally, this is not necessary; if there is no such file, this code 
                 * will never run because it is completely based on routing) */
                if (System.IO.File.Exists(strFileWaterMeters))
                {
                    using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xdWaterMeters.Load(fsWaterMeters);
                    }

                    // Get ready to change something on the file
                    using (FileStream fsWaterMeters = new FileStream(strFileWaterMeters, FileMode.Truncate, FileAccess.Write, FileShare.Write))
                    {
                        // Get a collection of water meter nodes
                        XmlNodeList xnlWaterMeters = xdWaterMeters.GetElementsByTagName("water-meter");

                        // Check each node
                        foreach (XmlNode xnWaterMeter in xnlWaterMeters)
                        {
                            /* If you find a water-meter record whose meter-id is 
                             * the same as the id of the record the user clicked, ... */
                            if (xnWaterMeter.FirstChild.InnerText == id.ToString())
                            {
                                // ... ask its parent to delete that record
                                xnWaterMeter.ParentNode.RemoveChild(xnWaterMeter);
                                // Now that the record has been deleted, save the XML file
                                xdWaterMeters.Save(fsWaterMeters);
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
