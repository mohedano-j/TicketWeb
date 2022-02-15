import * as React from "react";
import "./TableRow.scss";
import { UncontrolledPopover, PopoverBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { IColumn } from "./IColumn";

type propsType<T extends object> = {
  /*
    Id of the row
  */
  id?: string;
  /*
    Columns Configuration
  */
  columns: IColumn<T>[];
  /*
    Key of the row
  */
  key: string | number;
  /*
    Item to display
  */
  item?: T;
  /*
    Function to execute when clicking Edit. If null button will not be displayed
  */
  editButtonClicked?: ((value: T) => void) | null;
  /*
    Function to execute when clicking Delete. If null button will not be displayed
  */
  deleteButtonClicked?: ((value: T) => void) | null;
  /*
    Function to execute when clicking the row. If null, nothing will happen
  */
  rowClicked?: ((value: T) => void) | null;
  /*
    Function to execute when clicking a button define by button name. If null, nothing will happen
  */
  buttonAction?: ((value: T) => void) | null;
  /*
    Optional button name
  */
  buttonName?: string | null;
  /*
    Row is selected. Row will be highlighted
  */
  isSelected?: boolean;
  /*
    Function to apply style to a each row.
  */
  styleRowFunction?: ((value: T) => React.CSSProperties) | null;
};

export const TableRow = <T extends object>(props: propsType<T>) => {
  const { id, columns, item, editButtonClicked, deleteButtonClicked, rowClicked, buttonAction, buttonName, isSelected, styleRowFunction } = props;
  const tableColumns = columns.filter((column: IColumn<T>) => !column.displayInTooltip);
  const toolTipColumns = columns.filter((column: IColumn<T>) => column.displayInTooltip);
  const className = isSelected ? "table-row table-primary" : "table-row";

  const onClick =
    rowClicked && item
      ? () => {
          rowClicked(item);
        }
      : undefined;
  const styles = styleRowFunction && item ? styleRowFunction(item) : {};
  return (
    <div role="row" onClick={onClick} className={className} style={{ cursor: onClick ? "pointer" : "auto", ...styles }}>
      {tableColumns.map((column, index) => {
        if (item) {
          const modelKey = column.name as keyof T;
          const value = item[modelKey] as unknown as string | number | object;
          let text = column.lookup !== undefined && typeof value === "string" ? column.lookup[value] : column.transform != undefined ? column.transform(value) : value;
          let tooltipText = null;
          if (column.maxCharDisplay && text.toString().length > column.maxCharDisplay) {
            tooltipText = text;
            text = text.toString().substring(0, column.maxCharDisplay) + "...";
          }
          return (
            <>
              <div
                key={id + "-" + column.name + "-" + index}
                id={id + "-" + column.name + "-" + index}
                role="cell"
                className="table-column"
                style={{ width: column.width, position: "relative", textDecoration: tooltipText ? "underline dotted" : "none" }}
              >
                {text}
              </div>
              {tooltipText && (
                <UncontrolledPopover trigger="hover" placement="top" target={id + "-" + column.name + "-" + index}>
                  <PopoverBody>{tooltipText}</PopoverBody>
                </UncontrolledPopover>
              )}
            </>
          );
        }
      })}
      {toolTipColumns.length > 0 && (
        <div style={{ width: "3%" }} role="cell" className={"table-column"}>
          <div role="button" title="More Info" id={"Tooltip_" + id}>
            <FontAwesomeIcon icon={faInfoCircle} />
          </div>
          <UncontrolledPopover trigger="hover" placement="left" target={"Tooltip_" + id}>
            <PopoverBody>
              {toolTipColumns.map((column: IColumn<T>) => {
                if (item) {
                  const modelKey = column.name as keyof T;
                  const value = item[modelKey] as unknown as string | number | object;
                  const text = column.lookup !== undefined && typeof value === "string" ? column.lookup[value] : column.transform != undefined ? column.transform(value) : value;
                  return (
                    <div key={id + "-" + column.name}>
                      <div className="font-weight-bold"> {column.title + ":"} </div>
                      <div>{text}</div>
                    </div>
                  );
                }
              })}{" "}
            </PopoverBody>
          </UncontrolledPopover>
        </div>
      )}
      <div
        onClick={() => {
          rowClicked && item ? rowClicked(item) : null;
        }}
        className="table-column button-group"
        role="cell"
      >
        {buttonAction && (
          <button
            data-testid="button"
            id={"btn-" + id}
            onClick={() => {
              if (item) buttonAction(item);
            }}
            className={"btn btn-primary btn-sm"}
          >
            {buttonName}
          </button>
        )}{" "}
        {editButtonClicked && (
          <button
            data-testid="editButton"
            id={"editBtn-" + id}
            onClick={() => {
              if (item) editButtonClicked(item);
            }}
            className={"btn btn-primary btn-sm"}
          >
            Edit
          </button>
        )}{" "}
        {deleteButtonClicked && (
          <button
            id={"deleteBtn-" + id}
            onClick={() => {
              if (item) deleteButtonClicked(item);
            }}
            className={"btn btn-danger btn-sm"}
          >
            Delete
          </button>
        )}
      </div>
    </div>
  );
};

export default TableRow;
