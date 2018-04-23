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
'INSTANT VB NOTE: The field checkedItems was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private checkedItems_Renamed As New List(Of Object)()
		Private focusedItem As Object = Nothing

		Public Sub New(ByVal form As CustomizationForm)
			MyBase.New(form)
		End Sub

		Protected Function CalcCheckBoxRect(ByVal cache As GraphicsCache, ByVal bounds As Rectangle) As Rectangle
			Dim checkBox As New RepositoryItemCheckEdit()
'INSTANT VB NOTE: The variable viewInfo was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim viewInfo_Renamed As CheckEditViewInfo = CType(checkBox.CreateViewInfo(), CheckEditViewInfo)
			viewInfo_Renamed.Bounds = bounds
			viewInfo_Renamed.CalcViewInfo(cache.Graphics)
			Dim args As New ControlGraphicsInfoArgs(viewInfo_Renamed, cache, bounds)
			Dim ci As BaseCheckObjectInfoArgs = CType(args.ViewInfo, CheckEditViewInfo).CheckInfo

			Dim retValue As New Rectangle(bounds.X, ci.GlyphRect.Y, ci.GlyphRect.Width, ci.GlyphRect.Height)
			Return retValue
		End Function

		Protected Overrides Sub DrawItemObject(ByVal cache As GraphicsCache, ByVal index As Integer, ByVal bounds As Rectangle, ByVal itemState As DrawItemState)
			Dim checkBoxRect As Rectangle = CalcCheckBoxRect(cache, bounds)
			bounds.X += checkBoxRect.Width
			bounds.Width -= checkBoxRect.Width

			MyBase.DrawItemObject(cache, index, bounds, itemState)

			Dim checkState As ButtonState = ButtonState.Normal

			If index = pushedIndex Then
				checkState = ButtonState.Pushed
			End If

			If checkedItems_Renamed.Contains(Items(index)) Then
				checkState = ButtonState.Checked
			End If

			cache.Paint.DrawCheckBox(cache.Graphics, checkBoxRect, checkState)
			If focusedItem IsNot Nothing AndAlso Items.IndexOf(focusedItem) = index Then
				cache.Paint.DrawFocusRectangle(cache.Graphics, checkBoxRect, CustomizationForm.ForeColor, CustomizationForm.BackColor)
			End If
		End Sub

		Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
			Dim mousePoint As New Point(e.X, e.Y)
			Dim pointedItem As Object = ItemByPoint(New Point(e.X, e.Y))
			Dim itemIndex As Integer = Items.IndexOf(pointedItem)
			Dim itemRect As Rectangle = GetItemRectangle(itemIndex)
			GraphicsInfo.Default.AddGraphics(Nothing)
			Dim checkBoxRect As Rectangle
			Try
				checkBoxRect = CalcCheckBoxRect(GraphicsInfo.Default.Cache, itemRect)
			Finally
				GraphicsInfo.Default.ReleaseGraphics()
			End Try

			If checkBoxRect.Contains(mousePoint) AndAlso e.Button = MouseButtons.Left Then
				pushedIndex = itemIndex
				Me.InvalidateObject(pointedItem)

				Return
			ElseIf Not checkBoxRect.Contains(mousePoint) Then
				MyBase.OnMouseDown(e)
			End If
		End Sub

		Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
			Dim mousePoint As New Point(e.X, e.Y)
			Dim pointedItem As Object = ItemByPoint(New Point(e.X, e.Y))
			Dim itemIndex As Integer = Items.IndexOf(pointedItem)
			Dim itemRect As Rectangle = GetItemRectangle(itemIndex)
			GraphicsInfo.Default.AddGraphics(Nothing)
			Dim checkBoxRect As Rectangle
			Try
				checkBoxRect = CalcCheckBoxRect(GraphicsInfo.Default.Cache, itemRect)
			Finally
				GraphicsInfo.Default.ReleaseGraphics()
			End Try


			If checkBoxRect.Contains(mousePoint) AndAlso e.Button = MouseButtons.Left Then
				If ModifierKeys = Keys.Shift Then
					Dim startIndex As Integer = Items.IndexOf(focusedItem)
					Dim endIndex As Integer = Items.IndexOf(pointedItem)
					Dim check As Boolean = Not checkedItems_Renamed.Contains(pointedItem)

					If endIndex >= startIndex Then
						For i As Integer = startIndex To endIndex
							If check AndAlso Not checkedItems_Renamed.Contains(Items(i)) Then
								checkedItems_Renamed.Add(Items(i))
							ElseIf Not check AndAlso checkedItems_Renamed.Contains(Items(i)) Then
								checkedItems_Renamed.Remove(Items(i))
							End If
						Next i
						Else
						For i As Integer = endIndex To startIndex - 1
							If check AndAlso Not checkedItems_Renamed.Contains(Items(i)) Then
								checkedItems_Renamed.Add(Items(i))
							ElseIf Not check AndAlso checkedItems_Renamed.Contains(Items(i)) Then
								checkedItems_Renamed.Remove(Items(i))
							End If
						Next i
						End If
				ElseIf ModifierKeys = Keys.None Then
					If checkedItems_Renamed.Contains(pointedItem) Then
						checkedItems_Renamed.Remove(pointedItem)
					Else
						checkedItems_Renamed.Add(pointedItem)
					End If
				End If

				focusedItem = pointedItem
				pushedIndex = -1
				Me.InvalidateObject(pointedItem)

				Return
			ElseIf Not checkBoxRect.Contains(mousePoint) Then
				MyBase.OnMouseUp(e)
			End If
		End Sub

		Public ReadOnly Property CheckedItems() As List(Of Object)
			Get
				Return checkedItems_Renamed
			End Get
		End Property
	End Class
End Namespace
