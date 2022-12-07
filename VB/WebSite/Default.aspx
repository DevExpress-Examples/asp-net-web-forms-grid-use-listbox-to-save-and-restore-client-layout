<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>How to Save/Load ClientLayout</title>
</head>
<body>
	<form id="form1" runat="server">
	<dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
		DataSourceID="SqlDataSource1" KeyFieldName="CategoryID" OnClientLayout="Grid_ClientLayout"
		OnCustomCallback="Grid_CustomCallback">
		<ClientSideEvents EndCallback="function(s, e) {
			if (grid.cpCallback == 0)    
				lb.PerformCallback(&quot;refresh&quot;);
			}" />
		<Columns>
			<dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="0">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="1">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
			</dx:GridViewDataTextColumn>
		</Columns>
		<SettingsPager PageSize="5" />
	</dx:ASPxGridView>
		<dx:ASPxListBox ID="ListBox" runat="server" ClientInstanceName="lb" OnCallback="ListBox_Callback">
			<ClientSideEvents EndCallback="function(s, e) {
				lb.SetSelectedIndex(lb.cpSelected);    
			}" SelectedIndexChanged="function(s, e) {
				grid.PerformCallback(&quot;refresh&quot;);
		}" />
	</dx:ASPxListBox>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
		SelectCommand="SELECT [CategoryID], [CategoryName], [Description], [Picture] FROM [Categories]">
	</asp:SqlDataSource>
	</form>
</body>
</html>