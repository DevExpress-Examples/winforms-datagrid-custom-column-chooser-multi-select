Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Customization
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.Customization

Namespace MultiSelectColumnCustomization

    Public Class MultiSelectCustomizationForm
        Inherits CustomizationForm

        Public Sub New(ByVal view As GridView)
            MyBase.New(view)
        End Sub

        Protected Overrides Function CreateCustomizationListBox() As CustomizationListBoxBase
            Return New MultiSelectColumnCustomizationListBox(Me)
        End Function

        Protected Overrides Sub CreateListBox()
            MyBase.CreateListBox()
            Dim bottomPanel As Panel = New Panel()
            bottomPanel.Parent = Me
            bottomPanel.Dock = DockStyle.Bottom
            bottomPanel.Height = 30
            bottomPanel.SendToBack()
            Dim bAddCheckedCols As Button = New Button()
            bAddCheckedCols.Parent = bottomPanel
            bAddCheckedCols.Dock = DockStyle.Fill
            bAddCheckedCols.Text = "Add checked columns to grid"
            AddHandler bAddCheckedCols.Click, New EventHandler(AddressOf OnButtonAddCheckedColumns_Click)
        End Sub

        Private Sub OnButtonAddCheckedColumns_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim listBox As MultiSelectColumnCustomizationListBox = CType(ActiveListBox, MultiSelectColumnCustomizationListBox)
            For i As Integer = 0 To listBox.CheckedItems.Count - 1
                If listBox.CheckedItems(i) IsNot Nothing Then CType(listBox.CheckedItems(i), GridColumn).Visible = True
            Next
        End Sub
    End Class
End Namespace
