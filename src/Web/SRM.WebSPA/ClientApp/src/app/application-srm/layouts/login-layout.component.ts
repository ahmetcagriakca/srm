import { Component, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/platform-browser';
import { Message } from 'primeng/primeng';

@Component({
	selector: 'app-login-layout',
	template: `
    <angular-loader></angular-loader>
	<router-outlet></router-outlet>
	<p-toast [style]="{marginTop: '80px', 'z-index':'49999'}" [autoZIndex]="false"></p-toast>
  `,
	styles: []
})
export class LoginLayoutComponent {

	constructor(@Inject(DOCUMENT) private document: Document) { }

	ngOnInit() {
		this.document.body.classList.add('login-body');
	}
}
