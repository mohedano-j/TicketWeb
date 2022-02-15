export interface IColumn<T extends object> {
  /*
    Column key of the model
  */
  name: keyof T;
  /*
    Title to display in the header
  */
  title: string;
  /*
    Width of the column
  */
  width?: string;
  /*
    Default Sorted Order. "asc" or "desc".
    TODO Move this type to an enum with 2 options
  */
  sortOrder?: string;
  /*
    Function to transform the value before sorting the records. This is useful when you need to sort by a property that is not sortable.
   */
  sortTransform?: (value: any) => string;
  /*
    Function to transform the value before being displayed.
   */
  transform?: (value: any) => string;
  /*
    Lookup to convert the value to a string in a distionary.
   */
  lookup?: Record<string, string>;
  /*
    Display the value in a tooltip. This is useful when all the cells do not fit in one row.
  */
  displayInTooltip?: boolean;
  /*
    Max number of chararters to display in column. If item is longer, it will cut it and add a tooltip
  */
  maxCharDisplay?: number;
}
