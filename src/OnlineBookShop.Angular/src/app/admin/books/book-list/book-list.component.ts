import { Component, ViewChild, AfterViewInit } from '@angular/core';

import { BookGridRow } from '../../../_models/Books/BookGridRow';
import { BookService } from '../../../_services/book.service';

import {
  MatDialog,
  MatSnackBar,
  MatPaginator,
  MatTableDataSource,
  MatSort
} from '@angular/material';
import { ConfirmDialogComponent } from 'src/app/admin/shared/confirm-dialog.component';
import { PagedResult } from 'src/app/_infrastructure/models/PagedResult';
import { merge } from 'rxjs';
import { PaginatedRequest } from 'src/app/_infrastructure/models/PaginatedRequest';
import { Filter } from 'src/app/_infrastructure/models/Filter';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { TableColumn } from 'src/app/_infrastructure/models/TableColumn';
import { RequestFilters } from 'src/app/_infrastructure/models/RequestFilters';
import { FilterLogicalOperators } from 'src/app/_infrastructure/models/FilterLogicalOperators';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements AfterViewInit {

  pagedBooks: PagedResult<BookGridRow>;

  tableColumns: TableColumn[] = [
    { name: 'title', index: 'title', displayName: 'Title', useInSearch: true },
    { name: 'publisher', index: 'publisher.name', displayName: 'Publisher', useInSearch: true },
    { name: 'publishedOn', index: 'publishedOn', displayName: 'Published On' },
    { name: 'price', index: 'price', displayName: 'Price' },
    { name: 'id', index: 'id', displayName: 'Id' }
  ];

  displayedColumns: string[];

  searchInput = new FormControl('');
  filterForm: FormGroup;

  requestFilters: RequestFilters;

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false}) sort: MatSort;

  constructor(
    private bookService: BookService,
    private formBuilder: FormBuilder,
    public dialog: MatDialog,
    public snackBar: MatSnackBar
  ) {
    this.displayedColumns = this.tableColumns.map(column => column.name);
    this.filterForm = this.formBuilder.group({
      title: [''],
      publisher: ['']
    });
  }

  ngAfterViewInit() {
    this.loadBooksFromApi();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page).subscribe(() => {
      this.loadBooksFromApi();
    });
  }

  loadBooksFromApi() {
    const paginatedRequest = new PaginatedRequest(this.paginator, this.sort, this.requestFilters);
    this.bookService.getBooksPaged(paginatedRequest)
      .subscribe((pagedBooks: PagedResult<BookGridRow>) => {
        this.pagedBooks = pagedBooks;
      });
  }

  openDialogForDeleting(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { title: 'Dialog', message: 'Are you sure to delete this item?' }
    });
    dialogRef.disableClose = true;

    dialogRef.afterClosed().subscribe(result => {
      if (result === dialogRef.componentInstance.ACTION_CONFIRM) {
        this.bookService.deleteBook(id).subscribe(
          () => {
            this.loadBooksFromApi();

            this.snackBar.open('The item has been deleted successfully.', 'Close', {
              duration: 1500
            });
          }
        );
      }
    });

  }

  applySearch() {
    this.createFiltersFromSearchInput();
    this.loadBooksFromApi();
  }

  resetGrid() {
    this.requestFilters = {filters: [], logicalOperator: FilterLogicalOperators.And};
    this.loadBooksFromApi();
  }

  filterBooksFromForm() {
    this.createFiltersFromForm();
    this.loadBooksFromApi();
  }

  private createFiltersFromForm() {
    if (this.filterForm.value) {
      const filters: Filter[] = [];

      Object.keys(this.filterForm.controls).forEach(key => {
        const controlValue = this.filterForm.controls[key].value;
        if (controlValue) {
          const foundTableColumn = this.tableColumns.find(tableColumn => tableColumn.name === key);
          const filter: Filter = { path : foundTableColumn.index, value : controlValue };
          filters.push(filter);
        }
      });

      this.requestFilters = {
        logicalOperator: FilterLogicalOperators.And,
        filters
      };
    }
  }

  private createFiltersFromSearchInput() {
    const filterValue = this.searchInput.value.trim();
    if (filterValue) {
      const filters: Filter[] = [];
      this.tableColumns.forEach(column => {
        if (column.useInSearch) {
          const filter: Filter = { path : column.index, value : filterValue };
          filters.push(filter);
        }
      });
      this.requestFilters = {
        logicalOperator: FilterLogicalOperators.Or,
        filters
      };
    } else {
      this.resetGrid();
    }
  }

}
