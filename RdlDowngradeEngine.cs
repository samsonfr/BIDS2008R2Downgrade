//===========================================================================================================
// Class :       RdlDowngradeEngine
// Author :      Frederick Samson, SYNTELL inc. - http://www.syntell.com
//
// Copyright 2013 by Syntell inc.
// ==========================================================================================================

//Microsoft Public License (MS-PL)
// http://www.microsoft.com/en-us/openness/licenses.aspx

//This license governs use of the accompanying software. If you use the software, you
//accept this license. If you do not accept the license, do not use the software.

//1. Definitions
//The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
//same meaning here as under U.S. copyright law.
//A "contribution" is the original software, or any additions or changes to the software.
//A "contributor" is any person that distributes its contribution under this license.
//"Licensed patents" are a contributor's patent claims that read directly on its contribution.

//2. Grant of Rights
//(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

//3. Conditions and Limitations
//(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Microsoft.VisualBasic;

namespace BIDS2008R2Downgrade
{
  public class RdlDowngradeEngine
  {
    // ============================================================================================
    // Public constants
    // ============================================================================================

    #region "Public constants"

    public const string gcsDefaultNamespace2008R1 = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";
    public const string gcsDefaultNamespace2008R2 = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition";
    public const string gcsComponentDefinition2008R2 = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition";

    #endregion

    // ============================================================================================
    // Public data classes
    // ============================================================================================

    #region "Public data classes"

    // Based on error level from http://msdn.microsoft.com/en-us/library/ee635898(v=sql.105).aspx

    public enum ErrorLevel
    {
      FatalError = 0, // Most severe and unavoidable build issues that prevent preview and deployment of reports.
      LayoutDrasticError = 1, // Severe build issues that change the report layout drastically. 
      LayoutSignificantError = 2, // Less severe build issues that change report layout in significantly.
      LayoutMinorError = 3, // Minor build issues that change the report layout in minor ways that might not be noticeable.
      Warning = 4, // Used only for publishing warnings.
      LetDeployFail = 5 // That's what BIDS does with Lookup and aggregates of aggregates (ex. Avg(Sum(... )
    }

    public class R2Element
    {
      public ErrorLevel Severity { get; set; }
      public string ElementName { get; set; }
      public string ElementType { get; set; }
      public string ElementWithNameType { get; set; }
      public string ElementXPath { get; set; }
      public string LocalizedWarning { get; set; } // The localized warning that would be returned by BIDS
      public string LocalizedError { get; set; } // The localized error that would be returned by BIDS
    }

    public class DesiredSeverity
    {
      public DesiredSeverity(int nBIDSErrorLevel)
      {
        this.SeverityForNewDataSources = ErrorLevel.FatalError;
        this.SeverityForWritableVariables = ErrorLevel.FatalError;
        this.SeverityForSharedDataset = ErrorLevel.FatalError;

        this.SeverityForIndicator = nBIDSErrorLevel > 1 ? ErrorLevel.FatalError : ErrorLevel.LayoutDrasticError;
        this.SeverityForMap = nBIDSErrorLevel > 1 ? ErrorLevel.FatalError : ErrorLevel.LayoutDrasticError;

        this.SeverityForRotate270 = nBIDSErrorLevel > 2 ? ErrorLevel.FatalError : ErrorLevel.LayoutSignificantError;

        this.SeverityForNewPageBreakOptions = nBIDSErrorLevel > 2 ? ErrorLevel.FatalError : ErrorLevel.LayoutMinorError;
        this.SeverityForRenderFormat = nBIDSErrorLevel > 2 ? ErrorLevel.FatalError : ErrorLevel.LayoutMinorError;

        this.SeverityForOverallPageNumber = nBIDSErrorLevel > 2 ? ErrorLevel.LayoutMinorError : ErrorLevel.Warning;
        this.SeverityForOverallTotalPages = nBIDSErrorLevel > 2 ? ErrorLevel.LayoutMinorError : ErrorLevel.Warning;
        this.SeverityForPageName = nBIDSErrorLevel > 2 ? ErrorLevel.LayoutMinorError : ErrorLevel.Warning;
      }

