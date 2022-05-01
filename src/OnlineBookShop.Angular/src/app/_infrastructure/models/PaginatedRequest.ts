import { MatSort, MatPaginator } from '@angular/material';
import { RequestFilters } from './RequestFilters';

export class PaginatedRequest {
    pageIndex: number;
    pageSize: number;
    columnNameForSorting: string;
    sortDirection: string;
    requestFilters: RequestFilters;

    constructor(paginator: MatPaginator, sort: MatSort, filters: RequestFilters) {
        this.pageIndex = paginator.pageIndex;
        this.pageSize = paginator.pageSize;
        this.columnNameForSorting = sort.active;
        this.sortDirection = sort.direction;
        this.requestFilters = filters;
    }

}
