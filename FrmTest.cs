using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace BIDS2008R2Downgrade
{
  public partial class FrmTest : Form
  {
    public FrmTest()
    {
      InitializeComponent();
      cboErrorLevel.SelectedIndex = 2;
      dlgOpen.InitialDirectory = this.BasePath;
    }

    private void cmdBrowseReport_Click(object sender, EventArgs e)
    {
      try
      {
        if (dlgOpen.ShowDialog() == DialogResult.OK)
        {
          txtReportPath.Text = dlgOpen.FileName;
          cmdLoad_Click(cmdLoad, EventArgs.Empty);
        }
      }
      catch (Exception pException)
      {
        MessageBox.Show(pException.Message);
      }
    }

    private void cmdLoad_Click(object sender, EventArgs e)
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        XmlReader pReader = XmlReader.Create(txtReportPath.Text, new XmlReaderSettings());
        int num = (int)pReader.MoveToContent();
        string sNamespaceURI = pReader.NamespaceURI;
        pReader.Close();
        txtOutput.Text = sNamespaceURI + Environment.NewLine;

        switch (sNamespaceURI)
        {
          case "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition":
            txtOutput.AppendText("Report is in R1");
            break;

          case "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition":
            txtOutput.AppendText("Report is in R2");
            break;
        }
      }
      catch (Exception pException)
      {
        MessageBox.Show(pException.Message);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void cmdDowngrade_Click(object sender, EventArgs e)
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        txtDowngradeStatus.Clear();

        string sReportXml = File.ReadAllText(txtReportPath.Text, Encoding.UTF8);
        List<RdlDowngradeEngine.R2Element> pListElements;
        RdlDowngradeEngine pDowngradeEngine = new RdlDowngradeEngine(cboErrorLevel.SelectedIndex);
        string sXml = pDowngradeEngine.DowngradeReport(sReportXml, out pListElements);

        // Indent XML: http://stackoverflow.com/questions/203528/what-is-the-simplest-way-to-get-indented-xml-with-line-breaks-from-xmldocument

        txtOutput.Text = System.Xml.Linq.XElement.Parse(sXml).ToString(); 

        if (pListElements.Count > 0)
        {
          pListElements.Sort((pIn1, pIn2) => pIn1.Severity.CompareTo(pIn2.Severity));

          foreach (RdlDowngradeEngine.R2Element pCurElement in pListElements)
          {
            if (pCurElement.Severity != RdlDowngradeEngine.ErrorLevel.FatalError)
            {
              txtDowngradeStatus.AppendText(pCurElement.Severity.ToString() + " - " + pCurElement.LocalizedWarning + Environment.NewLine);
            }
            else
            {
              txtDowngradeStatus.AppendText(pCurElement.Severity.ToString() + " - " + pCurElement.LocalizedError + Environment.NewLine);
            }
          }
        }
      }
      catch (Exception pException)
      {
        MessageBox.Show(pException.Message);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    private void cmdSaveAs_Click(object sender, EventArgs e)
    {
      try
      {
        if (dlgSaveAs.ShowDialog() == DialogResult.OK)
        {
          File.WriteAllText(dlgSaveAs.FileName, txtOutput.Text, Encoding.GetEncoding("ISO-8859-1"));
        }
      }
      catch (Exception pException)
      {
        MessageBox.Show(pException.Message);
      }
    }

    private string BasePath
    {
      get
      {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      }
    }
  }
}
