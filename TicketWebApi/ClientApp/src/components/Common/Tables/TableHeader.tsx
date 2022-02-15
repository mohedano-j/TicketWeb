import * as React from "react";
import "./TableRow.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowUp, faArrowDown } from "@fortawesome/free-solid-svg-icons";
import { IColumn } from "./IColumn";

type propsType<T extends object> = {
  /*
    Columns Configuration
  */
  columns: IColumn<T>[];

  /*
    Sorting Function
  */
  sorting?: Function;
  /*
    Key that the table is being sorted
  */
  sortKey?: keyof T | null;
  /*
    Order that the table is being sorted "asc" or "desc". TODO Move to Enum
  */
  sortOrder?: string;
  /*
  Optional Last Column Label
 */
  lastColumnLabel?: string | null;
};

export const TableHeader = <T extends object>(props: propsType<T>) => {
  const { columns, sorting, sortKey, sortOrder, lastColumnLabel } = props;
  const tableColumns = columns.filter((column: IColumn<T>) => !column.displayInTooltip);

  return (
    <div role="row" className="table-row header">
      {tableColumns.map((column, index) => {
        return (
          <div
            key={column.name.toString() + index}
            role="columnheader"
            title={"Sort by " + column.name}
            id={"header-" + column.name}
            className="table-column"
            onClick={() => {
              sorting ? sorting(column.name, column.lookup, column.sortTransform) : "";
            }}
            style={{ cursor: "pointer", width: column.width }}
          >
            {" "}
            {sorting && sortKey == column.name ? <FontAwesomeIcon icon={sortOrder == "desc" ? faArrowDown : faArrowUp} /> : null}
            {column.title}
          </div>
        );
      })}
      {lastColumnLabel && (
        <div role="columnheader" id={"header-" + lastColumnLabel} className="table-column" style={{ flexGrow: 2 }}>
          {lastColumnLabel}
        </div>
      )}
    </div>
  );
};

export default TableHeader;
