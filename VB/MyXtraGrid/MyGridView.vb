Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraGrid.Scrolling
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.Utils

Namespace MyXtraGrid
	Public Class MyGridView
		Inherits DevExpress.XtraGrid.Views.Grid.GridView
		Public Sub New()
			Me.New(Nothing)
		End Sub
		Public Sub New(ByVal grid As DevExpress.XtraGrid.GridControl)
			MyBase.New(grid)
		End Sub
		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property

		Protected Overrides Function CreateScrollInfo() As ScrollInfo
			Dim newMyScrollInfo As ScrollInfo = MyBase.CreateScrollInfo()
			AddHandler newMyScrollInfo.VScroll.Scroll, AddressOf VScroll_Scroll
			Return newMyScrollInfo
		End Function

		Private Sub VScroll_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
			If e.Type = ScrollEventType.ThumbTrack Then
				Dim p As Point = GridControl.PointToScreen((CType(sender, VCrkScrollBar)).Location)
				p.Y = Control.MousePosition.Y
				MyShowHint(p, DevExpress.Utils.ToolTipLocation.TopLeft)
			End If
		End Sub

		Private Overloads Function GetToolTipText() As String
			Return String.Format("{0} - {1}", TopRowIndex, TopRowIndex + ViewInfo.RowsLoadInfo.ResultRowCount - 1)
		End Function

		Protected Overridable Sub MyShowHint(ByVal position As Point, ByVal location As ToolTipLocation)
			If GridControl Is Nothing Then
				Return
			End If
			Dim tool As ToolTipControllerShowEventArgs = GridControl.ToolTipController.CreateShowArgs()
			tool.ToolTip = GetToolTipText()
			tool.SelectedObject = Me
			tool.SelectedControl = GridControl
			tool.AutoHide = False
			tool.ToolTipLocation = location
			GridControl.ToolTipController.ShowHint(tool, position)
		End Sub
	End Class
End Namespace