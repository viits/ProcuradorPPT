using DocumentFormat.OpenXml.Packaging;
using SearchPowerPoint.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPowerPoint.Infraestructure.Data.Service
{
    public class PowerPointService : IPowerPointService
    {

        public string RetornaTextoPPT()
        {
            StringBuilder sb = new StringBuilder();
            var pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "Ppt");
            if (Directory.Exists(pathFolder))
            {
                string[] pptFiles = Directory.GetFiles(pathFolder, "*.pptx");
                foreach (string filePath in pptFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    var path = Path.Combine(pathFolder, fileName);
                    using (PresentationDocument presentation = PresentationDocument.Open(path, false))
                    {
                        if (presentation != null)
                        {
                            var slides = presentation.PresentationPart.SlideParts;
                            foreach (var slide in slides)
                            {
                                var texts = slide.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>();
                                foreach (var text in texts)
                                {
                                    sb.AppendLine(text.Text);
                                }

                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }

        public string RetornaArquivo(string mensagem)
        {
            var pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "Ppt");
            if (Directory.Exists(pathFolder))
            {
                string[] pptFiles = Directory.GetFiles(pathFolder, "*.pptx");

                foreach (string filePath in pptFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    var path = Path.Combine(pathFolder, fileName);
                    using (PresentationDocument presentation = PresentationDocument.Open(path, false))
                    {
                        if (presentation != null)
                        {
                            var slides = presentation.PresentationPart.SlideParts;
                            foreach (var slide in slides)
                            {
                                var texts = slide.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>();
                                foreach (var text in texts)
                                {
                                    if (text.Text.ToLower().Contains(mensagem.ToLower()))
                                    {
                                        return path;
                                    }
                                }

                            }
                        }
                    }
                }
            }

            return "";
        }
    }
}
