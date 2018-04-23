using System;
using DevExpress.XtraGrid.Scrolling;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils;

namespace MyXtraGrid {
	public class MyGridView : DevExpress.XtraGrid.Views.Grid.GridView {
		public MyGridView() : this(null) {}
		public MyGridView(DevExpress.XtraGrid.GridControl grid) : base(grid) {
		}
		protected override string ViewName { get { return "MyGridView"; } }

        protected override ScrollInfo CreateScrollInfo()
        {
            ScrollInfo newMyScrollInfo = base.CreateScrollInfo();
            newMyScrollInfo.VScroll.Scroll += VScroll_Scroll;
            return newMyScrollInfo;
        }

        void VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.ThumbTrack)
            {
                Point p = GridControl.PointToScreen(((VCrkScrollBar)sender).Location);
                p.Y = Control.MousePosition.Y;
                MyShowHint(p, DevExpress.Utils.ToolTipLocation.TopLeft);
            }
        }

        string GetToolTipText()
        {
            return string.Format("{0} - {1}", TopRowIndex, TopRowIndex + ViewInfo.RowsLoadInfo.ResultRowCount - 1);
        }

        protected virtual void MyShowHint(Point position, ToolTipLocation location)
        {
            if (GridControl == null) return;
            ToolTipControllerShowEventArgs tool = GridControl.ToolTipController.CreateShowArgs();
            tool.ToolTip = GetToolTipText();
            tool.SelectedObject = this;
            tool.SelectedControl = GridControl;
            tool.AutoHide = false;
            tool.ToolTipLocation = location;
            GridControl.ToolTipController.ShowHint(tool, position);
        }
	}
}