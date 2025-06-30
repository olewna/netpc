export interface Contact {
  readonly id: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly email: string;
  readonly password: string;
  readonly category: number | string;
  readonly subCategory: string;
  readonly phoneNumber: string;
  readonly dateOfBirth: string;
}
