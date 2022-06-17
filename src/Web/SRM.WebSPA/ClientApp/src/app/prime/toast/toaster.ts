import { NgModule, Component, Input, Output, OnInit, AfterViewInit, AfterContentInit, OnDestroy, ElementRef, ViewChild, EventEmitter, ContentChildren, QueryList, TemplateRef, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { trigger, state, style, transition, animate, query, animateChild } from '@angular/animations';
import { DomHandler, Message, PrimeTemplate, SharedModule } from 'primeng/primeng';
import { NotificationService } from 'prime/message/notification.service';


@Component({
	selector: 'p-toasterItem',
	template: `
        <div #container class="ui-toast-message ui-shadow" [@messageState]="'visible'"
            [ngClass]="{'ui-toast-message-info': message.severity == 'info','ui-toast-message-warn': message.severity == 'warn',
                'ui-toast-message-error': message.severity == 'error','ui-toast-message-success': message.severity == 'success'}"
                (mouseenter)="onMouseEnter()" (mouseleave)="onMouseLeave()">
            <div class="ui-toast-message-content">
                <a href="#" class="ui-toast-close-icon pi pi-times" (click)="onCloseIconClick($event)" *ngIf="message.closable !== false"></a>
                <ng-container *ngIf="!template">
                    <span class="ui-toast-icon pi"
                        [ngClass]="{'pi-info-circle': message.severity == 'info', 'pi-exclamation-triangle': message.severity == 'warn',
                            'pi-times': message.severity == 'error', 'pi-check' :message.severity == 'success'}"></span>
                    <div class="ui-toast-message-text-content">
                        <div class="ui-toast-summary">{{message.summary}}</div>
                        <div class="ui-toast-detail">{{message.detail}}</div>
                    </div>
                </ng-container>
                <ng-container *ngTemplateOutlet="template; context: {$implicit: message}"></ng-container>
            </div>
        </div>
    `,
	animations: [
		trigger('messageState', [
			state('visible', style({
				transform: 'translateY(0)',
				opacity: 1
			})),
			transition('void => *', [
				style({ transform: 'translateY(100%)', opacity: 0 }),
				animate('300ms ease-out')
			]),
			transition('* => void', [
				animate(('250ms ease-in'), style({
					height: 0,
					opacity: 0,
					transform: 'translateY(-100%)'
				}))
			])
		])
	],
	providers: [DomHandler]
})
export class ToasterItem implements AfterViewInit, OnDestroy {

	@Input() message: Message;

	@Input() index: number;

	@Input() template: TemplateRef<any>;

	@Output() onClose: EventEmitter<any> = new EventEmitter();

	@ViewChild('container') containerViewChild: ElementRef;

	timeout: any;

	ngAfterViewInit() {
		this.initTimeout();
	}

	initTimeout() {
		if (!this.message.sticky) {
			this.timeout = setTimeout(() => {
				this.onClose.emit({
					index: this.index,
					message: this.message
				});
			}, this.message.life || 3000);
		}
	}

	clearTimeout() {
		if (this.timeout) {
			clearTimeout(this.timeout);
			this.timeout = null;
		}
	}

	onMouseEnter() {
		this.clearTimeout();
	}

	onMouseLeave() {
		this.initTimeout();
	}

	onCloseIconClick(event) {
		this.clearTimeout();

		this.onClose.emit({
			index: this.index,
			message: this.message
		});

		event.preventDefault();
	}

	ngOnDestroy() {
		this.clearTimeout();
	}
}
@Component({
	selector: 'p-toaster',
	template: `
        <div #container [ngClass]="{'ui-toast ui-widget': true, 
                'ui-toast-top-right': position === 'top-right',
                'ui-toast-top-left': position === 'top-left',
                'ui-toast-bottom-right': position === 'bottom-right',
                'ui-toast-bottom-left': position === 'bottom-left',
                'ui-toast-top-center': position === 'top-center',
                'ui-toast-bottom-center': position === 'bottom-center',
                'ui-toast-center': position === 'center'}" 
                [ngStyle]="style" [class]="styleClass">
            <p-toasterItem *ngFor="let msg of value; let i=index" [message]="msg" [index]="i" (onClose)="onMessageClose($event)" [template]="template" @toastAnimation></p-toasterItem>
        </div>
    `,
	animations: [
		trigger('toastAnimation', [
			transition(':enter, :leave', [
				query('@*', animateChild())
			])
		])
	],
	providers: [DomHandler]
})
export class Toaster implements OnInit, AfterContentInit, OnDestroy {

	@Input() key: string;

	@Input() autoZIndex: boolean = true;

	@Input() baseZIndex: number = 0;

	@Input() style: any;

	@Input() styleClass: string;

	@Input() position: string = 'bottom-right';

	@Input() modal: boolean;

	@Output() onClose: EventEmitter<any> = new EventEmitter();

	@ViewChild('container') containerViewChild: ElementRef;

	@ContentChildren(PrimeTemplate) templates: QueryList<any>;

	messageSubscription: Subscription;

	clearSubscription: Subscription;

	messages: Message[];

	template: TemplateRef<any>;

	mask: HTMLDivElement;

	_value: Message[];
	timeout: any = 10;
	@Input() life: number = 3000;

	@Input() get value(): Message[] {
		return this._value;
	}

	set value(val: Message[]) {
		this._value = val;
		if (this.containerViewChild && this.containerViewChild.nativeElement) {
			this.handleValueChange();
		}
	}

	handleValueChange() {

		if (this.autoZIndex) {
			this.containerViewChild.nativeElement.style.zIndex = String(this.baseZIndex + (++DomHandler.zindex));
		}
		this.domHandler.fadeIn(this.containerViewChild.nativeElement, 250);

		this.initTimeout();
	}

	initTimeout() {
		if (this.timeout) {
			clearTimeout(this.timeout);
		}
		this.zone.runOutsideAngular(() => {
			this.timeout = setTimeout(() => {
				this.zone.run(() => {
					this.removeAll();
				});
			}, this.life);
		});
	}

	removeAll() {
		if (this.value && this.value.length) {
			this.domHandler.fadeOut(this.containerViewChild.nativeElement, 250);

			setTimeout(() => {
				this.value.forEach((msg, index) => this.onClose.emit({ message: this.value[index] }));
				this.value.splice(0, this.value.length);
			}, 250);
		}
	}

	constructor(public notificationService: NotificationService, public domHandler: DomHandler, private zone: NgZone) { }

	ngOnInit() {

	}
	getMessages() {
		return this.notificationService.messages;
	}
	ngAfterContentInit() {
		this.templates.forEach((item) => {
			switch (item.getType()) {
				case 'message':
					this.template = item.template;
					break;

				default:
					this.template = item.template;
					break;
			}
		});
	}

	ngAfterViewInit() {
		if (this.autoZIndex) {
			this.containerViewChild.nativeElement.style.zIndex = String(this.baseZIndex + (++DomHandler.zindex));
		}
	}

	onMessageClose(event) {
		this.notificationService.messages.splice(event.index, 1);

		if (this.notificationService.messages.length === 0) {
			this.disableModality();
		}

		this.onClose.emit({
			message: event.message
		});
	}

	enableModality() {
		if (!this.mask) {
			this.mask = document.createElement('div');
			this.mask.style.zIndex = String(parseInt(this.containerViewChild.nativeElement.style.zIndex) - 1);
			let maskStyleClass = 'ui-widget-overlay ui-dialog-mask';
			this.domHandler.addMultipleClasses(this.mask, maskStyleClass);
			document.body.appendChild(this.mask);
		}
	}

	disableModality() {
		if (this.mask) {
			document.body.removeChild(this.mask);
			this.mask = null;
		}
	}

	ngOnDestroy() {
		if (this.messageSubscription) {
			this.messageSubscription.unsubscribe();
		}

		if (this.clearSubscription) {
			this.clearSubscription.unsubscribe();
		}

		this.disableModality();
	}
}

@NgModule({
	imports: [CommonModule],
	exports: [Toaster, SharedModule],
	declarations: [Toaster, ToasterItem]
})
export class ToasterModule { }