      public ErrorLevel SeverityForNewDataSources { get; set; }
      public ErrorLevel SeverityForWritableVariables { get; set; }
      public ErrorLevel SeverityForSharedDataset { get; set; }

      public ErrorLevel SeverityForIndicator { get; set; }
      public ErrorLevel SeverityForMap { get; set; }

      public ErrorLevel SeverityForRotate270 { get; set; }

      public ErrorLevel SeverityForNewPageBreakOptions { get; set; }
      public ErrorLevel SeverityForRenderFormat { get; set; }

      public ErrorLevel SeverityForOverallPageNumber { get; set; }
      public ErrorLevel SeverityForOverallTotalPages { get; set; }
      public ErrorLevel SeverityForPageName { get; set; }
    }

    #endregion

    // ============================================================================================
    // Construction / Destruction
    // ============================================================================================

    #region "Construction / Destruction"

    public RdlDowngradeEngine(int nBIDSErrorLevel) : this(new DesiredSeverity(nBIDSErrorLevel), nBIDSErrorLevel) {}
   
    public RdlDowngradeEngine(DesiredSeverity pDesiredSeverity, int nBIDSErrorLevel)
    {
      mpDesiredSeverity = pDesiredSeverity;
      mnBIDSErrorLevel = nBIDSErrorLevel;
    }

    #endregion

    // ============================================================================================
    // Public member functions
    // ============================================================================================

    #region "Public member functions"

