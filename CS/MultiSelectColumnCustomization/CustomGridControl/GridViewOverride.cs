using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Customization;

namespace MultiSelectColumnCustomization
{
	public class GridViewOverride : GridView
	{
		public GridViewOverride(GridControl ownerGrid)
			: base(ownerGrid)
		{
		}
		public GridViewOverride()
		{
		}

		protected override CustomizationForm CreateCustomizationForm()
		{
			return new MultiSelectCustomizationForm(this);
		}

		protected override string ViewName
		{
			get
			{
				return "GridViewOverride";
			}
		}
	}
}