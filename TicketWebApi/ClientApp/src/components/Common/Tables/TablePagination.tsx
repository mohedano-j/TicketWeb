import React from "react";
import { Pagination, PaginationItem, PaginationLink } from "reactstrap";

export function TablePagination({
  currentPage,
  totalPages,
  recordsPerPage,
  list,
  onPageClicked,
  setCurrentPage,
  setFilteredList,
  ...props
}: {
  currentPage: number;
  totalPages: number;
  recordsPerPage: number;
  list?: any[];
  onPageClicked?: any;
  setCurrentPage?: Function;
  setFilteredList?: Function;
}) {
  const pageNumbers = [];
  const pageClicked = onPageClicked ? onPageClicked : defaultPageClicked;
  let maxPage = 0;

  if (totalPages != undefined) {
    maxPage = totalPages <= currentPage + 10 ? totalPages : currentPage <= 5 ? 10 : currentPage - 3 + 10;
    let firstPage = currentPage + 10 >= totalPages ? totalPages - 10 : currentPage > 5 ? currentPage - 3 : 1;
    if (firstPage < 1) {
      firstPage = 1;
    }
    for (let i = firstPage; i <= maxPage; i++) {
      pageNumbers.push(i);
    }
  }

  function defaultPageClicked(page: number) {
    if (list && setFilteredList && setCurrentPage) {
      const filteredListAux = list.slice((page - 1) * recordsPerPage, page * recordsPerPage);
      setFilteredList(filteredListAux);
      setCurrentPage(page);
    }
  }
  return (
    <>
      {" "}
      {totalPages != undefined && maxPage > 1 && (
        <Pagination listClassName="mt-sm-2 justify-content-end">
          <PaginationItem disabled={currentPage === 1}>
            <PaginationLink
              onClick={() => {
                pageClicked(1);
              }}
              first
            />
          </PaginationItem>
          <PaginationItem disabled={currentPage === 1}>
            <PaginationLink
              onClick={() => {
                pageClicked(currentPage - 1);
              }}
              previous
            />
          </PaginationItem>
          {pageNumbers.map((page) => (
            <PaginationItem className={currentPage == page ? "active" : ""} key={page}>
              <PaginationLink
                onClick={() => {
                  pageClicked(page);
                }}
              >
                {page}
              </PaginationLink>
            </PaginationItem>
          ))}
          <PaginationItem disabled={currentPage === totalPages}>
            <PaginationLink
              onClick={() => {
                pageClicked(currentPage + 1);
              }}
              next
            />
          </PaginationItem>
          <PaginationItem disabled={currentPage === totalPages}>
            <PaginationLink
              onClick={() => {
                pageClicked(totalPages);
              }}
              last
            />
          </PaginationItem>
        </Pagination>
      )}
    </>
  );
}

export default TablePagination;
