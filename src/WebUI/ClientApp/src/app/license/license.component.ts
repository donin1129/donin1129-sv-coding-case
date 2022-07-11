import { Component } from '@angular/core';

import {
  LicenseClient
} from '../web-api-client-new';

@Component({
  selector: 'app-license-component',
  templateUrl: './license.component.html',
  styleUrls: ['./license.component.scss']
})

export class LicenseComponent {
  public latestLicense = "";
  

  constructor(
    private licenseClient: LicenseClient,
  ) { }

  public getLatestLicense() {

    console.log("Start getting latest license");

    this.licenseClient
      .getLatestLicense()
      .subscribe(result => {
        this.latestLicense = result;
        console.log(result);
      }, error => console.error(error));

    console.log(this.latestLicense);

    console.log("End process");

  }

  public generateNewLicense() {

    console.log("Start generating new license");

    this.licenseClient
      .generateLicense()
      .subscribe(result => {
      }, error => console.error(error));

    console.log("End process");

  }
}
