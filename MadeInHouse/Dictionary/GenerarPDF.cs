﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Diagnostics;


namespace MadeInHouse.Dictionary
{
    class GenerarPDF
    {
        public void createPDF(string html,string Path,bool mostrar)
        {



            Document document = new Document();
            PdfWriter.GetInstance(document, 
                new FileStream(Environment.CurrentDirectory+Path, FileMode.Create));
            
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw =
                         new iTextSharp.text.html.simpleparser.HTMLWorker(document);
            hw.Parse(new StringReader(html));
            
            document.Close();

            if (mostrar)
            {
                Process.Start(Environment.CurrentDirectory + Path);
            }

        }

        public void Borrar(string path){
        
                   System.IO.FileInfo fi = new System.IO.FileInfo(path);
            try
            {
                fi.Delete();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        
        }
    }
}
