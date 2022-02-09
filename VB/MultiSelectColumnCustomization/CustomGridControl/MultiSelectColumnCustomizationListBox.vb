Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid.Customization

Namespace MultiSelectColumnCustomization

    Public Class MultiSelectColumnCustomizationListBox
        Inherits ColumnCustomizationListBox

        Private pushedIndex As Integer = -1

        Private checkedItemsField As List(Of Object) = New List(Of Object)()

        Private focusedItem As Object = Nothing

        Public Sub New(ByVal form As CustomizationForm)
            MyBase.New(form)
        End Sub

        Protected Function CalcCheckBoxRect(ByVal cache As GraphicsCache, ByVal bounds As Rectangle) As Rectangle
            Dim checkBox As RepositoryItemCheckEdit = New RepositoryItemCheckEdit()
            Dim viewInfo As CheckEditViewInfo = CType(checkBox.CreateViewInfo(), CheckEditViewInfo)
            viewInfo.Bounds = bounds
            viewInfo.CalcViewInfo(cache.Graphics)
            Dim args As ControlGraphicsInfoArgs = New ControlGraphicsInfoArgs(viewInfo, cache, bounds)
            Dim ci As BaseCheckObjectInfoArgs = CType(args.ViewInfo, CheckEditViewInfo).CheckInfo
            Dim retValue As Rectangle = New Rectangle(bounds.X, ci.GlyphRect.Y, ci.GlyphRect.Width, ci.GlyphRect.Height)
            Return retValue
        End Function

        Protected Overrides Sub DrawItemObject(ByVal cache As GraphicsCache, ByVal index As Integer, ByVal bounds As Rectangle, ByVal itemState As DrawItemState)
            Dim checkBoxRect As Rectangle = CalcCheckBoxRect(cache, bounds)
            bounds.X += checkBoxRect.Width
            bounds.Width -= checkBoxRect.Width
            MyBase.DrawItemObject(cache, index, bounds, itemState)
            Dim checkState As ButtonState = ButtonState.Normal
            If index = pushedIndex Then checkState = ButtonState.Pushed
            If checkedItemsField.Contains(Items(index)) Then checkState = ButtonState.Checked
            cache.Paint.DrawCheckBox(cache.Graphics, checkBoxRect, checkState)
            If focusedItem IsNot Nothing AndAlso Items.IndexOf(focusedItem) = index Then cache.Paint.DrawFocusRectangle(cache.Graphics, checkBoxRect, CustomizationForm.ForeColor, CustomizationForm.BackColor)
        End Sub

        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
            Dim mousePoint As Point = New Point(e.X, e.Y)
            Dim pointedItem As Object = ItemByPoint(New Point(e.X, e.Y))
            Dim itemIndex As Integer = Items.IndexOf(pointedItem)
            Dim itemRect As Rectangle = GetItemRectangle(itemIndex)
            Dim checkBoxRect As Rectangle = CalcCheckBoxRect(New GraphicsCache(CreateGraphics()), itemRect)
            If checkBoxRect.Contains(mousePoint) AndAlso e.Button = MouseButtons.Left Then
                pushedIndex = itemIndex
                InvalidateObject(pointedItem)
                Return
            ElseIf Not checkBoxRect.Contains(mousePoint) Then
                MyBase.OnMouseDown(e)
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
            Dim mousePoint As Point = New Point(e.X, e.Y)
            Dim pointedItem As Object = ItemByPoint(New Point(e.X, e.Y))
            Dim itemIndex As Integer = Items.IndexOf(pointedItem)
            Dim itemRect As Rectangle = GetItemRectangle(itemIndex)
            Dim checkBoxRect As Rectangle = CalcCheckBoxRect(New GraphicsCache(CreateGraphics()), itemRect)
            If checkBoxRect.Contains(mousePoint) AndAlso e.Button = MouseButtons.Left Then
                If ModifierKeys = Keys.Shift Then
                    Dim startIndex As Integer = Items.IndexOf(focusedItem)
                    Dim endIndex As Integer = Items.IndexOf(pointedItem)
                    Dim check As Boolean = Not checkedItemsField.Contains(pointedItem)
                    If endIndex >= startIndex Then
                        For i As Integer = startIndex To endIndex
                            If check AndAlso Not checkedItemsField.Contains(Items(i)) Then
                                checkedItemsField.Add(Items(i))
                            ElseIf Not check AndAlso checkedItemsField.Contains(Items(i)) Then
                                checkedItemsField.Remove(Items(i))
                            End If
                        Next
                    Else
                        For i As Integer = endIndex To startIndex - 1
                            If check AndAlso Not checkedItemsField.Contains(Items(i)) Then
                                checkedItemsField.Add(Items(i))
                            ElseIf Not check AndAlso checkedItemsField.Contains(Items(i)) Then
                                checkedItemsField.Remove(Items(i))
                            End If
                        Next
                    End If
                ElseIf ModifierKeys = Keys.None Then
                    If checkedItemsField.Contains(pointedItem) Then
                        checkedItemsField.Remove(pointedItem)
                    Else
                        checkedItemsField.Add(pointedItem)
                    End If
                End If

                focusedItem = pointedItem
                pushedIndex = -1
                InvalidateObject(pointedItem)
                Return
            ElseIf Not checkBoxRect.Contains(mousePoint) Then
                MyBase.OnMouseUp(e)
            End If
        End Sub

        Public ReadOnly Property CheckedItems As List(Of Object)
            Get
                Return checkedItemsField
            End Get
        End Property
    End Class
End Namespace
