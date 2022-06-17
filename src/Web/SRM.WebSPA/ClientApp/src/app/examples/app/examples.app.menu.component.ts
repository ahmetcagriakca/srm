import {Component, Input, OnInit, AfterViewInit, ViewChild} from '@angular/core';
import {trigger, state, style, transition, animate} from '@angular/animations';
import {Location} from '@angular/common';
import {MenuItem, ScrollPanel} from 'primeng/primeng';
import { ExamplesAppLayoutComponent } from 'examples/layouts';

@Component({
    selector: 'examples-app-menu',
    templateUrl: './examples.app.menu.component.html'
})
export class ExamplesAppMenuComponent implements OnInit, AfterViewInit {

    @Input() reset: boolean;

    model: any[];

    @ViewChild('layoutMenuScroller') layoutMenuScrollerViewChild: ScrollPanel;

    constructor(public app: ExamplesAppLayoutComponent) {}

    ngOnInit() {
        this.model = [
            {label: 'Dashboard', icon: 'fa fa-fw fa-home', routerLink: ['/examples/']},
            {
                label: 'Menu', icon: 'fa fa-fw fa-bars', badge: 4, badgeStyleClass: 'green-badge',
                items: [
                    {label: 'Horizontal', icon: 'fa fa-fw fa-bars', command: event => this.app.menuMode = 'horizontal' },
                    {label: 'Static', icon: 'fa fa-fw fa-bars', command: event => this.app.menuMode = 'static' },
                    {label: 'Overlay', icon: 'fa fa-fw fa-bars', command: event => this.app.menuMode = 'overlay' },
                    {label: 'Popup', icon: 'fa fa-fw fa-bars', command: event => this.app.menuMode = 'popup' }
                ]
            },
            {
                label: 'Layout Colors', icon: 'fa fa-fw fa-magic',
                items: [
                    {
                        label: 'Flat',
                        icon: 'fa fa-fw fa-circle',
                        badge: 7, badgeStyleClass: 'blue-badge',
                        items: [
                            {label: 'Dark', icon: 'fa fa-fw fa-paint-brush',
                              command: (event) => {this.changeLayout('dark'); }},
                            {label: 'Turquoise', icon: 'fa fa-fw fa-paint-brush',
                              command: (event) => {this.changeLayout('turquoise'); }},
                            {label: 'Green', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('green'); }},
                            {label: 'Blue', icon: 'fa fa-fw fa-paint-brush',
                              command: (event) => {this.changeLayout('blue'); }},
                            {label: 'Rose', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('rose'); }},
                            {label: 'Teal', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('teal'); }},
                            {label: 'Blue Grey', icon: 'fa fa-fw fa-paint-brush',
                              command: (event) => {this.changeLayout('bluegrey'); }}
                        ]
                    },
                    {
                        label: 'Special',
                        icon: 'fa fa-fw fa-fire',
                        badge: 8, badgeStyleClass: 'blue-badge',
                        items: [
                            {label: 'Cosmic', icon: 'fa fa-fw fa-paint-brush',
                              command: (event) => {this.changeLayout('cosmic'); }},
                            {label: 'Lawrencium', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('lawrencium'); }},
                            {label: 'Couple', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('couple'); }},
                            {label: 'Stellar', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('stellar'); }},
                            {label: 'Beach', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('beach'); }},
                            {label: 'Flow', icon: 'fa fa-fw fa-paint-brush',
                              command: (event) => {this.changeLayout('flow'); }},
                            {label: 'Fly', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('fly'); }},
                            {label: 'Nepal', icon: 'fa fa-fw fa-paint-brush', command: (event) => {this.changeLayout('nepal'); }}
                        ]
                    }
                ]
            },
            {
                label: 'Themes', icon: 'fa fa-fw fa-paint-brush', badge: 7, badgeStyleClass: 'amber-badge',
                items: [
                    {label: 'Green Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('green'); }},
                    {label: 'Teal Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('teal'); }},
                    {label: 'Blue Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('blue'); }},
                    {label: 'Amber Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('amber'); }},
                    {label: 'Purple Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('purple'); }},
                    {label: 'Turquoise Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('turquoise'); }},
                    {label: 'Blue Grey Theme', icon: 'fa fa-fw fa-diamond', command: (event) => {this.changeTheme('bluegrey'); }},
                ]
            },
            {
                label: 'Components', icon: 'fa fa-fw fa-sitemap',
                items: [
                    {label: 'Sample Page', icon: 'fa fa-fw fa-columns', routerLink: ['/examples/sample']},
                    {label: 'Forms', icon: 'fa fa-fw fa-code', routerLink: ['/examples/forms']},
                    {label: 'Data', icon: 'fa fa-fw fa-table', routerLink: ['/examples/data']},
                    {label: 'Panels', icon: 'fa fa-fw fa-list-alt', routerLink: ['/examples/panels']},
                    {label: 'Overlays', icon: 'fa fa-fw fa-square', routerLink: ['/examples/overlays']},
                    {label: 'Menus', icon: 'fa fa-fw fa-minus-square-o', routerLink: ['/examples/menus']},
                    {label: 'Messages', icon: 'fa fa-fw fa-circle-o-notch', routerLink: ['/examples/messages']},
                    {label: 'Charts', icon: 'fa fa-fw fa-area-chart', routerLink: ['/examples/charts']},
                    {label: 'File', icon: 'fa fa-fw fa-arrow-circle-o-up', routerLink: ['/examples/file']},
                    {label: 'Misc', icon: 'fa fa-fw fa-user-secret', routerLink: ['/examples/misc']}
                ]
            },
            {
                label: 'Template Pages', icon: 'fa fa-fw fa-life-saver',
                items: [
                    {label: 'Empty Page', icon: 'fa fa-fw fa-square-o', routerLink: ['/examples/empty']},
                    {label: 'Landing Page', icon: 'fa fa-fw fa-globe', url: 'assets/pages/landing.html', target: '_blank'},
                    {label: 'Login Page', icon: 'fa fa-fw fa-sign-in', url: 'assets/pages/login.html', target: '_blank'},
                    {label: 'Error Page', icon: 'fa fa-fw fa-exclamation-circle', url: 'assets/pages/error.html', target: '_blank'},
                    {label: '404 Page', icon: 'fa fa-fw fa-times', url: 'assets/pages/404.html', target: '_blank'},
                    {label: 'Access Denied Page', icon: 'fa fa-fw fa-exclamation-triangle',
                      url: 'assets/pages/access.html', target: '_blank'}
                ]
            },
            {
                label: 'Menu Hierarchy', icon: 'fa fa-fw fa-gg',
                items: [
                    {
                        label: 'Submenu 1', icon: 'fa fa-fw fa-sign-in',
                        items: [
                            {
                                label: 'Submenu 1.1', icon: 'fa fa-fw fa-sign-in',
                                items: [
                                    {label: 'Submenu 1.1.1', icon: 'fa fa-fw fa-sign-in'},
                                    {label: 'Submenu 1.1.2', icon: 'fa fa-fw fa-sign-in'},
                                    {label: 'Submenu 1.1.3', icon: 'fa fa-fw fa-sign-in'},
                                ]
                            },
                            {
                                label: 'Submenu 1.2', icon: 'fa fa-fw fa-sign-in',
                                items: [
                                    {label: 'Submenu 1.2.1', icon: 'fa fa-fw fa-sign-in'},
                                    {label: 'Submenu 1.2.2', icon: 'fa fa-fw fa-sign-in'}
                                ]
                            },
                        ]
                    },
                    {
                        label: 'Submenu 2', icon: 'fa fa-fw fa-sign-in',
                        items: [
                            {
                                label: 'Submenu 2.1', icon: 'fa fa-fw fa-sign-in',
                                items: [
                                    {label: 'Submenu 2.1.1', icon: 'fa fa-fw fa-sign-in'},
                                    {label: 'Submenu 2.1.2', icon: 'fa fa-fw fa-sign-in'},
                                    {label: 'Submenu 2.1.3', icon: 'fa fa-fw fa-sign-in'},
                                ]
                            },
                            {
                                label: 'Submenu 2.2', icon: 'fa fa-fw fa-sign-in',
                                items: [
                                    {label: 'Submenu 2.2.1', icon: 'fa fa-fw fa-sign-in'},
                                    {label: 'Submenu 2.2.2', icon: 'fa fa-fw fa-sign-in'}
                                ]
                            },
                        ]
                    }
                ]
            },
            {
                label: 'Documentation', icon: 'fa fa-fw fa-book',
                items: [
                    {label: 'Setup', icon: 'fa fa-fw fa-gear', routerLink: ['/examples/documentation']},
                    {label: 'Utils', icon: 'fa fa-fw fa-wrench', routerLink: ['/examples/utils']}
                ]
            }
        ];
    }

    ngAfterViewInit() {
        setTimeout(() => {this.layoutMenuScrollerViewChild.moveBar(); }, 100);
    }

    changeTheme(theme) {
        const themeLink: HTMLLinkElement = <HTMLLinkElement> document.getElementById('theme-css');
        themeLink.href = 'assets/theme/theme-' + theme + '.css';
    }
    changeLayout(layout) {
        const layoutLink: HTMLLinkElement = <HTMLLinkElement> document.getElementById('layout-css');
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
    selector: '[examples-app-submenu]',
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
                <ul examples-app-submenu [item]="child" *ngIf="child.items" [visible]="isActive(i)" [reset]="reset" [parentActive]="isActive(i)"
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
export class ExamplesAppSubMenuComponent {

    @Input() item: MenuItem;

    @Input() root: boolean;

    @Input() visible: boolean;

    _reset: boolean;

    _parentActive: boolean;

    activeIndex: number;

    hover: boolean;

    constructor(public app: ExamplesAppLayoutComponent, public location: Location, public appMenu: ExamplesAppMenuComponent) {}

    itemClick(event: Event, item: MenuItem, index: number) {
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
            item.command({originalEvent: event, item: item});
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
            if (this.app.isMobile() || this.app.menuMode === 'overlay' || this.app.menuMode === 'popup') {
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
