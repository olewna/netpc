import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { pastDate } from '../../../shared/directives/past-date.directive';
import { SubCategoryValidator } from '../../../shared/directives/subcategory.directive';
import { ContactService } from '../../../shared/services/contact.service';
import {
  ContactCreateDto,
  ContactDto,
} from '../../../shared/form.models/ContactDto.model';
import { Contact } from '../../../shared/models/Contact.model';
import { HttpErrorResponse } from '@angular/common/http';
import { formatDate } from '../../../shared/utils/formatDate';
import { CategoryService } from '../../../shared/services/category.service';
import { Category } from '../../../shared/models/Category.model';
import { SubCategory } from '../../../shared/models/SubCategory.model';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrl: './contact-form.component.scss',
})
export class ContactFormComponent implements OnInit {
  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private contactService: ContactService,
    private categoryService: CategoryService
  ) {}

  protected id: string = '';
  protected isAddMode: boolean = true;
  protected showPassword: boolean = false;
  protected contactForm!: FormGroup;
  protected categories: Category[] = [];
  protected subcategories: SubCategory[] = [];
  protected maxDate!: string;
  protected errorMsg: string = '';

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id; // jeśli nie ma id to dodajemy, jak jest to edytujemy kontakt z podanym id

    const yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    this.maxDate = yesterday.toISOString().split('T')[0];

    this.categoryService.getAllCategories().subscribe({
      next: (categories: Category[]) => {
        this.categories = categories;
        // console.log(categories);
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        this.errorMsg = err.error
          ? err.error.message
          : 'Błąd pobierania kategorii...';
      },
    });

    this.categoryService.getAllSubcategories().subscribe({
      next: (subcategories: SubCategory[]) => {
        this.subcategories = subcategories;
        // console.log(subcategories);
      },
      error: (err: HttpErrorResponse) => {
        console.error(err);
        this.errorMsg = err.error
          ? err.error.message
          : 'Błąd pobierania podkategorii...';
      },
    });

    this.contactForm = this.fb.group({
      firstname: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.pattern(/^[A-Za-z]+$/),
        ],
      ],
      lastname: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.pattern(/^[A-Za-z]+$/),
        ],
      ],
      email: ['', [Validators.email, Validators.required]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,}$/
          ),
        ],
      ],
      categoryId: ['', [Validators.required]],
      subCategoryName: [
        '',
        [SubCategoryValidator.conditionalRequired('categoryId')],
      ],
      phoneNumber: [
        '',
        [Validators.required, Validators.pattern(/^\+?[0-9]{9,12}$/)],
      ],
      dateOfBirth: ['', [Validators.required, pastDate]],
    });

    this.contactForm.get('categoryId')?.valueChanges.subscribe((value) => {
      const subCategoryControl = this.contactForm.get('subCategoryName');

      subCategoryControl?.reset('');

      subCategoryControl?.updateValueAndValidity();
    }); // resetowanie wartości subcategory, kiedy category zostanie zeminione

    if (!this.isAddMode) {
      this.contactService.getContactById(this.id).subscribe({
        next: (response: Contact) => {
          const {
            firstName,
            lastName,
            dateOfBirth,
            categoryName,
            subCategoryName,
            ...rest
          } = response;

          const catId = this.categories.find((c) => c.name == categoryName)?.id;

          this.contactForm.patchValue({
            firstname: firstName,
            lastname: lastName,
            categoryId: catId?.toString(),
            subCategoryName: subCategoryName,
            dateOfBirth: formatDate(dateOfBirth),
          });
          this.contactForm.patchValue(rest); //wpisanie w inputy reszte istniejących wartości
        },
        error: (err: HttpErrorResponse) => {
          this.errorMsg = err.error
            ? err.error.message
            : 'Nie można pobrać danych tego kontaktu lub taki kontakt nie istnieje';
        },
      });
    }
  }

  public goBack(): void {
    this.location.back();
  }

  public submit(): void {
    if (this.contactForm.valid) {
      // console.log(this.contactForm.value);
      if (this.isAddMode) {
        this.contactService
          .createContact(this.contactForm.value as ContactCreateDto)
          .subscribe({
            next: (response: Contact) => {
              this.errorMsg = 'Pomyślnie utworzono kontakt';
              setTimeout(() => {
                this.errorMsg = '';
              }, 5000);

              this.contactForm.reset();
            },
            error: (err: HttpErrorResponse) => {
              console.error(err);
              this.errorMsg = err.error ? err.error.message : 'Błąd';
              setTimeout(() => {
                this.errorMsg = '';
              }, 5000);
            },
          });
      } else {
        this.contactService
          .updateContact(this.contactForm.value as ContactDto, this.id)
          .subscribe({
            next: (response: Contact) => {
              this.errorMsg = 'Pomyślnie zaaktualizowano kontakt';
              setTimeout(() => {
                this.errorMsg = '';
              }, 5000);

              // this.contactForm.reset();
            },
            error: (err: HttpErrorResponse) => {
              console.error(err);
              this.errorMsg = err.error ? err.error.message : 'Błąd';
              setTimeout(() => {
                this.errorMsg = '';
              }, 5000);
            },
          });
      }
    } else {
      this.errorMsg = 'Wystąpił błąd';
      setTimeout(() => {
        this.errorMsg = '';
      }, 5000);
    }
  }

  public togglePassword(): void {
    this.showPassword = !this.showPassword;
  }
}
