using Microsoft.AspNetCore.Authorization;
using System;
using System.Diagnostics;
using System.IO;

namespace SigortaApp.Web.Areas.Admin
{
    [AllowAnonymous]
    public class GeneratePDF
    {
        private string url { get; set; }

        public GeneratePDF(string _url)
        {
            url = _url;
        }

        public byte[] GetPdf()
        {
            var switches = $"-q {url} -";

            string rotativaPath = Path.Combine(Directory.GetCurrentDirectory(), "rotativa", "wkhtmltopdf.exe");

            using (var proc = new Process())
            {
                try
                {
                    proc.StartInfo = new ProcessStartInfo
                    {
                        FileName = rotativaPath,
                        Arguments = switches,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        CreateNoWindow = true,
                    };
                    proc.Start();
                }
                catch (Exception ex)
                {
                    throw;
                }

                using (var ms = new MemoryStream())
                {
                    proc.StandardOutput.BaseStream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}
