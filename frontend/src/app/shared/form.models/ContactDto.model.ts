export interface ContactDto {
  readonly firstName: string;
  readonly lastName: string;
  readonly email: string;
  readonly password: string;
  readonly category: string;
  readonly subCategory: string;
  readonly phoneNumber: string;
  readonly dateOfBirth: string;
}
export interface ContactCreateDto {
  readonly firstName: string;
  readonly lastName: string;
  readonly email: string;
  readonly password: string;
  readonly categoryId: string;
  readonly subCategoryName: string;
  readonly phoneNumber: string;
  readonly dateOfBirth: string;
}
