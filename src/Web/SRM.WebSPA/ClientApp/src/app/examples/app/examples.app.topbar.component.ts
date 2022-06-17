import { Component } from '@angular/core';
import { AuthService } from 'shared/services';
import { ExamplesAppLayoutComponent } from 'examples/layouts';

@Component({
  selector: 'examples-app-topbar',
  templateUrl: './examples.app.topbar.component.html'
})
export class ExamplesAppTopBarComponent {

  constructor(public app: ExamplesAppLayoutComponent, public auth: AuthService) { }

  logout() {
    this.auth.SignOut();
  }
}
