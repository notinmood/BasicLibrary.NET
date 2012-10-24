<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplicationConsole._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        插件系统Web测试项目
    </h2>
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Height="105px" Width="265px" 
            TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnLoad" runat="server" Text="载入" onclick="btnLoad_Click" />
        <asp:Button ID="btnDisplay"
            runat="server" Text="显示" onclick="btnDisplay_Click" />
    </p>
    <p>
       
    </p>
</asp:Content>
