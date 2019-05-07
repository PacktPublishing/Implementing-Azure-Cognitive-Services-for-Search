import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bing-search',
  templateUrl: './bing-search.component.html'
})
export class BingSearchComponent {

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  public websearch() {
    // todo
  }

  public imagesearch() {
    // todo
  }

  public videosearch() {
    // todo
  }

  public entitysearch() {
    // todo
  }
}
