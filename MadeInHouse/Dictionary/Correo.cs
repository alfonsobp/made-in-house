using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MadeInHouse.Dictionary
{
    class Correo
    {
        public void EnviarCorreo(string subject , string to , string msg, string path )
        {

          

                string bodyMessage = msg;


                // Create a message and set up the recipients.

                MailMessage message = new MailMessage("madeinhouse.sw@gmail.com", to,
                                                      subject, bodyMessage);

                message.IsBodyHtml = true;


                if (path != null)
                {
                    // Create  the file attachment for this e-mail message.
                    Attachment data = new Attachment(path, MediaTypeNames.Application.Octet);

                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(path);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(path);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(path);

                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);
                }

                    //Send the message.

                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        // Add credentials if the SMTP server requires them.
                        Credentials = new NetworkCredential("madeinhouse.sw@gmail.com", "insignia"),
                        EnableSsl = true
                    };


                    try
                    {
                        client.Send(message);
                        MessageBox.Show("Mensaje enviado satisfactoriamente");
                    }

                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace.ToString());
                    }
                

            }

           

        }

    
}
