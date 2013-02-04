using System;
using System.Collections;
using System.IO;
using Microsoft.Office.Word.Server.Conversions;
using Microsoft.SharePoint;

namespace DocumentConvertation.Layouts.DocumentConvertation
{
    class ConvertDocumentsclass
    {
        public string SourceLibrary;
        public string DestinationLibrary;
        public string Extension;
        ConversionJobStatus _status;
        public string[] Files;
        public enum DocSaveFormat
        {
            Document, Document97, DocumentMacroEnabled, Mhtml, Pdf, Rtf, Template, Template97, TemplateMacroEnabled, Xml, Xps
        };

        public enum DocSaveBehaviour
        {
            AlwaysOverwrite, AppendIfPossible, AppendOnly, NeverOverwrite
        };

        public Guid ConvertDocuments(SPSite spSite, SPWeb spWeb, ArrayList files, DocSaveFormat docSaveFormat, DocSaveBehaviour docSaveBehaviour)
        {
            SaveFormat savFormat = GetSaveFormat(docSaveFormat);
            SaveBehavior savBehaviour = getSaveBehavior(docSaveBehaviour);
            const string wordAutomationServiceName = "Word Automation Services";
            var job = new ConversionJob(wordAutomationServiceName) {UserToken = spSite.UserToken};
            job.Settings.UpdateFields = true;
            job.Settings.OutputFormat = savFormat;
            job.Settings.OutputSaveBehavior = savBehaviour;
            job.Settings.AddThumbnail = true;
            foreach (string file in files)
            {
                job.AddFile(spWeb.Url + "/" + SourceLibrary + "/" + file, spWeb.Url + "/" + DestinationLibrary + "/" + Path.GetFileNameWithoutExtension(file) + Extension);
            }
            job.Start();
            _status = new ConversionJobStatus(wordAutomationServiceName, job.JobId, null);
            return job.JobId;
        }
        public string GetResult(Guid jobId)
        {
            _status = new ConversionJobStatus("Word Automation Services", jobId, null);
            string result;
            if (_status.Count == _status.Succeeded + _status.Failed)
            {
                result = "Completed, Successful: " + _status.Succeeded + ", Failed: " + _status.Failed;
            }
            else
            {
                result = "In progress, Successful: " + _status.Succeeded + ", Failed: " + _status.Failed;
            }
            return result;
        }

        private SaveBehavior getSaveBehavior(DocSaveBehaviour docSaveBehaviour)
        {
            var savBehavior = SaveBehavior.NeverOverwrite;
            switch (docSaveBehaviour)
            {
                case DocSaveBehaviour.AlwaysOverwrite:
                    savBehavior = SaveBehavior.AlwaysOverwrite;
                    break;
                case DocSaveBehaviour.AppendIfPossible:
                    savBehavior = SaveBehavior.AppendIfPossible;
                    break;
                case DocSaveBehaviour.AppendOnly:
                    savBehavior = SaveBehavior.AppendOnly;
                    break;
                case DocSaveBehaviour.NeverOverwrite:
                    savBehavior = SaveBehavior.NeverOverwrite;
                    break;
            }
            return savBehavior;
        }

        private SaveFormat GetSaveFormat(DocSaveFormat doc)
        {
            var savFormat = SaveFormat.Automatic;
            switch (doc)
            {
                case DocSaveFormat.Document:
                    savFormat = SaveFormat.Document;
                    Extension = ".docx";
                    break;
                case DocSaveFormat.Document97:
                    savFormat = SaveFormat.Document97;
                    Extension = ".doc";
                    break;
                case DocSaveFormat.DocumentMacroEnabled:
                    savFormat = SaveFormat.DocumentMacroEnabled;
                    Extension = ".docx";
                    break;
                case DocSaveFormat.Mhtml:
                    savFormat = SaveFormat.MHTML;
                    Extension = ".mht";
                    break;
                case DocSaveFormat.Pdf:
                    savFormat = SaveFormat.PDF;
                    Extension = ".pdf";
                    break;
                case DocSaveFormat.Rtf:
                    savFormat = SaveFormat.RTF;
                    Extension = ".rtf";
                    break;
                case DocSaveFormat.Template:
                    savFormat = SaveFormat.Template;
                    Extension = ".dotx";
                    break;
                case DocSaveFormat.Template97:
                    savFormat = SaveFormat.Template97;
                    Extension = ".dot";
                    break;
                case DocSaveFormat.TemplateMacroEnabled:
                    savFormat = SaveFormat.TemplateMacroEnabled;
                    Extension = ".dotm";
                    break;
                case DocSaveFormat.Xml:
                    savFormat = SaveFormat.XML;
                    Extension = ".xml";
                    break;
                case DocSaveFormat.Xps:
                    savFormat = SaveFormat.XPS;
                    Extension = ".xps";
                    break;
            }
            return savFormat;
        }
    }
}
