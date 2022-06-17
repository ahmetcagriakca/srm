import { Component, OnInit } from '@angular/core';


@Component({
    selector: 'unouthorized',
    templateUrl: './unauthorized.component.html'
})

export class UnauthorizedComponent implements OnInit {
    model: any = {};
    returnUrl: string;

    constructor(
		) { }

    ngOnInit() {
    }
}
