export class PagedList<T> {
  pageNumber?: number;
  pageSize?: number;
  totalCount?: number;
  lastPage?: number;
  items?: T[];

  constructor({
    pageNumber,
    pageSize,
    totalCount,
    lastPage,
    items,
  }: PagedList<T> = {}) {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
    this.totalCount = totalCount;
    this.lastPage = lastPage;
    this.items = items;
  }

  static fromJson<T>(
    data: any,
    itemConstructor: { fromJson: (json: any) => T }
  ): PagedList<T> {
    return new PagedList<T>({
      pageNumber: data["pageNumber"],
      pageSize: data["pageSize"],
      items: data["items"].map((item: any) => itemConstructor.fromJson(item)),
      lastPage: data["lastPage"],
      totalCount: data["totalCount"],
    });
  }
}
