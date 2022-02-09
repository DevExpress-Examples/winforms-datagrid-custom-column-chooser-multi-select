<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128626810/13.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2434)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Data Grid for Windows Forms - How to create custom Column Chooser that supports multiple column selection using check boxes

This example creates a [Grid Control](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.GridControl)'s descendant to implement a custom Column Chooser. This Column Chooser displays check marks for each column. Users can select columns, and then click a custom `Add checked columns to grid` button to display the selected columns in the grid.

<!-- default file list -->
## Files to Look At

* [GridControlOverride.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/GridControlOverride.cs) (VB: [GridControlOverride.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/GridControlOverride.vb))
* [GridInfoRegistratorOverride.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/GridInfoRegistratorOverride.cs) (VB: [GridInfoRegistratorOverride.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/GridInfoRegistratorOverride.vb))
* [GridViewOverride.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/GridViewOverride.cs) (VB: [GridViewOverride.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/GridViewOverride.vb))
* [MultiSelectColumnCustomizationListBox.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/MultiSelectColumnCustomizationListBox.cs) (VB: [MultiSelectColumnCustomizationListBox.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/MultiSelectColumnCustomizationListBox.vb))
* [MultiSelectCustomizationForm.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/MultiSelectCustomizationForm.cs) (VB: [MultiSelectCustomizationForm.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/MultiSelectCustomizationForm.vb))
* [Form1.cs](./CS/MultiSelectColumnCustomization/Form1.cs) (VB: [Form1.vb](./VB/MultiSelectColumnCustomization/Form1.vb))
* [Program.cs](./CS/MultiSelectColumnCustomization/Program.cs) (VB: [Program.vb](./VB/MultiSelectColumnCustomization/Program.vb))
<!-- default file list end -->

## Documentation
- [Grid Control](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.GridControl)

## See Also
- [How to create a GridView descendant](https://github.com/DevExpress-Examples/winforms-grid-create-gridview-descendant)
