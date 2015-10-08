using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Minimo_Institucional.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public string EnviaEmailContato(string nome, string telefone, string email, string mensagem, string instituicao)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(mensagem))
            {
                return "Os campos email e mensagem são obrigatórios.";
            }

            try 
            {
                var agora = DateTime.Now;
                var arrMsg = new string[]{"EMAIL: " + email, "NOME: " + nome, "TELEFONE: " + telefone, "MENSAGEM: " + mensagem, "INSTITUICAO: " + instituicao, "DIA: " + agora.ToShortDateString(), "HORÁRIO: " + agora.ToShortTimeString() };
                var msg = String.Join(" <br /> ", arrMsg);
                var fromAddress = new MailAddress("horario@minimo.net.br");
                var toAddress = new MailAddress("claudio@minimo.net.br");
                var smtp = new SmtpClient { };
                smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "92279227")
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Contato através do site",
                    IsBodyHtml = true,
                    Body = msg,
                })
                {
                    message.CC.Add("fernando@minimo.net.br");
                    message.CC.Add("rodrigo@minimo.net.br");
                    message.CC.Add("guilherme@minimo.net.br");
                    //message.CC.Add("luis@minimo.net.br");
                    smtp.Send(message);
                }
 
            }
            catch
            {
                return "Mensagem não enviada!";
            }
            

            return String.Empty;
            //return RedirectToAction("Index", "Home");
        }
    }
}
