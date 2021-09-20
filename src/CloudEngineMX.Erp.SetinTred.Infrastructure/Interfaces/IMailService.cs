namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces
{
    using System.Net.Mail;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        Task<bool> SendEmail(string[] to, string subject, AlternateView view);

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        Task<bool> SendEmail(string[] to, string[] cc, string subject, AlternateView view);

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="pathImg">The path img.</param>
        /// <param name="label">The label.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        AlternateView GetHtml(string path, string pathImg, string[] label, string[] data);
    }
}
