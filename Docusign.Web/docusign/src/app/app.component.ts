import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { DataService } from './_services/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'docusign';
  templatesDisplayedColumns: string[] = ['id', 'name', 'created', 'owner', 'use'];

  templates$: Observable<any[]>;
  envelopes$: Observable<any[]>;

  constructor(private dataService: DataService) {
    this.templates$ = dataService.listTemplates();
  }


}
