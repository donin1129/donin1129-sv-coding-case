import { Component } from '@angular/core';
import {
  SearchEngineClient,
  DataFileDto,
  BuildingDto, GroupDto, LockDto, MediaDto
} from '../web-api-client-new';

@Component({
  selector: 'app-search-engine-component',
  templateUrl: './search-engine.component.html',
  styleUrls: ['./search-engine.component.scss']
})
/*export class SearchEngineComponent {
  public orderedSearchResults: DataFileDto;

  constructor(private client: SearchEngineClient) {
    client.getDataFileUsingSearchEngine("test").then(result => {
      this.orderedSearchResults = result;
    }, error => console.error(error));
  }
}*/
export class SearchEngineComponent {
  public searchString = "";
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
