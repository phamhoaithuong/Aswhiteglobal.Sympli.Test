import { SearchEngineEnum } from "./search-engine.enum";

export interface SearchRequest {
  total: number;
  keyword: string;
}

export interface QuerySEO extends SearchRequest {
  uRL: string;
  searchEngineTypes: Array<SearchEngineEnum>;
}
