import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const passwordConfirmValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const password: string = control.get('password')?.value || '';
  const confirmPassword: string = control.get('confirmPassword')?.value || '';

  // console.log(password);
  // console.log(confirmPassword);

  if (!password || !confirmPassword || password === confirmPassword) {
    return null;
  }

  return { passwordMismatch: true };
};
