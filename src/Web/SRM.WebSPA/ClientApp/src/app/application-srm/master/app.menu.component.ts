import { Component, Input, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Location } from '@angular/common';
import { MenuItem, ScrollPanel } from 'primeng/primeng';
import { AppLayoutComponent } from 'application-srm/layouts';
import { Subscription } from 'rxjs';
import { AuthService, StorageService } from 'shared/services';
import { Page } from 'shared/models';

@Component({
	selector: 'app-menu',
	templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit, AfterViewInit {

	@Input() reset: boolean;
	menuKey: string = "MenuStorageKey";

	model: any[];

	items: MenuItem[];
	pageItems: MenuItem[];
	pageItems2: MenuItem[];
	subscription: Subscription;

	@ViewChild('layoutMenuScroller') layoutMenuScrollerViewChild: ScrollPanel;

	constructor(
		public app: AppLayoutComponent,
		public auth: AuthService,
		public storageService: StorageService,
	) { }

	ngOnInit() {
		this.changeLayout('turquoise');
		var menuItemsString = this.storageService.retrieve(this.menuKey);
		if (menuItemsString != null) {
			var menuItems = JSON.parse(menuItemsString);

			if (menuItems != null && menuItems.length > 0) {
				this.items = menuItems;
			}
		}
		// new authentication listen from authentication service
		this.subscription = this.auth.authenticationChallenge$.subscribe(res => {
			if (res == true)
				this.getMenuItems();
		});
		// firs page loading call menu items
		this.getMenuItems();
	}
	private getMenuItems() {
		if (this.auth.Authenticated()) {
			this.auth.getUserPages()
				.subscribe(response => {
					var pages = (<Page[]>response.resultValue);
					this.items = [];
					if (pages && pages.length > 0) {
						for (let page of pages) {
							if (!page.isParent) {
								this.items.push({ label: page.name, icon: page.icon, routerLink: [page.url] });
							}
							else {
								this.pageItems = [];
								for (let subPage of page.children) {
									if (!subPage.hasChild) {
										this.pageItems.push({ label: subPage.name, icon: subPage.icon, routerLink: [page.url + subPage.url] });
									}
									else {
									}
								}
								this.items.push({ label: page.name, icon: page.icon, items: this.pageItems });
							}
						}
						var storageString = JSON.stringify(this.items);
						this.storageService.store(this.menuKey, storageString);
					}
				});
		}
	}

	ngAfterViewInit() {
		setTimeout(() => { this.layoutMenuScrollerViewChild.moveBar(); }, 100);
	}

	changeTheme(theme) {
		const themeLink: HTMLLinkElement = <HTMLLinkElement>document.getElementById('theme-css');
		themeLink.href = 'assets/theme/theme-' + theme + '.css';
	}
	changeLayout(layout) {
		const layoutLink: HTMLLinkElement = <HTMLLinkElement>document.getElementById('layout-css');
		layoutLink.href = 'assets/layout/css/layout-' + layout + '.css';
	}

	onMenuClick() {
		if (!this.app.isHorizontal()) {
			setTimeout(() => {
				this.layoutMenuScrollerViewChild.moveBar();
			}, 450);
		}

		this.app.onMenuClick();
	}
}

@Component({
	/* tslint:disable:component-selector */
	selector: '[app-submenu]',
	/* tslint:enable:component-selector */
	template: `
        <ng-template ngFor let-child let-i="index" [ngForOf]="(root ? item : item.items)">
            <li [ngClass]="{'active-menuitem': isActive(i)}">
                <a [href]="child.url||'#'" (click)="itemClick($event,child,i)" *ngIf="!child.routerLink"
                   [attr.tabindex]="!visible ? '-1' : null" [attr.target]="child.target" (mouseenter)="onMouseEnter(i)">
                    <i [ngClass]="child.icon"></i>
                    <span>{{child.label}}</span>
                    <i class="fa fa-fw fa-angle-down layout-submenu-toggler" *ngIf="child.items"></i>
                    <span class="menuitem-badge" *ngIf="child.badge" [ngClass]="child.badgeStyleClass">{{child.badge}}</span>
                </a>

                <a (click)="itemClick($event,child,i)" *ngIf="child.routerLink"
                    [routerLink]="child.routerLink" routerLinkActive="active-menuitem-routerlink"
                    [routerLinkActiveOptions]="{exact: true}" [attr.tabindex]="!visible ? '-1' : null" [attr.target]="child.target"
                    (mouseenter)="onMouseEnter(i)">
                    <i [ngClass]="child.icon"></i>
                    <span>{{child.label}}</span>
                    <i class="fa fa-fw fa-angle-down" *ngIf="child.items"></i>
                    <span class="menuitem-badge" *ngIf="child.badge" [ngClass]="child.badgeStyleClass">{{child.badge}}</span>
                </a>
                <ul app-submenu [item]="child" *ngIf="child.items" [visible]="isActive(i)" [reset]="reset" [parentActive]="isActive(i)"
                    [@children]="isActive(i) ? 'visible' : 'hidden'"></ul>
            </li>
        </ng-template>
    `,
	animations: [
		trigger('children', [
			state('visible', style({
				height: '*'
			})),
			state('hidden', style({
				height: '0px'
			})),
			transition('visible => hidden', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)')),
			transition('hidden => visible', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
		])
	]
})
export class AppSubMenuComponent {

	@Input() item: MenuItem;

	@Input() root: boolean;

	@Input() visible: boolean;

	_reset: boolean;

	_parentActive: boolean;

	activeIndex: number;

	hover: boolean;

	constructor(public app: AppLayoutComponent, public location: Location, public appMenu: AppMenuComponent) { }

	itemClick(event: Event, item: MenuItem, index: number) {
		if (this.root) {

			this.app.menuHoverActive = !this.app.menuHoverActive;
		}

		// avoid processing disabled items
		if (item.disabled) {
			event.preventDefault();
			return true;
		}

		// activate current item and deactivate active sibling if any
		if (item.routerLink || item.items || item.command || item.url) {
			this.activeIndex = (this.activeIndex === index) ? null : index;
		}

		// execute command
		if (item.command) {
			item.command({ originalEvent: event, item: item });
		}

		// prevent hash change
		if (item.items || (!item.url && !item.routerLink)) {
			setTimeout(() => {
				this.appMenu.layoutMenuScrollerViewChild.moveBar();
			}, 450);
			event.preventDefault();
		}

		// hide menu
		if (!item.items) {
			if (this.app.menuMode === 'horizontal') {
				this.app.resetMenu = true;
			} else {
				this.app.resetMenu = false;
			}
			if (this.app.isMobile() || this.app.menuMode === 'overlay' || this.app.menuMode === 'popup') {
				this.app.menuActive = false;
			}

			this.app.menuHoverActive = false;
		}
	}

	onMouseEnter(index: number) {
		if (this.root && this.app.menuHoverActive && this.app.isHorizontal()
			&& !this.app.isMobile() && !this.app.isTablet()) {
			this.activeIndex = index;
		}
	}

	isActive(index: number): boolean {
		return this.activeIndex === index;
	}

	@Input() get reset(): boolean {
		return this._reset;
	}

	set reset(val: boolean) {
		this._reset = val;

		if (this._reset && (this.app.menuMode === 'horizontal')) {
			this.activeIndex = null;
		}
	}

	@Input() get parentActive(): boolean {
		return this._parentActive;
	}

	set parentActive(val: boolean) {
		this._parentActive = val;

		if (!this._parentActive) {
			this.activeIndex = null;
		}
	}
}
