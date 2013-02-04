using System;
using System.Collections;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentConvertation.Layouts.DocumentConvertation;
using Microsoft.SharePoint;

namespace DocumentConvertation.DocumentConvertation
{
    public partial class DocumentConvertationUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var web = SPContext.Current.Web;
                ddlSourceLists.Items.Clear();
                ddlTargetLists.Items.Clear();
                foreach (SPList lib in web.Lists)
                {
                    if (lib.BaseTemplate == SPListTemplateType.DocumentLibrary || lib.BaseTemplate == SPListTemplateType.PictureLibrary)
                    {
                        string s = lib.Title + " : " + lib.BaseTemplate;
                        var item = new ListItem(s, lib.Title);
                        ddlSourceLists.Items.Add(item);
                        ddlTargetLists.Items.Add(item);
                    }
                }
                ddlTypes.Items.Clear();
                ddlOverWrite.Items.Clear();

                ddlTypes.DataSource = Enum.GetValues(typeof(ConvertDocumentsclass.DocSaveFormat));
                ddlTypes.DataBind();
               
                ddlOverWrite.DataSource = Enum.GetValues(typeof(ConvertDocumentsclass.DocSaveBehaviour));
                ddlOverWrite.DataBind();
            }
        }        

        protected void BtnConvertClick(object sender, EventArgs e)
        {
            var docs = new ConvertDocumentsclass();
            var filelist = new ArrayList();
            foreach (ListItem item in chkItems.Items)
            {
                if (item.Selected)
                    filelist.Add(item.Text);
            }
            docs.SourceLibrary = ddlSourceLists.SelectedValue;
            docs.DestinationLibrary = ddlTargetLists.SelectedValue;

            _jobId = docs.ConvertDocuments(SPContext.Current.Site, SPContext.Current.Web, filelist, (ConvertDocumentsclass.DocSaveFormat)Enum.Parse(typeof(ConvertDocumentsclass.DocSaveFormat), ddlTypes.SelectedValue), (ConvertDocumentsclass.DocSaveBehaviour)Enum.Parse(typeof(ConvertDocumentsclass.DocSaveBehaviour), ddlOverWrite.SelectedValue));
            ShowResult(_jobId);
        }

        static Guid _jobId;

        private void ShowResult(Guid jobId)
        {
            var docs = new ConvertDocumentsclass();
            

           // lblResult.Text = DateTime.Now.ToString() + " - " + docs.GetResult(jobId);

            lblResult.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture) + " - " + docs.GetResult(jobId);
        }

        protected void BtnCheckResultClick(object sender, EventArgs e)
        {
            ShowResult(_jobId);
        }

        protected void DdlSourceListsSelectedIndexChanged(object sender, EventArgs e)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList(ddlSourceLists.SelectedValue);
            chkItems.Items.Clear();
            if (list != null)
            {
                foreach (SPListItem item in list.Items)
                {
                   // chkItems.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                    chkItems.Items.Add(new ListItem(item.Name, item.ID.ToString(CultureInfo.InvariantCulture)));
                }
            }
        }
    }
}
