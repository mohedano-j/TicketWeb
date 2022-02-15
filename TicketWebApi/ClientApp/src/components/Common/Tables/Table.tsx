import * as React from "react";
import { TablePagination } from "..";
import { useState, useEffect } from "react";
import { sortBy } from "../../../utils";
import { TableHeader, TableRow, TableRowEmpty } from ".";
import { IColumn } from "./IColumn";

type propsType<T extends object> = {
  /*
    List of objects to display
  */
  items: T[] | undefined;
  /*
    Function to execute when clicking Edit
  */
  editClicked?: ((value: T) => void) | null;
  /*
    Function to execute when clicking Delete
  */
  deleteClicked?: ((value: T) => void) | null;
  /*
    Function to execute when clicking on the Row
  */
  rowClicked?: ((value: T) => void) | null;
  /*
    Function to apply style to a each row.
  */
  styleRowFunction?: ((value: T) => React.CSSProperties) | null;
  /*
    Column Configuration
  */
  columns: IColumn<T>[];
  /*
    Key to sort by default
  */
  defaultSortKey?: keyof T | null;
  /*
    Order to sort by default. Asc or Desc
  */
  defaultSortOrder?: string | null;
  /*
    Records Per Page if pagination is active
  */
  recordsPerPage?: number;
  /*
    Hide Search Functionality
  */
  hideSearch?: boolean;
  /*
    Remove Pagination
  */
  noPagination?: boolean;
  /*
    Selected Model in table. The row of this model will be highlighted
  */
  selectedItem?: T | null;
  /*
    Primary Key of the Model. This is only required for selectable tables.
  */
  primaryKey?: keyof T;
  /*
    Function to execute when clicking a button define by button name. If null, nothing will happen
  */
  buttonAction?: ((value: T) => void) | null;
  /*
   Optional button name
 */
  buttonName?: string | null;

  /*
  Optional Last Column Label
 */
  lastColumnLabel?: string | null;
};

