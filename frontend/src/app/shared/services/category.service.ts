import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/Category.model';
import { SubCategory } from '../models/SubCategory.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private httpClient: HttpClient) {}

  public getAllCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(`api/category`);
  }

  public getAllSubcategories(): Observable<SubCategory[]> {
    return this.httpClient.get<SubCategory[]>(`api/subcategory`);
  }
}
