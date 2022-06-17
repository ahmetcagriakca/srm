import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoaderService } from 'shared/components/loader/loader.service';
// Implementing a Retry-Circuit breaker policy 
// is pending to do for the SPA app
@Injectable()
export class DataService {
	private headers: HttpHeaders;

	constructor(
		private http: HttpClient,
		private loaderService: LoaderService) {
	}


	private setHeaders() {
		this.headers = new HttpHeaders();
		this.headers.append('Content-Type', 'application/json');
		this.headers.append('Accept', 'application/json');
		this.headers.append('Access-Control-Allow-Origin', '*');
	}


	getById<T>(url: string, params?: any): Observable<T> {
		this.showLoader();
		this.setHeaders();
		return this.http.get(url, { headers: this.headers, params: params })
			.map(response => <T>(<T>response))
			.finally(() => {
				this.onEnd();
			});

	}

	get<T>(url: string, params?: any): Observable<T> {
		this.showLoader();
		this.setHeaders();
		return this.http.get(url, { headers: this.headers, params: params })
			.map(response => <T>(<T>response))
			.finally(() => {
				this.onEnd();
			});
	}

	post<T>(url: string, data: any, params?: any): Observable<T> {
		this.showLoader();
		this.setHeaders();
		return this.doPost<T>(url, data, params);
	}


	put<T>(url: string, data: any, params?: any): Observable<T> {
		this.showLoader();
		this.setHeaders();
		return this.doPut<T>(url, data, params);
	}

	delete<T>(url: string, params?: any): Observable<T> {
		this.showLoader();
		this.setHeaders();
		return this.http.delete(url, { headers: this.headers, params: params })
			.map(response => <T>(<T>response))
			.finally(() => {
				this.onEnd();
			});
	}

	private doPost<T>(url: string, data: any, params?: any): Observable<T> {
		return this.http.post(url, data, { headers: this.headers, params: params })
			.map(response => <T>(<T>response))
			.finally(() => {
				this.onEnd();
			});
	}
	private doPut<T>(url: string, data: any, params?: any): Observable<T> {
		return this.http.put(url, data, { headers: this.headers, params: params })
			.map(response => <T>(<T>response))
			.finally(() => {
				this.onEnd();
			});
	}

	private onEnd(): void {
		this.hideLoader();
	}

	private showLoader(): void {
		this.loaderService.show();
	}

	private hideLoader(): void {
		this.loaderService.hide();
	}
}
