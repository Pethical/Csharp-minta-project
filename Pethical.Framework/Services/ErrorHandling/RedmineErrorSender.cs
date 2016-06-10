using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;
using Redmine.Net.Api.Types;
using System.IO;
using Redmine.Net.Api;
using System.Collections;
using System.Windows.Interop;
using Pethical.Framework.Utils;

namespace Pethical.Framework.Services.ErrorHandling
{
   /// <summary>
   /// Az osztály a hibák beküldését teszi lehetővé redmineba
   /// </summary>
   /// <remarks>
   /// A használatához redmine api szükséges
   /// </remarks>
   public class RedmineErrorSender : IErrorSender
   {
       //
       // TODO: A paraméterek most bele vannak égetve, később adatbázisból kellene venni
       //
       
       /// <summary>
       /// A hibából redmine issue-t (ticketet) készít majd beküldi
       /// </summary>
       /// <param name="e">A hibát leíró kivétel</param>
       public void SendException(Exception e)
       {
            var manager = new RedmineManager("https://condominium.hu/redmine", "16a31a68b68f84da3661bb3f79f5791007b09316");

            Upload upload;
            Upload upload2;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();            
            encoder.Frames.Add(BitmapFrame.Create(Screen.CaptureFullScreen(false)));
            using(MemoryStream mem = new MemoryStream())
            {
                encoder.Save(mem);
                upload = manager.UploadData(mem.GetBuffer());
            }
            encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Screen.CaptureRegion(((HwndSource)HwndSource.FromVisual(Application.Current.MainWindow)).Handle,
                (int)Application.Current.MainWindow.Left, (int)Application.Current.MainWindow.Top,
                (int)Application.Current.MainWindow.Width, (int)Application.Current.MainWindow.Height, false)));
            using (MemoryStream mem = new MemoryStream())
            {
                encoder.Save(mem);
                upload2 = manager.UploadData(mem.GetBuffer());
            }

            upload.ContentType = "image/jpeg";
            upload.FileName = "ScreenShot.jpg";
            upload.Description = "ScreenShot";

            upload2.ContentType = "image/jpeg";
            upload2.FileName = "MainWindow.jpg";
            upload2.Description = "Főablak";

            Issue issue      = new Issue();
            issue.Subject    = e.GetType().Name;
            issue.Project    = new IdentifiableName() { Id = 1 };
            issue.Priority   = new IdentifiableName() { Id = 4 };
            issue.Category   = new IdentifiableName() { Id = 2 };
            issue.Status     = new IdentifiableName() { Id = 1 };
            issue.AssignedTo = new IdentifiableName() { Id = 3 };            

            issue.Uploads    = new List<Upload>();
            issue.Uploads.Add(upload);
            issue.Uploads.Add(upload2);
            Exception ex = e;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("h2. Automatikus hibajelentés");
            while (ex != null)
            {
                builder.AppendLine();
                builder.AppendLine( String.Format("h2. {0}",ex.GetType().ToString()));
                builder.AppendLine();
                builder.AppendLine(String.Format("h3. {0}", ex.Message));
                builder.AppendLine();
                builder.AppendLine(String.Format("<pre>{0}</pre>", ex.StackTrace));
                builder.AppendLine();
                if (ex.HelpLink!="" && ex.HelpLink!=null)
                {
                    builder.AppendLine(String.Format("\"MSDN Help\": {0}", ex.HelpLink));
                }
                if ((ex.Data != null) && (ex.Data.Count > 0))
                {
                    builder.AppendLine(String.Format("|_Név|_Érték|"));
                    foreach(KeyValuePair<object, object> item in ex.Data)
                    {
                        builder.AppendLine( String.Format("|{0}|{1}|", item.Key.ToString(), item.Value.ToString()));
                    }
                }
                ex = ex.InnerException;
            }
            builder.AppendLine();
            builder.AppendLine("h3. Betöltött assembly infó");
            builder.AppendLine();
            builder.AppendLine(String.Format("|_.Assembly|_.Verzió|_.Runtime verzió|_.Filenév|"));
            foreach (var loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {                                
                builder.AppendLine(String.Format("|{0}|{1}|{2}|{3}|",
                    loadedAssembly.GetName().Name,
                    loadedAssembly.GetName().Version,
                    loadedAssembly.ImageRuntimeVersion,
                    loadedAssembly.IsDynamic?"Dinamikus":Path.GetFileName(loadedAssembly.Location)
                    // ,loadedAssembly.IsDynamic?"Dinamikus":loadedAssembly.Location
                    ));
            }
            builder.AppendLine();
            builder.AppendLine("h3. Környezet");
            builder.AppendLine();
            builder.AppendLine("|_.Név|_.Érték|");
            OperatingSystem os = System.Environment.OSVersion;
            builder.AppendLine(String.Format("|{0}|{1}|", "Felhasználó", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
            builder.AppendLine(String.Format("|{0}|{1}|", "Rendszer", os.VersionString));
            builder.AppendLine(String.Format("|{0}|{1}|", "Platform", os.Platform));
            var variables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry entry in variables)
            {
                builder.AppendLine(String.Format("|{0}|{1}|", entry.Key, entry.Value));
            }
            issue.Description = builder.ToString();
            manager.CreateObject(issue);
        }

       }
  }

