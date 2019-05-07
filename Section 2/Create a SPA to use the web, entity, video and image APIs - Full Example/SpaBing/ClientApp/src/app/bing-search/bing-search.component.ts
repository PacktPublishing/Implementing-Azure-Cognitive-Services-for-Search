import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bing-search',
  templateUrl: './bing-search.component.html'
})
export class BingSearchComponent {
  public websearchresults: WebSearchResult[];
  public imagesearchresults: ImageSearchResult[];
  public videosearchresults: VideoSearchResult[];
  public entitysearchresults: EntitySearchResult[];

  public searchQuery: string;
  public market: string;
  public freshness: string;

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.market = 'en-US';
  }

  public clearAll() {
    this.websearchresults = null;
    this.imagesearchresults = null;
    this.videosearchresults = null;
    this.entitysearchresults = null;
  }

  public websearch() {
    this.clearAll();
    this.http.post<WebSearchResult[]>(this.baseUrl + 'api/BingSearch/WebSearch', { query: this.searchQuery, market: this.market, freshness: this.freshness }).subscribe(result => {
      this.websearchresults = result;
    }, error => console.error(error));
  }

  public imagesearch() {
    this.clearAll();
    this.http.post<ImageSearchResult[]>(this.baseUrl + 'api/BingSearch/ImageSearch', { query: this.searchQuery, market: this.market, freshness: this.freshness }).subscribe(result => {
      this.imagesearchresults = result;
    }, error => console.error(error));
  }

  public videosearch() {
    this.clearAll();
    this.http.post<VideoSearchResult[]>(this.baseUrl + 'api/BingSearch/VideoSearch', { query: this.searchQuery, market: this.market, freshness: this.freshness }).subscribe(result => {
      this.videosearchresults = result;
    }, error => console.error(error));
  }

  public entitysearch() {
    this.clearAll();
    this.http.post<EntitySearchResult[]>(this.baseUrl + 'api/BingSearch/EntitySearch', { query: this.searchQuery, market: this.market }).subscribe(result => {
      this.entitysearchresults = result;
    }, error => console.error(error));
  }
}

interface WebSearchResult {
  name: string;
  displayUrl: string;
  snippet: string;
}

interface ImageSearchResult {
  name: string;
  contentUrl: string;
  hostPageUrl: string;
  thumbnailUrl: string;
}

interface EntitySearchResult {
  name: string;
  description: string;
  url: string;
  types: string;
}

interface VideoSearchResult {
  name: string;
  description: string;
  contentUrl: string;
  thumbnailUrl: string;
}
