import ReactPaginate from "react-paginate";

export default function PaginationComponent({
  onPageChange,
  currentPage,
  pageCount,
}: {
  onPageChange: (page: number) => void;
  currentPage: number;
  pageCount: number;
}) {
  return (
    <div>
      <ReactPaginate
        breakLabel="..."
        nextLabel={"Next"}
        onPageChange={(item) => {
          onPageChange(item.selected + 1);
        }}
        forcePage={currentPage - 1}
        pageRangeDisplayed={5}
        pageCount={pageCount}
        previousLabel={"Previous"}
        renderOnZeroPageCount={null}
        className="pagination justify-content-center"
        pageClassName="page-item"
        pageLinkClassName="page-link"
        previousClassName="page-item"
        nextClassName="page-item"
        previousLinkClassName="page-link"
        nextLinkClassName="page-link"
        breakClassName="page-item"
        breakLinkClassName="page-link"
        activeClassName="active"
      />
    </div>
  );
}
