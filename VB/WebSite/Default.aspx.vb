Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private isSaveLayout As Boolean = True

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Grid.JSProperties("cpCallback") = 0
		If Session("layout") Is Nothing Then
			Session("layout") = New Dictionary(Of String, String)()
		Else
			Dim dictionary As Dictionary(Of String, String) = TryCast(Session("layout"), Dictionary(Of String, String))
			ListBox.Items.Clear()
			For Each item As KeyValuePair(Of String, String) In dictionary
				Dim editItem As New ListEditItem(item.Key, item.Value)
				ListBox.Items.Add(editItem)
			Next item
		End If
	End Sub
	Protected Sub ListBox_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		If Session("selectedLayout") IsNot Nothing Then
			ListBox.JSProperties("cpSelected") = ListBox.Items.IndexOfValue(Session("selectedLayout"))
		End If
	End Sub
	Protected Sub Grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		If ListBox.SelectedItem IsNot Nothing Then
			Grid.LoadClientLayout(ListBox.SelectedItem.Value.ToString())
			isSaveLayout = False
			Grid.JSProperties("cpCallback") = 1
		End If
	End Sub
	Protected Sub Grid_ClientLayout(ByVal sender As Object, ByVal e As ASPxClientLayoutArgs)
		If e.LayoutMode = ClientLayoutMode.Saving AndAlso isSaveLayout Then
			Dim key As String = DateTime.Now.ToString()
			Dim layout As String = e.LayoutData
			Dim dictionary As Dictionary(Of String, String) = TryCast(Session("layout"), Dictionary(Of String, String))
			If (Not dictionary.ContainsValue(layout)) Then
				ListBox.Items.Add(key, layout)
				dictionary(key) = layout
			Else
				Session("selectedLayout") = layout
			End If
			Session("layout") = dictionary
		End If
	End Sub
End Class
