import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const pastDate: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const selectedDate = new Date(control.value);
  const now = new Date();

  if (!control.value || selectedDate >= now) {
    return { futureDate: true };
  }

  return null;
};
