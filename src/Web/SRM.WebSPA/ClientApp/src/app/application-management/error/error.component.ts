import { Component, OnInit } from '@angular/core';


@Component({
    selector: 'error',
    templateUrl: './error.component.html'
})

export class ErrorComponent implements OnInit {
    model: any = {};
    returnUrl: string;

    constructor(
		) { }

    ngOnInit() {
    }
}
