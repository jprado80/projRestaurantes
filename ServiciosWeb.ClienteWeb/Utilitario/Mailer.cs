namespace ServiciosWeb.ClienteWeb.Utilitario
{
    using SeServiciosWeb.ClienteWeb.Utilitario;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    public class Mailer
    {

        public Mailer()
        {
            this.Notificacion = new Diccionario();
        }

        public Diccionario Notificacion { get; set; }

        private void EnviarCorreo(List<string> correosPara, string correoDe, List<string> correosCopia, string asunto, string cuerpo, List<Attachment> adjuntoList)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(correoDe);
            mail.Body = cuerpo;
            mail.Subject = asunto;
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            adjuntoList.ForEach(x => mail.Attachments.Add(x));

            correosPara.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x))
                    mail.To.Add(x);
            });
            correosCopia.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x))
                    mail.CC.Add(x);
            });

            SmtpClient Cliente = new SmtpClient();

            Cliente.Host = WebConfigReader.Mailer.Host;
            if (WebConfigReader.Mailer.Port > 0)
                Cliente.Port = Convert.ToInt32(WebConfigReader.Mailer.Port);

            Cliente.EnableSsl = WebConfigReader.Mailer.EnabledSSL;
            Cliente.UseDefaultCredentials = WebConfigReader.Mailer.UseDefaultCredentials;
            if (WebConfigReader.Mailer.UseDefaultCredentials == false)
                Cliente.Credentials = new NetworkCredential(WebConfigReader.Mailer.CredentialsUser, WebConfigReader.Mailer.CredentialsClave);

            Cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            Cliente.Send(mail);

            //log.Debug(string.Format("Correo enviado a... {0}: {1}", mail.To, mail.Subject));
        }

        public void Enviar()
        {
            string asunto = "";
            string cuerpo = "";
            try
            {
                asunto = Notificacion.Asunto;
                cuerpo = Notificacion.Cuerpo;

                foreach (var item in Notificacion.AsuntoValores)
                    asunto = asunto.Replace(item.Key, item.Value);

                foreach (var item in Notificacion.CuerpoValores)
                    cuerpo = cuerpo.Replace(item.Key, item.Value);

                this.Notificacion.CorreoDe = WebConfigReader.Mailer.From;

                List<string> conCopiaList = Notificacion.ConCopia.Split(';').ToList();
                this.Notificacion.CorreosPara.AddRange(conCopiaList);
                this.Notificacion.CorreosPara.ForEach(x =>
                {
                    if (x != "")
                    {
                        EnviarCorreo(new List<string> { x }, this.Notificacion.CorreoDe, this.Notificacion.CorreosCopia, asunto, cuerpo, this.Notificacion.Adjuntos);
                    }
                });
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    public class Diccionario
    {
        public Diccionario()
        {
            this.AsuntoValores = new Dictionary<string, string>();
            this.CuerpoValores = new Dictionary<string, string>();
            this.CorreosPara = new List<string>();
            this.CorreosCopia = new List<string>();
            this.Adjuntos = new List<Attachment>();
        }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string CorreoDe { get; set; }
        public string ConCopia { get; set; }
        public Dictionary<string, string> AsuntoValores { get; set; }
        public Dictionary<string, string> CuerpoValores { get; set; }
        public List<string> CorreosPara { get; set; }
        public List<string> CorreosCopia { get; set; }
        public List<Attachment> Adjuntos { get; set; }
        public string ResolveBody()
        {
            var cuerpo = this.Cuerpo;
            foreach (var item in CuerpoValores)
                cuerpo = cuerpo.Replace(item.Key, item.Value);
            return cuerpo;
        }

    }

    public class FormatoCorreo
    {
        

        public string BodyMensaje(string pNombreUsuario,string pDetalle, string link)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' dir='ltr' style='font-size:16px;background-color:rgb(255,255,255)'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("        <td align='center' valign='top' style='margin:0;padding:40'>");
            sb.Append("            <table align='center' border='0' cellspacing='0' cellpadding='0' width='700' bgcolor='#1ab394' style='width:700px;border:1px solid ");
            sb.Append("         transparent; ");
            sb.Append("order-radius:13px;margin:auto;background-color:#1872a6'>");
            sb.Append("                <tbody>");
            sb.Append("					<tr>");
            sb.Append("					<td>");
            sb.Append("						<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("						<tbody>");
            sb.Append("							<tr>'");
            sb.Append("							<td valign='top' align='left' style='padding:0px;margin:0px'>");
            sb.Append("								<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("								<tbody>");
            sb.Append("									<tr>");
            sb.Append("									<td align='left' valign='top'>");
            sb.Append("									<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'>");
            sb.Append("										<tbody>");
            sb.Append("											<tr>");
            sb.Append("											<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:20px;border-radius:6px");
            sb.Append("	                                        color:rgb(' sb.Append('55,255,255)'>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'> SISTEMA DE RESTAURANTE");
            sb.Append("                                             </span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<span style='color:rgb(38,38,38)'></span>");
            sb.Append("											</td>");
            sb.Append("											</tr>");
            sb.Append("										</tbody>");
            sb.Append("									</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										<table width='100%' border='0' cellpadding='10' cellspacing='10' align='center'  bgcolor='white'>");
            sb.Append("										<tbody>");
            sb.Append("										       <tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)");
            sb.Append("                                             font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'> ");

            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("											<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                         font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'>");
            sb.Append("													Bienvenido a la web site,"+ pNombreUsuario + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("												<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                             font-size:14px;font-weight:bold;background-color:rgb(255,255,255)'  colspan='4'>");
            sb.Append("													<span style='font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)'>  "+ pDetalle + " <a href='"+ link + "'> "+ link + "</a>");
            sb.Append("</span>");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("									");
            sb.Append("										</tbody>");
            sb.Append("										");
            sb.Append("										</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										");
            sb.Append("						</tbody>");
            sb.Append("						</table>");
            sb.Append("					</td>");
            sb.Append("					</tr>");
            sb.Append("				</tbody>");
            sb.Append("			</table>");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }

        public string BodyMensajeCambioClave(string pNombreUsuario, string pDetalle)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' dir='ltr' style='font-size:16px;background-color:rgb(255,255,255)'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("        <td align='center' valign='top' style='margin:0;padding:40'>");
            sb.Append("            <table align='center' border='0' cellspacing='0' cellpadding='0' width='700' bgcolor='#1ab394' style='width:700px;border:1px solid ");
            sb.Append("         transparent; ");
            sb.Append("order-radius:13px;margin:auto;background-color:#1872a6'>");
            sb.Append("                <tbody>");
            sb.Append("					<tr>");
            sb.Append("					<td>");
            sb.Append("						<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("						<tbody>");
            sb.Append("							<tr>'");
            sb.Append("							<td valign='top' align='left' style='padding:0px;margin:0px'>");
            sb.Append("								<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            sb.Append("								<tbody>");
            sb.Append("									<tr>");
            sb.Append("									<td align='left' valign='top'>");
            sb.Append("									<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center'>");
            sb.Append("										<tbody>");
            sb.Append("											<tr>");
            sb.Append("											<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:20px;border-radius:6px");
            sb.Append("	                                        color:rgb(' sb.Append('55,255,255)'>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'> SISTEMA DE RESTAURANTE");
            sb.Append("                                             </span></div>");
            sb.Append("												<div style='text-align:center'><span style='color:rgb(255,255,255);font-weight:bold'><br></span></div>");
            sb.Append("												<span style='color:rgb(38,38,38)'></span>");
            sb.Append("											</td>");
            sb.Append("											</tr>");
            sb.Append("										</tbody>");
            sb.Append("									</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										<table width='100%' border='0' cellpadding='10' cellspacing='10' align='center'  bgcolor='white'>");
            sb.Append("										<tbody>");
            sb.Append("										       <tr>");
            sb.Append("												<td align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)");
            sb.Append("                                             font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'> ");

            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("											<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                         font-size:12px;background-color:rgb(255,255,255);width:190px '  colspan='4'>");
            sb.Append("													Bienvenido a la web site," + pNombreUsuario + "");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("												<tr>");
            sb.Append("												<td colspan='2' align='left' valign='top' style='padding:10px;font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38);");
            sb.Append("                                             font-size:14px;font-weight:bold;background-color:rgb(255,255,255)'  colspan='4'>");
            sb.Append("													<span style='font-family:Arial,Helvetica,sans-serif;color:rgb(38,38,38)'>  " + pDetalle);
            sb.Append("</span>");
            sb.Append("												</td>");
            sb.Append("											</tr>");
            sb.Append("									");
            sb.Append("										</tbody>");
            sb.Append("										");
            sb.Append("										</table>");
            sb.Append("									</td>");
            sb.Append("									</tr>");
            sb.Append("									<tr>");
            sb.Append("									<td>");
            sb.Append("										");
            sb.Append("						</tbody>");
            sb.Append("						</table>");
            sb.Append("					</td>");
            sb.Append("					</tr>");
            sb.Append("				</tbody>");
            sb.Append("			</table>");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");


            return sb.ToString();
        }


    }
}
