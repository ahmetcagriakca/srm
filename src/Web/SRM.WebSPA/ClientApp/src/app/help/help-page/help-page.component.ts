import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'help-page',
	templateUrl: './help-page.component.html'
})

export class HelpPageComponent implements OnInit {

	ngOnInit() {
	}
}
