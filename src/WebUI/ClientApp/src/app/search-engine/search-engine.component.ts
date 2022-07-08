import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';

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

export class SearchEngineComponent {
  public searchStringFomControl = new FormControl('');
  public searchResults = new DataFileDto();
  

  constructor(
    private searchEngineClient: SearchEngineClient,
  ) { }

  public search() {

    console.log("Start query");

    console.log("Search string is: " + this.searchStringFomControl.value);

    this.searchEngineClient
      .getDataFileUsingSearchEngine(this.searchStringFomControl.value)
      .subscribe(result => {
        this.searchResults = result;
        console.log(result);
      }, error => console.error(error));

    console.log(this.searchResults);

    console.log("End query");

    //Mocking some data to test if UI can display

    //this.searchResults.buildings = [
    //  BuildingDto.fromJS({ 'id': '0cccab2b-bc8d-44c5-b248-8a9ca6d7e899', 'name': 'Head Office', 'shortCut': 'HOFF', 'description': 'Head Office, Feringastraße 4, 85774 Unterföhring' }),
    //  BuildingDto.fromJS({ 'id': '9605186f-7eb4-4f40-967e-2885d9a8b3c4', 'name': 'Produktionsstätte', 'shortCut': 'PROD', 'description': 'Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld' })
    //];

    //this.searchResults.locks = [
    //  LockDto.fromJS({ 'id': '0a1e6f38-6076-4da8-8d6c-87356f975baf', 'serialNumber': 'UID-A89F98F3', 'type': 'cylinder', 'name': 'Gästezimmer 4.OG', 'floor': '4.OG', 'roomNumber': '454', 'description': '' }),
    //  LockDto.fromJS({ 'id': 'e657a28e-d744-4f62-b5d8-a64123c2400f', 'serialNumber': 'UID-C043133A', 'type': 'cylinder', 'name': 'WC Herren 3.OG süd', 'floor': '3.OG', 'roomNumber': 'WC.HL', 'description': '' }),
    //];

    //this.searchResults.groups = [
    //  GroupDto.fromJS({ 'id': '70c886f5-b74e-49f4-8d9f-cf2d6645d4d6', 'name': '<default>', 'description': 'Default group where all transponders' }),
    //  GroupDto.fromJS({ 'id': 'abf2f0f5-ca6c-47c6-a018-dffd3e59d3ea', 'name': 'Vorstand', 'description': 'CEO, CFO, CTO, etc.' }),
    //];

    //this.searchResults.media = [
    //  MediaDto.fromJS({ 'id': '2c2ec98f-137a-4c49-a47a-814ae18a3364', 'serialNumber': 'UID-378D17F6', 'type': 'card', 'owner': 'Gast 1' }),
    //  MediaDto.fromJS({ 'id': '1a3648e2-1641-47f5-b9e3-dec74bb0fe56', 'serialNumber': 'UID-B1196558', 'type': 'card', 'owner': 'Gast 2' }),
    //];

    //console.log(this.searchResults);

  }
}
