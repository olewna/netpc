import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Category } from '../utils/enum/category.enum';

export class SubCategoryValidator {
  static conditionalRequired(categoryControlName: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.parent) return null; // formularz nie gotowy

      const category = control.parent.get(categoryControlName)?.value;
      const subCategory = control.value;

      if (
        (category === '3' || category === '1') &&
        (!subCategory || subCategory.trim() === '')
      ) {
        return { subCategoryRequired: true };
      }

      return null;
    };
  }
}
