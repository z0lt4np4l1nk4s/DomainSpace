export class BaseFilter {
  query?: string;
  pageNumber?: number;
  pageSize?: number;
  orderBy?: string;

  constructor({ query, pageNumber, pageSize, orderBy }: BaseFilter) {
    this.query = query;
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
    this.orderBy = orderBy;
  }
}
