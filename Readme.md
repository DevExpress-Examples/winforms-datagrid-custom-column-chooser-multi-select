<!-- default file list -->
*Files to look at*:

* [GridControlOverride.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/GridControlOverride.cs) (VB: [GridControlOverride.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/GridControlOverride.vb))
* [GridInfoRegistratorOverride.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/GridInfoRegistratorOverride.cs) (VB: [GridInfoRegistratorOverride.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/GridInfoRegistratorOverride.vb))
* [GridViewOverride.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/GridViewOverride.cs) (VB: [GridViewOverride.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/GridViewOverride.vb))
* [MultiSelectColumnCustomizationListBox.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/MultiSelectColumnCustomizationListBox.cs) (VB: [MultiSelectColumnCustomizationListBox.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/MultiSelectColumnCustomizationListBox.vb))
* [MultiSelectCustomizationForm.cs](./CS/MultiSelectColumnCustomization/CustomGridControl/MultiSelectCustomizationForm.cs) (VB: [MultiSelectCustomizationForm.vb](./VB/MultiSelectColumnCustomization/CustomGridControl/MultiSelectCustomizationForm.vb))
* [Form1.cs](./CS/MultiSelectColumnCustomization/Form1.cs) (VB: [Form1.vb](./VB/MultiSelectColumnCustomization/Form1.vb))
* [Program.cs](./CS/MultiSelectColumnCustomization/Program.cs) (VB: [Program.vb](./VB/MultiSelectColumnCustomization/Program.vb))
<!-- default file list end -->
# How to create custom column chooser allowing column multi select


<p>Imagine that you have dozens of columns and you need to add them to your grid view from a column chooser. It's very inconvenient to drag and drop them one by one and it would be great to have a possibility of selecting them at once and sending to the grid view altogether. This example demonstrates what to do with the column chooser to achieve this behavior. Here we created a ColumnCustomizationListBox descendant with the capability to draw a check box across every column in the column chooser's list box. Also we have added a button to the bottom of column chooser. So now you can check columns you need and send them to the grid view by pressing the button.</p>

<br/>


