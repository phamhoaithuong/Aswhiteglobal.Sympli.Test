import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { QuerySEO } from '../../models/query-seo.model';
import { environment } from '../../environments/environment.development';
import { SEOResult } from '../../models/seo-result.model';

@Injectable({
  providedIn: 'root'
})
export class SeoService {
  private readonly apiName = 'search';
  constructor(private http: HttpClient) {
  }

  public search(query: QuerySEO) {
    return this.http
      .post<Array<SEOResult>>(`${environment.api}/api/${this.apiName}`, query)
  }
}
