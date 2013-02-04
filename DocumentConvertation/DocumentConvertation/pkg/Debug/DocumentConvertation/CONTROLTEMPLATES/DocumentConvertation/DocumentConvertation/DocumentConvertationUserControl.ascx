<%@ Assembly Name="DocumentConvertation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c24b91b969abeefa" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentConvertationUserControl.ascx.cs" Inherits="DocumentConvertation.DocumentConvertation.DocumentConvertationUserControl" %>

<link href="../../../_layouts/DocumentConvertation/Stylesheet1.css" rel="stylesheet"
    type="text/css" />
    
 <div class="Container">
     <h1>Document conversation</h1><br/>
<table>
    <tr class="header">
        <td class="contentStyle">
            Select Source Library
        </td>
        <td>
            <asp:DropDownList ID="ddlSourceLists" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSourceListsSelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="header">
        <td class="contentStyle">
            Select Target Library
        </td>
        <td>
            <asp:DropDownList ID="ddlTargetLists" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="header">
        <td class="contentStyle">
            Select Target Type
        </td>
        <td>
            <asp:DropDownList ID="ddlTypes" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="header">
        <td class="contentStyle">
            Overwrite Options
        </td>
        <td>
            <asp:DropDownList ID="ddlOverWrite" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="header">
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="data"  colspan="2" valign="top" style="text-align: center">
            <asp:Button CssClass="btnStyle" ID="btnConvert" runat="server" Text="Start Conversion" OnClick="BtnConvertClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button CssClass="btnStyle" ID="btnCheckResult" runat="server" Text="Check Conversion Result" 
                OnClick="BtnCheckResultClick" Width="177px" />
        </td>
       
    </tr>
    <tr>
        <td class="data" colspan="2" valign="top">
            <asp:Label ID="lblResult" runat="server" Text="No Process"></asp:Label>
        </td>
    </tr>
    <tr>
        <td  class="data" colspan="2" valign="top">
            Select Documents :
            <asp:CheckBoxList ID="chkItems" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
</table>
</div>
