using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Web.Mvc;

namespace WaterDistributionCompany1.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult Index()
        {
            XmlDocument xdPayments = new XmlDocument();
            string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

            if (System.IO.File.Exists(strFilePayments))
            {
                using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdPayments.Load(fsPayments);
                }

                if (xdPayments.DocumentElement.ChildNodes.Count > 0)
                {
                     ViewData["Payments"] = xdPayments.DocumentElement.ChildNodes;
                }
                else
                {
                     ViewData["Payments"] = null;
                }
            }

            return View();
        }

        // GET: Payments/Details/5
        public ActionResult Details(int id)
        {
            XmlDocument xdPayments = new XmlDocument();
            string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

            if (System.IO.File.Exists(strFilePayments))
            {
                using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdPayments.Load(fsPayments);

                    XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment-id");

                    foreach (XmlNode xnPayment in xnlPayments)
                    {
                        if (xnPayment.InnerText == id.ToString())
                        {
                             ViewData["PaymentID"]     = xnPayment.InnerText;
                             ViewData["ReceiptNumber"] = xnPayment.NextSibling.InnerText;
                             ViewData["WaterBillID"]   = xnPayment.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentDate"]   = xnPayment.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentAmount"] = xnPayment.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            int payment_id = 0;
            XmlDocument xdPayments = new XmlDocument();
            string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

            if (System.IO.File.Exists(strFilePayments))
            {
                using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdPayments.Load(fsPayments);

                    XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment-id");

                    foreach (XmlNode xnPayment in xnlPayments)
                    {
                        payment_id = int.Parse(xnPayment.FirstChild.InnerText);
                    }
                }
            }

            ViewData["PaymentID"] = (payment_id + 1);

            Random rndNumber = new Random();
            ViewData["ReceiptNumber"] = rndNumber.Next(100001, 999999).ToString();

            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                // int pmtId = 0;
                XmlDocument xdPayments = new XmlDocument();
                string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

                if (System.IO.File.Exists(strFilePayments))
                {
                    using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xdPayments.Load(fsPayments);

                        /* XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment-id");

                        foreach (XmlNode xnPayment in xnlPayments)
                        {
                            pmtId = int.Parse(xnPayment.InnerText);
                        } */
                    }
                }
                else
                {
                    xdPayments.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                       "<payments></payments>");
                    // pmtId = 0;
                }

                // pmtId++;

                XmlElement xePayment = xdPayments.CreateElement("payment");

                xePayment.InnerXml = "<payment-id>"     + collection["PaymentID"]     + "</payment-id>"     +
                                     "<receipt-number>" + collection["ReceiptNumber"] + "</receipt-number>" +
                                     "<water-bill-id>"  + collection["WaterBillID"]   + "</water-bill-id>"  +
                                     "<payment-date>"   + collection["PaymentDate"]   + "</payment-date>"   +
                                     "<payment-amount>" + collection["PaymentAmount"] + "</payment-amount>";

                xdPayments.DocumentElement.AppendChild(xePayment);

                using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    xdPayments.Save(fsPayments);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int id)
        {
            XmlDocument xdPayments = new XmlDocument();
            string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

            if (System.IO.File.Exists(strFilePayments))
            {
                using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdPayments.Load(fsPayments);

                    XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment-id");

                    foreach (XmlNode xnPayment in xnlPayments)
                    {
                        if (xnPayment.InnerText == id.ToString())
                        {
                             ViewData["PaymentID"]     = xnPayment.InnerText;
                             ViewData["ReceiptNumber"] = xnPayment.NextSibling.InnerText;
                             ViewData["WaterBillID"]   = xnPayment.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentDate"]   = xnPayment.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentAmount"] = xnPayment.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: Payments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                XmlDocument xdPayments = new XmlDocument();
                string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

                if (System.IO.File.Exists(strFilePayments))
                {
                    using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        xdPayments.Load(fsPayments);
                    }

                    XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment-id");

                    using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        foreach (XmlNode xnPayment in xnlPayments)
                        {
                            if (xnPayment.InnerText == id.ToString())
                            {
                                xnPayment.ParentNode.InnerXml = "<payment-id>"     + id                          + "</payment-id>"     +
                                                                "<receipt-number>" + collection["ReceiptNumber"] + "</receipt-number>" +
                                                                "<water-bill-id>"  + collection["WaterBillID"]   + "</water-bill-id>"  +
                                                                "<payment-date>"   + collection["PaymentDate"]   + "</payment-date>"   +
                                                                "<payment-amount>" + collection["PaymentAmount"] + "</payment-amount>";
                                xdPayments.Save(fsPayments);
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

        // GET: Payments/Delete/5
        public ActionResult Delete(int id)
        {
            XmlDocument xdPayments = new XmlDocument();
            string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

            if (System.IO.File.Exists(strFilePayments))
            {
                using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    xdPayments.Load(fsPayments);

                    XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment-id");

                    foreach (XmlNode xnPayment in xnlPayments)
                    {
                        if (xnPayment.InnerText == id.ToString())
                        {
                             ViewData["PaymentID"]     = xnPayment.InnerText;
                             ViewData["ReceiptNumber"] = xnPayment.NextSibling.InnerText;
                             ViewData["WaterBillID"]   = xnPayment.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentDate"]   = xnPayment.NextSibling.NextSibling.NextSibling.InnerText;
                             ViewData["PaymentAmount"] = xnPayment.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                        }
                    }
                }
            }

            return View();
        }

        // POST: Payments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                XmlDocument xdPayments = new XmlDocument();
                string strFilePayments = Server.MapPath("/WaterDistribution/Payments.xml");

                if (System.IO.File.Exists(strFilePayments))
                {
                    using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xdPayments.Load(fsPayments);
                    }

                    using (FileStream fsPayments = new FileStream(strFilePayments, FileMode.Truncate, FileAccess.Write, FileShare.Write))
                    {
                        XmlNodeList xnlPayments = xdPayments.GetElementsByTagName("payment");

                        foreach (XmlNode xnPayment in xnlPayments)
                        {

                            if (xnPayment.FirstChild.InnerText == id.ToString())
                            {
                                xnPayment.ParentNode.RemoveChild(xnPayment);
                                xdPayments.Save(fsPayments);
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
