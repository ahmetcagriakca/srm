import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

// @Injectable()
// export class IconService {

// 	constructor(private http: HttpClient) { }

// 	icons: any[];
// 	selectedIcon: any;
// 	apiUrl = "assets/data/icons.json";

// 	getIcons() {
// 		return this.http.get(this.apiUrl).pipe(map((response: any) => {
// 			this.icons = response.icons;
// 			return this.icons;
// 		}));
// 	}

// 	getIcon(id: any) {
// 		if (this.icons) {
// 			this.selectedIcon = this.icons.find(x => x.properties.id === id) as Object;
// 			return this.selectedIcon;
// 		}
// 	}

// 	getIconByName(name: any) {
// 		if (this.icons) {
// 			this.selectedIcon = this.icons.find(x => x.properties.name === name) as Object;
// 			return this.selectedIcon;
// 		}
// 	}
// }


@Injectable()
export class IconService {

	constructor(private http: HttpClient) { }

	icons: any[];
	selectedIcon: any;
	apiUrl = "assets/data/fa-icons.json";

	getIcons() {
		return this.http.get(this.apiUrl).pipe(map((response: any) => {
			this.icons=[];
			for (let index = 0; index < response.icons.length; index++) {
				const element = response.icons[index];
				this.icons.push({name:element,id:element})
			}
			return this.icons;
		}));
	}

	getIcon(id: any) {
		if (this.icons) {
			this.selectedIcon = this.icons.find(x => x.id === id) as Object;
			return this.selectedIcon;
		}
	}

	getIconByName(name: any) {
		if (this.icons) {
			this.selectedIcon = this.icons.find(x => x.name === name) as Object;
			return this.selectedIcon;
		}
	}
}