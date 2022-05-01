export interface PagedResult<T> {
    pageIndex: number;
    pageSize: number;
    total: number;
    items: T [];
}