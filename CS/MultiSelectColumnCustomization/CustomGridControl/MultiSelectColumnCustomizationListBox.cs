using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.Customization;

namespace MultiSelectColumnCustomization
{
	public class MultiSelectColumnCustomizationListBox : ColumnCustomizationListBox
	{
		private int pushedIndex = -1;
		List<object> checkedItems = new List<object>();
		object focusedItem = null;

		public MultiSelectColumnCustomizationListBox(CustomizationForm form)
			: base(form)
		{
		}

		protected Rectangle CalcCheckBoxRect(GraphicsCache cache, Rectangle bounds)
		{
			RepositoryItemCheckEdit checkBox = new RepositoryItemCheckEdit();
			CheckEditViewInfo viewInfo = (CheckEditViewInfo)checkBox.CreateViewInfo();
			viewInfo.Bounds = bounds;
			viewInfo.CalcViewInfo(cache.Graphics);
			ControlGraphicsInfoArgs args = new ControlGraphicsInfoArgs(viewInfo, cache, bounds);
			CheckObjectInfoArgs ci = ((CheckEditViewInfo)args.ViewInfo).CheckInfo;

			Rectangle retValue = new Rectangle(bounds.X, ci.GlyphRect.Y, ci.GlyphRect.Width, ci.GlyphRect.Height);
			return retValue;
		}

		protected override void DrawItemObject(GraphicsCache cache, int index, Rectangle bounds, DrawItemState itemState)
		{
			Rectangle checkBoxRect = CalcCheckBoxRect(cache, bounds);
			bounds.X += checkBoxRect.Width;
			bounds.Width -= checkBoxRect.Width;

			base.DrawItemObject(cache, index, bounds, itemState);

			ButtonState checkState = ButtonState.Normal;

			if ( index == pushedIndex )
				checkState = ButtonState.Pushed;

			if ( checkedItems.Contains(Items[index]) )
				checkState = ButtonState.Checked;

			cache.Paint.DrawCheckBox(cache.Graphics, checkBoxRect, checkState);
			if ( focusedItem != null && Items.IndexOf(focusedItem) == index )
				cache.Paint.DrawFocusRectangle(cache.Graphics, checkBoxRect, CustomizationForm.ForeColor, CustomizationForm.BackColor);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Point mousePoint = new Point(e.X, e.Y);
			object pointedItem = ItemByPoint(new Point(e.X, e.Y));
			int itemIndex = Items.IndexOf(pointedItem);
			Rectangle itemRect = GetItemRectangle(itemIndex);
			Rectangle checkBoxRect = CalcCheckBoxRect(new GraphicsCache(this.CreateGraphics()), itemRect);

			if ( checkBoxRect.Contains(mousePoint) && e.Button == MouseButtons.Left )
			{
				pushedIndex = itemIndex;
				this.InvalidateObject(pointedItem);

				return;
			} else if ( !checkBoxRect.Contains(mousePoint) )
				base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			Point mousePoint = new Point(e.X, e.Y);
			object pointedItem = ItemByPoint(new Point(e.X, e.Y));
			int itemIndex = Items.IndexOf(pointedItem);
			Rectangle itemRect = GetItemRectangle(itemIndex);
			Rectangle checkBoxRect = CalcCheckBoxRect(new GraphicsCache(this.CreateGraphics()), itemRect);

			if ( checkBoxRect.Contains(mousePoint) && e.Button == MouseButtons.Left )
			{
				if ( ModifierKeys == Keys.Shift )
				{
					int startIndex = Items.IndexOf(focusedItem);
					int endIndex = Items.IndexOf(pointedItem);
					bool check = !checkedItems.Contains(pointedItem);

					if ( endIndex >= startIndex )
						for ( int i = startIndex; i <= endIndex; i++ )
						{
							if ( check && !checkedItems.Contains(Items[i]) )
								checkedItems.Add(Items[i]);
							else if ( !check && checkedItems.Contains(Items[i]) )
								checkedItems.Remove(Items[i]);
						} else
						for ( int i = endIndex; i < startIndex; i++ )
						{
							if ( check && !checkedItems.Contains(Items[i]) )
								checkedItems.Add(Items[i]);
							else if ( !check && checkedItems.Contains(Items[i]) )
								checkedItems.Remove(Items[i]);
						}
				} else if ( ModifierKeys == Keys.None )
				{
					if ( checkedItems.Contains(pointedItem) )
						checkedItems.Remove(pointedItem);
					else
						checkedItems.Add(pointedItem);
				}

				focusedItem = pointedItem;
				pushedIndex = -1;
				this.InvalidateObject(pointedItem);

				return;
			} else if ( !checkBoxRect.Contains(mousePoint) )
				base.OnMouseUp(e);
		}

		public List<object> CheckedItems
		{
			get
			{
				return checkedItems;
			}
		}
	}
}
