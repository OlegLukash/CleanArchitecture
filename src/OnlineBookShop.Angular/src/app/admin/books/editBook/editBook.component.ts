import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from 'src/app/_services/book.service';
import { Book } from 'src/app/_models/Books/Book';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PublisherService } from 'src/app/_services/publisher.service';
import { Publisher } from 'src/app/_models/Publishers/Publisher';
import * as _ from 'lodash';

@Component({
  selector: 'app-editBook',
  templateUrl: './editBook.component.html',
  styleUrls: ['./editBook.component.css']
})
export class EditBookComponent implements OnInit {

  public pageTitle: string;
  public publishers: Publisher[];
  public bookForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private bookService: BookService,
    private publisherService: PublisherService
  ) { }

  ngOnInit() {
    let objectId: number;
    this.route.params.subscribe(params => {
      objectId = +params['id'];
      if (objectId === 0) {
        this.pageTitle = 'Add Book:';
      } else {
        this.getBook(objectId);
        this.pageTitle = 'Edit Book:';
      }
    });

    this.bookForm = this.formBuilder.group({
      id: [objectId, [Validators.required]],
      title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
      description: ['', Validators.minLength(5)],
      publishedOn: ['', [Validators.required]],
      publisherId: ['', [Validators.required]],
      price: ['', [Validators.required]]
    });

    this.getAllPublishers();
  }

  getAllPublishers(): void {
    this.publisherService.getAllPublishers().subscribe((publishers: Publisher[]) => {
      this.publishers = _.orderBy(publishers, 'name');
    });
  }

  getBook(id: number): void {
    this.bookService.getBook(id).subscribe((book: Book) => {
      this.bookForm.patchValue({
        ...book
      });
    });
  }

  saveBook(): void {
    if (this.bookForm.dirty && this.bookForm.valid) {
       const bookToSave: Book = {
         ...this.bookForm.value
       };

       this.bookService.saveBook(bookToSave).subscribe(
         () => this.onSaveComplete()
       );
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.bookForm.reset();
    this.router.navigate(['/admin/books']);
  }

}
