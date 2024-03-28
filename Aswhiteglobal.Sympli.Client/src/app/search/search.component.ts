import { Component, OnInit, inject } from '@angular/core';

import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SeoService } from '../services/seo.service';
import { SEOResult } from '../../models/seo-result.model';
import { SearchEngineEnum } from '../../models/search-engine.enum';
import { QuerySEO } from '../../models/query-seo.model';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit {

  constructor(private fb: FormBuilder, private service: SeoService) {
  }

  public form = this.fb.group({
    keyword: ["", [Validators.required, Validators.maxLength(255)]],
    url: ["", [Validators.required, Validators.maxLength(255)]],
    searchEngineTypes: [null, [Validators.required]],
    total: [1, [Validators.required, Validators.min(1), Validators.max(200)]]
  });
  public dataSource: SEOResult[] = [];
  public displayedColumns: string[] = ['searchEngine', 'numberResult',];
  public searchTypeEnum = SearchEngineEnum;
  public searchTypes = [
    {
      name: SearchEngineEnum[SearchEngineEnum.Google],
      value: SearchEngineEnum.Google
    },
    {
      name: SearchEngineEnum[SearchEngineEnum.Bing],
      value: SearchEngineEnum.Bing
    }
  ];

  ngOnInit(): void {
  }







  onSubmit(): void {
    if (this.form.invalid) return;
    const value = this.form.value as unknown as QuerySEO;
    this.dataSource = [];
    this.service.search(value).subscribe(r => {
      this.dataSource = r;
    })
  }
}