    public string DowngradeReport(string sReportXml, out List<R2Element> pListElementsOut)
    {
      XmlReader pReader = XmlReader.Create(new StringReader(sReportXml), new XmlReaderSettings());
      int num = (int)pReader.MoveToContent();
      string sNamespaceURI = pReader.NamespaceURI;
      pReader.Close();

      // New reports created in BIDS R2 stay in R1 until a R2 only element is used

      bool bIsR2 = sNamespaceURI.ToLower() == gcsDefaultNamespace2008R2;
      mpDOM = new XmlDocument();

      if (bIsR2)
      {
        // Change namespace from R2 to R1

        string sNewXml = Strings.Replace(sReportXml, gcsDefaultNamespace2008R2, gcsDefaultNamespace2008R1, 1, 1, CompareMethod.Text);
        string sFndCD = "xmlns:cl=\"" + gcsComponentDefinition2008R2 + "\"";
        sNewXml = Strings.Replace(sNewXml, sFndCD, "", 1, 1, CompareMethod.Text);

        // Load in a DOM with 2008 R1 namespace

        mpDOM.LoadXml(sNewXml);
      }
      else
      {
        mpDOM.LoadXml(sReportXml);
      }

      mpNSManager = new XmlNamespaceManager(mpDOM.NameTable);
      mpNSManager.AddNamespace("dns", gcsDefaultNamespace2008R1);
      mpListElements = new List<R2Element>();

      if (bIsR2)
      {
        // Move Width, Page and Body under <Report>

        XmlNode pNodeRoot = mpDOM.DocumentElement;

        MoveAsChild(pNodeRoot, "dns:ReportSections/dns:ReportSection/dns:Width", mpNSManager);
        MoveAsChild(pNodeRoot, "dns:ReportSections/dns:ReportSection/dns:Page", mpNSManager);
        MoveAsChild(pNodeRoot, "dns:ReportSections/dns:ReportSection/dns:Body", mpNSManager);

        // <ReportSections> is not used in R1

        XmlNode pNodeFndReportSections = pNodeRoot.SelectSingleNode("dns:ReportSections", mpNSManager);

        if (pNodeFndReportSections != null)
        {
          pNodeFndReportSections.ParentNode.RemoveChild(pNodeFndReportSections);
        }

        // Remove unsupported elements (if any)

        HandleElements("dns:Body/dns:ReportItems//dns:GaugePanel[count(dns:StateIndicators) > 0]", mpDesiredSeverity.SeverityForIndicator, RemoveNodeFromParent, null, Localization.ErrorIndicator, Localization.WarningIndicator);
        HandleElements("dns:Body/dns:ReportItems//dns:Map", mpDesiredSeverity.SeverityForMap, RemoveNodeFromParent, null, Localization.ErrorMap, Localization.WarningMap);
        HandleElements("//dns:WritingMode[text() = 'Rotate270']", mpDesiredSeverity.SeverityForRotate270, ChangeNodeValue, new string[] { "Vertical" }, Localization.ErrorRotate270, Localization.WarningRotate270);
        HandleElements("dns:DataSources/dns:DataSource[text() = 'SHAREPOINTLIST' or text() = 'SQLAZURE' or text() = 'SQLPDW']", mpDesiredSeverity.SeverityForNewDataSources, RemoveNodeFromParent, null, Localization.ErrorNewDataSource, Localization.WarningNewDataSource);
        HandleElements("dns:Body/dns:ReportItems//dns:PageBreak/dns:Disabled | dns:Body/dns:ReportItems//dns:PageBreak/dns:ResetPageNumber", mpDesiredSeverity.SeverityForNewPageBreakOptions, RemoveNodeFromParent, null, Localization.ErrorNewPageBreakOptions, Localization.WarningNewPageBreakOptions);
        HandleElements("dns:Variables/dns:Variable/dns:Writable", mpDesiredSeverity.SeverityForWritableVariables, RemoveNodeFromParent, null, Localization.ErrorWritableVariable, Localization.WarningWritableVariable);
        HandleElements("dns:DataSets/dns:DataSet[count(dns:SharedDataSet) > 0]", mpDesiredSeverity.SeverityForSharedDataset, RemoveNodeFromParent, null, Localization.ErrorSharedDataset, Localization.WarningSharedDataset);

        // It's invalid to have a DataSets node without any DataSet under it

        XmlNode pNodeFndDataSets = pNodeRoot.SelectSingleNode("dns:DataSets", mpNSManager);

        if (pNodeFndDataSets != null && !pNodeFndDataSets.HasChildNodes)
        {
          pNodeFndDataSets.ParentNode.RemoveChild(pNodeFndDataSets);
        }

        // Even though Sparklines and DataBars are new in the Toolbox, they use 2008 R1 RDL syntax
        // It's just a designer trick
      }

      // BIDS doesn't detect aggregates of aggregates but fail at deploy time, anyway we can't fix them ...
      // http://prologika.com/CS/blogs/blog/archive/2009/11/11/aggregates-of-aggregates.aspx
      // =Avg(Sum(Fields!Internet_Sales_Amount.Value, "Year")) 

      // Same for Lookup, LookupSet and Multilookup
      //=Lookup(Fields!SaleProdId.Value, Fields!ProductID.Value,  Fields!Name.Value, "Product")
      //=LookupSet(Fields!TerritoryGroupID.Value, Fields!TerritoryID.Value, Fields!StoreName.value, "Stores")
      //=Multilookup(Split(Fields!ProductCategoryIDList.Value, ","), Fields!ProductCategoryID.Value, Fields!ProductCategoryName.value, "Categories") 

      ReplaceTextInElements("Globals!RenderFormat.Name", "\"RPL\"", mpDesiredSeverity.SeverityForRenderFormat);
      ReplaceTextInElements("Globals!RenderFormat.IsInteractive", "True", mpDesiredSeverity.SeverityForRenderFormat);
      ReplaceTextInElements("Globals!OverallPageNumber", "Globals!PageNumber", mpDesiredSeverity.SeverityForOverallPageNumber);
      ReplaceTextInElements("Globals!OverallTotalPages", "Globals!TotalPages", mpDesiredSeverity.SeverityForOverallTotalPages);
      ReplaceTextInElements("Globals!PageName", "\"\"", mpDesiredSeverity.SeverityForPageName);

      pListElementsOut = mpListElements;
      return mpDOM.OuterXml;
    }

    #endregion

    // ============================================================================================
    // Private member functions
    // ============================================================================================

    #region "Private member functions"

    private static void MoveAsChild(XmlNode pNodeParent, string sXPathToElement, XmlNamespaceManager pNSManager)
    {
      XmlNode pNodeFnd = pNodeParent.SelectSingleNode(sXPathToElement, pNSManager);

      if (pNodeFnd != null)
      {
        pNodeFnd.ParentNode.RemoveChild(pNodeFnd);
        pNodeParent.AppendChild(pNodeFnd);
      }
    }

