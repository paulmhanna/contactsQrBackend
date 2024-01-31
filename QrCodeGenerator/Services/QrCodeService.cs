using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using QrCodeGenerator.Models;
using QRCoder;

namespace QrCodeGenerator.Services;

public class QrCodeService : IQrCodeService
{
    public byte[] GenerateQrCode(Contact contactCard)
    {
        // Build vCard string
        StringBuilder vCardBuilder = new StringBuilder();
        vCardBuilder.AppendLine("BEGIN:VCARD");
        vCardBuilder.AppendLine("VERSION:4.0");
        vCardBuilder.AppendLine($"FN:{contactCard.FirstName} {contactCard.LastName}");
        vCardBuilder.AppendLine($"N:{contactCard.LastName};{contactCard.FirstName};;;");
        foreach (string email in contactCard.Emails)
        {
            vCardBuilder.AppendLine($"EMAIL;TYPE=INTERNET:{email}");
        }

        foreach (var phoneNumber in contactCard.PhoneNumbers)
        {
            vCardBuilder.AppendLine($"TEL;TYPE={phoneNumber.Type}:{phoneNumber.Number}");
        }
        foreach (string socialUrl in contactCard.SocialMediaLinks)
        {
            // Use URL as the property name for generic social links
            vCardBuilder.AppendLine($"URL:{socialUrl}");
        }
        vCardBuilder.AppendLine("END:VCARD");
        string vCardString = vCardBuilder.ToString();

        // Generate QR code using QRCoder
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(vCardString, QRCodeGenerator.ECCLevel.Q);

        // Create and draw QR code image
        int matrixWidth = qrCodeData.ModuleMatrix[0].Count;
        int matrixHeight = qrCodeData.ModuleMatrix.Count;
        using (Bitmap qrCodeImage = new Bitmap(matrixWidth, matrixHeight))
        {
            for (var y = 0; y < matrixHeight; y++)
            {
                for (var x = 0; x < matrixWidth; x++)
                {
                    qrCodeImage.SetPixel(x, y, qrCodeData.ModuleMatrix[y][x] ? Color.Black : Color.White);
                }
            }

            // Convert image to byte array
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}