export const Table = <T extends object>(props: propsType<T>) => {
  const {
    defaultSortKey,
    recordsPerPage,
    items,
    columns,
    editClicked,
    deleteClicked,
    rowClicked,
    buttonAction,
    styleRowFunction,
    buttonName,
    hideSearch,
    noPagination,
    selectedItem,
    primaryKey,
    lastColumnLabel,
    defaultSortOrder,
  } = props;
  const [sortKey, setSortKey] = useState<keyof T | null | undefined>(defaultSortKey);
  const [searchedList, setSearchedList] = useState<any[] | undefined>(undefined);
  const [sortOrder, setSortOrder] = useState<string>(defaultSortOrder ?? "desc");
  const [filteredList, setFilteredList] = useState<any[] | undefined>(undefined);
  const [totalPages, setTotalPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(1);

  useEffect(() => {
    //When list changed refresh list state
    if (items) {
      refreshList(items);
    }
  }, [items]);

  useEffect(() => {
    //When list changed refresh list state
    if (defaultSortKey && defaultSortKey !== sortKey) {
      setSortKey(defaultSortKey);
      sorting(defaultSortKey);
    }
  }, [defaultSortKey]);

  //Refrest List after update/delete/add
  const refreshList = (listAux: T[]) => {
    const column = columns.find((k) => k.name === sortKey);
    const sortedList = sortKey ? sortBy(sortKey, sortOrder, listAux, column?.lookup, column?.sortTransform) : listAux;
    if (noPagination || !recordsPerPage) {
      setFilteredList(sortedList);
      setTotalPages(1);
    } else {
      const filteredListAux = sortedList.slice((currentPage - 1) * recordsPerPage, currentPage * recordsPerPage);
      const totalPagesAux = Math.floor(listAux.length / recordsPerPage) + 1;
      setTotalPages(totalPagesAux);
      setFilteredList(filteredListAux);
    }
  };

  //Sort list when column is click
  const sorting = (key: keyof T, lookup?: Record<string, string>, transform?: (value: any) => string) => {
    let sortOrderAux = sortOrder;
    if (sortKey == key) {
      if (sortOrder == "desc") {
        sortOrderAux = "asc";
      } else {
        sortOrderAux = "desc";
      }
    } else {
      sortOrderAux = "desc";
    }
    if (items) {
      const sortedListAux = sortBy(key, sortOrderAux, items, lookup, transform);
      const filteredListAux = noPagination ? sortedListAux : sortedListAux.slice(0, recordsPerPage);
      const searchedListAux = searchedList ? sortBy(key, sortOrderAux, searchedList) : undefined;
      setSortKey(key);
      setSearchedList(searchedListAux);
      setSortOrder(sortOrderAux);
      setFilteredList(filteredListAux);
      setCurrentPage(1);
    }
  };

  //Filter list when a key is pressed in the Keysearch
  const onSearchKeyPress = (event: any) => {
    const searchTerm = event.target.value;
    if (items != null && searchTerm.length > 2) {
      const searchedList = items.filter((obj) => {
        let returnObj = false;
        Object.keys(obj).forEach((e) => {
          const key = e as keyof typeof obj;
          const column = columns.find((c) => c.name == e);
          const recordValue: any = obj[key];
          /* Convert value before searching */
          let value = "";
          if (column?.lookup && column.lookup[recordValue] !== null) {
            value = column.lookup[recordValue];
          } else if (column?.transform) {
            value = column.transform(recordValue);
          } else {
            value = typeof recordValue === typeof Number() && recordValue != undefined ? recordValue.toString() : recordValue;
          }
          if (value && typeof value === typeof String() && value.toUpperCase().indexOf(searchTerm.toUpperCase()) != -1) {
            returnObj = true;
          }
        });
        if (returnObj) {
          return obj;
        }
      });
      refreshList(searchedList);
    } else {
      if (items) {
        const sortedList = sortBy(sortKey, sortOrder, items);
        refreshList(sortedList);
      }
    }
  };

  return (
    <>
      {filteredList && (
        <>
          <div className="small">
            <div className={noPagination ? "sticky-header" : ""}>
              {!hideSearch && (
                <div className="form-group float-right">
                  <input onChange={onSearchKeyPress} type="text" className="form-control" placeholder="Search by keyword" />
                </div>
              )}
              <TableHeader sorting={sorting} columns={columns} sortKey={sortKey} sortOrder={sortOrder} lastColumnLabel={lastColumnLabel} />
            </div>
            {filteredList.length > 0 ? (
              filteredList.map((item: T, index: number) => {
                let isSelected = false;
                if (primaryKey && selectedItem) {
                  if (typeof item[primaryKey] === "string" || typeof item[primaryKey] === "number") {
                    // key is number or string
                    isSelected = selectedItem !== undefined && item[primaryKey] === selectedItem[primaryKey];
                  } else {
                    // key is a JSON
                    isSelected = selectedItem !== undefined && JSON.stringify(item[primaryKey]) === JSON.stringify(selectedItem[primaryKey]);
                  }
                }
                return (
                  <TableRow
                    deleteButtonClicked={deleteClicked}
                    editButtonClicked={editClicked}
                    rowClicked={rowClicked}
                    id={"row_" + index} //Using index because primary key may contain symbols which do not work with popover
                    key={index}
                    columns={columns}
                    item={item}
                    isSelected={isSelected}
                    buttonAction={buttonAction}
                    buttonName={buttonName}
                    styleRowFunction={styleRowFunction}
                  />
                );
              })
            ) : (
              <TableRowEmpty />
            )}
          </div>
          {recordsPerPage !== undefined && !noPagination && (
            <TablePagination currentPage={currentPage} totalPages={totalPages} recordsPerPage={recordsPerPage} setCurrentPage={setCurrentPage} setFilteredList={setFilteredList} list={items} />
          )}
        </>
      )}
    </>
  );
};