    private void HandleElements(string sXPath, ErrorLevel eSeverity, Action<XmlNode, string[]> pActionOnNode, string[] asNewValue, string sErrorTemplate, string sWarningTemplate)
    {
      if (eSeverity != ErrorLevel.LetDeployFail)
      {
        XmlNode pNodeParent = mpDOM.DocumentElement;

        foreach (XmlNode pNodeFnd in pNodeParent.SelectNodes(sXPath, mpNSManager))
        {
          string sCurName = GetNodeAttributeValue(pNodeFnd, "Name");
          string sCurType = pNodeFnd.LocalName;
          string sCurElementWithNameType = null;

          if (!String.IsNullOrEmpty(sCurName))
          {
            sCurElementWithNameType = sCurType;
          }
          else
          {
            XmlNode pNodeWithName = pNodeFnd.SelectSingleNode("ancestor::dns:*[string-length(@Name) > 0]", mpNSManager);

            if (pNodeWithName != null)
            {
              sCurName = GetNodeAttributeValue(pNodeWithName, "Name");
              sCurElementWithNameType = pNodeWithName.LocalName;
            }
          }

          // Action will vary so use a delegate

          pActionOnNode(pNodeFnd, asNewValue);

          // Add information about the R2Element

          R2Element pElement = new R2Element() { Severity = eSeverity, ElementName = sCurName, ElementType = sCurType, ElementWithNameType = sCurElementWithNameType, ElementXPath = sXPath };

          if (sWarningTemplate != null)
          {
            pElement.LocalizedWarning = String.Format(sWarningTemplate, sCurName);
          }

          if (sErrorTemplate != null)
          {
            pElement.LocalizedError = String.Format(sErrorTemplate, sCurName);
          }

          mpListElements.Add(pElement);
        }
      }
    }

    private static string GetNodeAttributeValue(XmlNode pNodeSrc, string sAttrName)
    {
      if (pNodeSrc.Attributes != null)
      {
        XmlNode pAttr = pNodeSrc.Attributes.GetNamedItem(sAttrName);

        if (pAttr != null)
        {
          return pAttr.InnerText;
        }
      }

      return null;
    }

    private void ReplaceTextInElements(string sSearchText, string sReplaceBy, ErrorLevel eSeverity)
    {
      string sXPathCmd = String.Format("//dns:*[contains(translate(text(), '{0}', '{1}'), '{1}')]", sSearchText.ToLower(), sSearchText.ToUpper());
      string sErrorTemplate = Localization.ErrorBuiltInField.Replace("{OldFieldName}", sSearchText).Replace("{NewFieldName}", sReplaceBy);
      string sWarningTemplate = Localization.WarningBuiltInField.Replace("{OldFieldName}", sSearchText).Replace("{NewFieldName}", sReplaceBy);
      HandleElements(sXPathCmd, eSeverity, ReplaceNodeValue, new string[] { sSearchText, sReplaceBy }, sErrorTemplate, sWarningTemplate);
    }

    private static void RemoveNodeFromParent(XmlNode pNode, string[] asUnused)
    {
      if (pNode != null && pNode.ParentNode != null)
      {
        pNode.ParentNode.RemoveChild(pNode);
      }
    }

    private static void ChangeNodeValue(XmlNode pNode, string[] asNewValue)
    {
      if (pNode != null)
      {
        pNode.InnerText = asNewValue != null && asNewValue.Length == 1 ? asNewValue[0] : String.Empty;
      }
    }

    private static void ReplaceNodeValue(XmlNode pNode, string[] asNewValue)
    {
      if (pNode != null && asNewValue != null && asNewValue.Length == 2)
      {
        pNode.InnerText = Strings.Replace(pNode.InnerText, asNewValue[0], asNewValue[1], 1, -1, CompareMethod.Text);
      }
    }

    #endregion

    // ============================================================================================
    // Private data members
    // ============================================================================================

    #region "Private data members"

    private int mnBIDSErrorLevel = -1;
    private DesiredSeverity mpDesiredSeverity = null;
    private XmlDocument mpDOM = null;
    private XmlNamespaceManager mpNSManager = null;
    private List<R2Element> mpListElements = null;

    #endregion
  }
}
