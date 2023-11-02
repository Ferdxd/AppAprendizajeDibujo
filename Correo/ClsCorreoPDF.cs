using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Font=iTextSharp.text.Font;
using Element = iTextSharp.text.Element;

namespace AppDibujoArtistico.Correo
{
    internal class ClsCorreoPDF: ClsCorreo
    {
        public void enviarPDF(string correodestinatario, string nombre_completo)
        {
            // Crear el documento PDF en memoria con orientación horizontal
            MemoryStream memoryStream = new MemoryStream();
            Document doc = new Document(PageSize.A4.Rotate()); // A4 horizontal
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();

            // Configurar el color del marco (azul marino)
            BaseColor borderColor = new BaseColor(0, 0, 128); // Azul marino (R, G, B)

            // Personalizar el formato del PDF (ejemplo: certificado)
            // Crear fuentes personalizadas
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font titleFont = new Font(baseFont, 24, Font.BOLD, BaseColor.BLACK);
            Font normalFont = new Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK);

            // Agregar marco alrededor de la página
            PdfContentByte contentByte = writer.DirectContent;
            float borderWidth = 20f; // Ancho del marco
            Rectangle pageSize = doc.PageSize;

            // Dibuja un marco con un color y ancho específicos
            contentByte.SetColorStroke(borderColor);
            contentByte.SetLineWidth(borderWidth);
            contentByte.Rectangle(pageSize.Left + borderWidth / 2, pageSize.Bottom + borderWidth / 2, pageSize.Width - borderWidth, pageSize.Height - borderWidth);
            contentByte.Stroke();

            // Agregar línea decorativa en la parte superior
            contentByte.SetLineWidth(2f); // Ancho de la línea
            contentByte.SetColorStroke(BaseColor.BLACK); // Color de la línea
            contentByte.MoveTo(pageSize.Left, pageSize.Top - 100); // Posición inicial (x, y)
            contentByte.LineTo(pageSize.Right, pageSize.Top - 100); // Posición final (x, y)
            contentByte.Stroke();

            // Agregar línea decorativa en la parte inferior con el texto "Firma"
            contentByte.SetLineWidth(2f); // Ancho de la línea
            contentByte.SetColorStroke(BaseColor.BLACK); // Color de la línea
            contentByte.MoveTo(pageSize.Left, pageSize.Bottom + 70); // Posición inicial (x, y)
            contentByte.LineTo(pageSize.Right, pageSize.Bottom + 70); // Posición final (x, y)
            contentByte.Stroke();
            // Agregar el texto "Firma" en la parte inferior
            ColumnText.ShowTextAligned(contentByte, Element.ALIGN_CENTER, new Phrase("Firma", normalFont), pageSize.Width / 2, pageSize.Bottom + 50, 0);
            ColumnText.ShowTextAligned(contentByte, Element.ALIGN_CENTER, new Phrase("App Dibujo Artistico Agua Blanca", normalFont), pageSize.Width / 2, pageSize.Bottom + 80, 0);
            // Agregar una línea en el medio
            contentByte.SetLineWidth(2f); // Ancho de la línea
            contentByte.MoveTo(pageSize.Left, pageSize.Top - 200); // Posición inicial (x, y)
            contentByte.LineTo(pageSize.Right, pageSize.Top - 200); // Posición final (x, y)
            contentByte.Stroke();
            // Agregar el nombre de la persona a la que se certifica
            ColumnText.ShowTextAligned(contentByte, Element.ALIGN_CENTER, new Phrase(nombre_completo, titleFont), pageSize.Width / 2, pageSize.Top - 220, 0);

            // Agregar título
            Paragraph titulo = new Paragraph("CERTIFICADO DE FINALIZACIÓN", titleFont);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            // Agregar texto del certificado
            Paragraph certificadoTexto = new Paragraph("Por la presente, certificamos que "+nombre_completo+", un talentoso estudiante y apasionado del arte, ha completado con éxito el curso de \"Dibujo Artístico\" a través de nuestra aplicación móvil dedicada al arte y la creatividad.\r\n\r\nEste logro refleja su dedicación al aprendizaje en el campo del arte", normalFont);
            certificadoTexto.Alignment = Element.ALIGN_JUSTIFIED;
            certificadoTexto.SpacingBefore = 20;
            doc.Add(certificadoTexto);

            // ... Agregar más contenido y detalles de diseño según tus necesidades ...

            doc.Close();


            // Configurar los datos del correo electrónico
            string fromEmail = emailOrigen();
            string toEmail = correodestinatario;
            string subject = "Documento PDF adjunto";
            string body = "Por favor, encuentre adjunto el documento PDF.";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, contraseña()),
                EnableSsl = true,
            };

            MailMessage mail = new MailMessage(fromEmail, toEmail, subject, body);
            mail.Attachments.Add(new Attachment(new MemoryStream(memoryStream.ToArray()), "documento.pdf"));

            // Enviar el correo electrónico
            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Correo enviado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}
