Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.Customization

Namespace MultiSelectColumnCustomization

    Public Class GridViewOverride
        Inherits GridView

        Public Sub New(ByVal ownerGrid As GridControl)
            MyBase.New(ownerGrid)
        End Sub

        Public Sub New()
        End Sub

        Protected Overrides Function CreateCustomizationForm() As CustomizationForm
            Return New MultiSelectCustomizationForm(Me)
        End Function

        Protected Overrides ReadOnly Property ViewName As String
            Get
                Return "GridViewOverride"
            End Get
        End Property
    End Class
End Namespace
