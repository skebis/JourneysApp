import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  {
    provide: 'BASE_URL', useFactory: getBaseUrl, deps: []
  },
  {
    provide: 'BASE_JOURNEYS_API_URL', useFactory: () => "api/Journeys", deps: []
  },
  {
    provide: 'BASE_STATIONS_API_URL', useFactory: () => "api/Stations", deps: []
  },
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
