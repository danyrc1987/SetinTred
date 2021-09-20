namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces.IMailService" />
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        public async Task<bool> SendEmail(string[] to, string subject, AlternateView view)
        {
            var messages = new MailMessage();
            var clientSmtp = new SmtpClient();
            try
            {
                if (to.Length <= 0) return false;
                if (Convert.ToBoolean(_configuration["Mail:IsDebugMode"]) == true)
                {
                    subject += "Correo Real : ";
                    subject = to.Aggregate(subject, (current, t) => current + (t + " ; "));

                    to = this._configuration["Mail:EmailTest"].Split(';');
                }

                foreach (var t in to.Where(t => t.ToString() != ""))
                {
                    messages.To.Add(t);
                }

                var from = this._configuration["Mail:From"];

                messages.Subject = subject;
                messages.AlternateViews.Add(view);
                messages.IsBodyHtml = true;
                messages.From = new MailAddress(from);

                using (clientSmtp)
                {
                    var credential = new NetworkCredential
                    {
                        UserName = this._configuration["Mail:User"],
                        Password = this._configuration["Mail:Password"],
                    };

                    clientSmtp.UseDefaultCredentials = false;
                    clientSmtp.Port = int.Parse(this._configuration["Mail:Port"]);
                    clientSmtp.Credentials = credential;
                    clientSmtp.Host = this._configuration["Mail:Smtp"];
                    clientSmtp.EnableSsl = (this._configuration["Mail:Ssl"].ToLower() == "true") ? true : false;

                    clientSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;



                    await clientSmtp.SendMailAsync(messages);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        public async Task<bool> SendEmail(string[] to, string[] cc, string subject, AlternateView view)
        {
            var messages = new MailMessage();
            var clientSmtp = new SmtpClient();
            try
            {
                if (to.Length <= 0) return false;
                if (Convert.ToBoolean(_configuration["Mail:IsDebugMode"]) == true)
                {
                    subject += "Correo Real : ";
                    subject = to.Aggregate(subject, (current, t) => current + (t + " ; "));

                    to = this._configuration["Mail:EmailTest"].Split(';');
                }

                foreach (var t in to.Where(t => t.ToString() != ""))
                {
                    messages.To.Add(t);
                }

                if (Convert.ToBoolean(_configuration["Mail:IsDebugMode"]) == true)
                {
                    cc = this._configuration["Mail:EmailTest"].Split(';');
                }

                foreach (var t in cc.Where(t => t.ToString() != ""))
                {
                    messages.CC.Add(t);
                }

                var from = this._configuration["Mail:From"];

                messages.Subject = subject;
                messages.AlternateViews.Add(view);
                messages.IsBodyHtml = true;
                messages.From = new MailAddress(from);

                using (clientSmtp)
                {
                    var credential = new NetworkCredential
                    {
                        UserName = this._configuration["Mail:User"],
                        Password = this._configuration["Mail:Password"],
                    };

                    clientSmtp.UseDefaultCredentials = false;
                    clientSmtp.Port = int.Parse(this._configuration["Mail:Port"]);
                    clientSmtp.Credentials = credential;
                    clientSmtp.Host = this._configuration["Mail:Smtp"];
                    clientSmtp.EnableSsl = (this._configuration["Mail:Ssl"].ToLower() == "true") ? true : false;

                    clientSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;



                    await clientSmtp.SendMailAsync(messages);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="pathImg">The path img.</param>
        /// <param name="label">The label.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error en el AlternateView: {ex.Message}, {path}</exception>
        public AlternateView GetHtml(string path, string pathImg, string[] label, string[] data)
        {
            try
            {
                if (label.Length != data.Length)
                    new Exception("Error en el número de datos");

                var stringHtml = new StringBuilder(File.ReadAllText(path));

                for (var counter = 0; counter < label.Length; counter++)
                    stringHtml.Replace(label[counter], data[counter]);

                var html = AlternateView.CreateAlternateViewFromString(stringHtml.ToString(), Encoding.UTF8, MediaTypeNames.Text.Html);

                const string mediaType = MediaTypeNames.Image.Jpeg;

                var pathLogo = $"{pathImg}/global_assets/images/Logo-Setin-Tred.jpg";
                var pathPrincipal = $"{pathImg}/global_assets/images/correo.jpg";

                var img = new LinkedResource(pathLogo, mediaType)
                {
                    ContentId = "companylogo",
                    ContentType = { MediaType = mediaType },
                    TransferEncoding = TransferEncoding.Base64
                };
                img.ContentType.Name = img.ContentId;
                img.ContentLink = new Uri("cid:" + img.ContentId);

                var imgHero = new LinkedResource(pathPrincipal, mediaType)
                {
                    ContentId = "heroe",
                    ContentType = { MediaType = mediaType },
                    TransferEncoding = TransferEncoding.Base64
                };
                imgHero.ContentType.Name = imgHero.ContentId;
                imgHero.ContentLink = new Uri("cid:" + imgHero.ContentId);

                html.LinkedResources.Add(imgHero);
                html.LinkedResources.Add(img);

                return html;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el AlternateView: {ex.Message}, {path} ");
            }

        }
    }
}
