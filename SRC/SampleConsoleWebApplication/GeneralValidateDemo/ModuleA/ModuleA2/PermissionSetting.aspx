<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionSetting.aspx.cs"
    Inherits="WebApplicationConsole.GeneralValidateDemo.ModuleA.ModuleA2.PermissionSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .divCommon
        {
            padding:2px;
            border: 1px solid;
        }
        
        .divAppliation
        {
            border-color: Green;
        }
        
        .divModule
        {
            border-color: Red;
        }
        .divSubModule
        {
            border-color: Blue;
        }
    </style>
    <script language="javascript" type="text/javascript" >

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="RepeaterApplication" runat="server" OnItemDataBound="RepeaterApplication_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="divCommon divAppliation">
                    <asp:Literal runat="server" ID="applicationName"></asp:Literal>
                    <asp:Repeater ID="RepeaterModule" runat="server">
                        <ItemTemplate>
                            <div class="divCommon divModule">
                                <asp:Literal runat="server" ID="moduleName"></asp:Literal>
                                <asp:Repeater ID="RepeaterSubModule" runat="server">
                                    <ItemTemplate>
                                        <div class="divCommon divSubModule">
                                            <asp:Literal runat="server" ID="subModuleName"></asp:Literal>
                                            <asp:Literal ID="litDisplayPermissionItem" runat="server"></asp:Literal>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
