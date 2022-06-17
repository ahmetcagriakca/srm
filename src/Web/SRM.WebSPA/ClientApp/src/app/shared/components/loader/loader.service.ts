import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { LoaderState } from './loader';

@Injectable()

export class LoaderService {

	public loaderSubject = new Subject<LoaderState>();

	public loaderState = this.loaderSubject.asObservable();

	constructor() { }

	show() {
		this.loaderSubject.next(<LoaderState>{ show: true });
	}

	hide() {
		this.loaderSubject.next(<LoaderState>{ show: false });
	}
}