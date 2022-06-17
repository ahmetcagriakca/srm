import { NgModule, Directive, ElementRef, AfterViewInit, OnDestroy, HostBinding, HostListener, Input, NgZone, Renderer, ViewContainerRef, SimpleChanges, OnChanges, AfterViewChecked } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DomHandler } from 'primeng/primeng';
import { AbstractControlDirective, AbstractControl, NG_VALIDATORS, Validators, ValidationErrors, FormControl } from '@angular/forms';

@Directive({
	selector: '[pValidationTooltip]',
	providers: [DomHandler],
	host: { '(ngModelChange)': 'onInputChange($event)' }
})
export class ValidationTooltip implements AfterViewInit, AfterViewChecked, OnDestroy {

	@Input() tooltipPosition: string = 'bottom';

	@Input() tooltipEvent: string = 'hover';

	@Input() appendTo: any = 'body';

	@Input() positionStyle: string;

	@Input() tooltipStyleClass: string;

	@Input() tooltipZIndex: string = 'auto';

	@Input("tooltipDisabled") disabled: boolean;

	@Input() escape: boolean = true;

	@Input() showDelay: number;

	@Input() hideDelay: number;

	@Input() life: number;

	container: any;

	styleClass: string;

	tooltipText: any;

	showTimeout: any;

	hideTimeout: any;

	active: boolean;

	_text: string;

	changeListener: Function;

	mouseEnterListener: Function;

	mouseLeaveListener: Function;

	clickListener: Function;

	focusListener: Function;

	blurListener: Function;

	resizeListener: any;

	@Input()
	private control: AbstractControlDirective | AbstractControl;

	constructor(public el: ElementRef, public domHandler: DomHandler, public zone: NgZone, public renderer: Renderer, private _view: ViewContainerRef) { }

	// ngOnInit() {
	// 	let component = (<any>this._view)._element.component

	// 	//TODO: add initialization code here
	// }
	ngOnInit() {
		this.checkValidation();
	}

	onInputChange(event) {
		this.checkValidation();
	}

	ngAfterViewInit() {
		this.checkValidation();
		this.zone.runOutsideAngular(() => {
			if (this.tooltipEvent === 'hover') {
				this.mouseEnterListener = this.onMouseEnter.bind(this);
				this.mouseLeaveListener = this.onMouseLeave.bind(this);
				this.clickListener = this.onClick.bind(this);
				this.changeListener = this.onChange.bind(this);
				this.blurListener = this.onBlur.bind(this);
				this.el.nativeElement.addEventListener('mouseenter', this.mouseEnterListener);
				this.el.nativeElement.addEventListener('mouseleave', this.mouseLeaveListener);
				if (this.imageContainer) {
					if (typeof this.imageContainer.addEventListener !== 'undefined') {
						this.imageContainer.addEventListener('mouseenter', this.mouseEnterListener);
						this.imageContainer.addEventListener('mouseleave', this.mouseLeaveListener);
						this.imageContainer.addEventListener('click', this.mouseEnterListener);
					}
				}
				this.el.nativeElement.addEventListener('click', this.clickListener);
				this.el.nativeElement.addEventListener('change', this.changeListener);
				this.el.nativeElement.addEventListener('blur', this.blurListener);
			}
			else if (this.tooltipEvent === 'focus') {
				this.focusListener = this.onFocus.bind(this);
				this.blurListener = this.onBlur.bind(this);
				this.el.nativeElement.addEventListener('focus', this.focusListener);
				this.el.nativeElement.addEventListener('blur', this.blurListener);
			}
		});
	}

	mainContainer: any;
	inputContainer: any;
	imageContainer: any;
	image: any;
	test: any;

	validate(c: AbstractControl) {
		this.checkValidation();
		return null;
	}
	checkValidation() {
		this._text = '';
		if (this.control) {
			this.test = this.control;
			//for mismatch error added if form group have sub formgroup authen
			//TODO: this case may be occore error and performance is not good. This can be improve 
			if (this.hasError() || this.hasParentError(this.test.parent)) {
				this.renderer.setElementClass(this.el.nativeElement, 'ng-dirty', true);
				this.renderer.setElementClass(this.el.nativeElement, 'ng-invalid', true);
				if (this.hasError()) {
					for (let error of this.listOfErrors()) {
						this._text += error + '\n';
					}
				}
				if (this.hasParentError(this.test.parent)) {
					if (this.listOfErrors(this.test.parent)) {
						for (let error of this.listOfErrors(this.test.parent)) {
							this._text += error + '\n';
						}
					}
				}
				if (!this.mainContainer) {
					this.mainContainer = document.createElement('div');
					this.domHandler.appendChild(this.mainContainer, this.el.nativeElement.parentElement);
					this.inputContainer = document.createElement('div');
					var widthPerFrame = this.mainContainer.offsetWidth / 12;

					this.inputContainer.className = 'ui-g-11 no-padding';
					this.domHandler.appendChild(this.inputContainer, this.mainContainer);
					this.domHandler.appendChild(this.el.nativeElement, this.inputContainer);
					this.imageContainer = document.createElement('div');

					this.imageContainer.className = 'ui-g-1 validation-image-container no-padding';
					this.image = document.createElement('img');
					this.image.className = 'validation-image';
					this.image.src = "assets/layout/images/validation/valdation.png";
					this.imageContainer.appendChild(this.image);

					this.inputContainer.className = 'ui-g-11 no-padding';
					var widthPercentage = ((this.mainContainer.offsetWidth - 27) / (this.mainContainer.offsetWidth)) * 100;
					this.imageContainer.style.width = (100 - widthPercentage).toString() + '%';
					this.inputContainer.style.width = widthPercentage.toString() + '%';
					this.domHandler.appendChild(this.imageContainer, this.mainContainer);
				}
				else {
					this.domHandler.appendChild(this.imageContainer, this.mainContainer);
					var widthPerFrame = this.mainContainer.offsetWidth / 12;
					this.inputContainer.className = 'ui-g-11 no-padding';
					var widthPercentage = ((this.mainContainer.offsetWidth - 27) / (this.mainContainer.offsetWidth)) * 100;
					this.imageContainer.style.width = (100 - widthPercentage).toString() + '%';
					this.inputContainer.style.width = widthPercentage.toString() + '%';
				}
			}
			else {
				this._text = '';
				this.renderer.setElementClass(this.el.nativeElement, 'ng-dirty', false);
				this.renderer.setElementClass(this.el.nativeElement, 'ng-invalid', false);
				this.deactivate();
				if (this.mainContainer) {
					var hasImageChild = false;
					for (let element of this.mainContainer.children) {
						if (element.className.indexOf('validation-image-container') !== -1) {
							hasImageChild = true;
							break;
						}
					}
					if (hasImageChild) {
						this.mainContainer.removeChild(this.imageContainer);
					}
					this.inputContainer.className = 'ui-g-12 no-padding';
					this.inputContainer.style.width = '';
				}
			}
		}
	}
	onChange(e: Event) {
		this.checkValidation();
	}

	ngAfterViewChecked(): void {
		this.checkValidation();
	}

	onMouseEnter(e: Event) {
		if (!this.container && !this.showTimeout) {
			this.activate();
		}
	}

	onMouseLeave(e: Event) {
		this.deactivate();
	}

	onFocus(e: Event) {
		this.activate();
	}

	onBlur(e: Event) {
		this.deactivate();
		this.checkValidation();
	}

	onClick(e: Event) {
		this.deactivate();
	}

	activate() {
		this.active = true;
		this.clearHideTimeout();

		if (this.showDelay)
			this.showTimeout = setTimeout(() => { this.show() }, this.showDelay);
		else
			this.show();

		if (this.life) {
			let duration = this.showDelay ? this.life + this.showDelay : this.life;
			this.hideTimeout = setTimeout(() => { this.hide() }, duration);
		}
	}

	deactivate() {
		this.active = false;
		this.clearShowTimeout();

		if (this.hideDelay) {
			this.clearHideTimeout();    //life timeout
			this.hideTimeout = setTimeout(() => { this.hide() }, this.hideDelay);
		}
		else {
			this.hide();
		}
	}

	get text(): string {
		return this._text;
	}

	private static readonly errorMessages = {
		'required': () => 'Bu alanın girilmesi gerekli.',
		'minlength': (params) => 'Minimum karakter sayısı ' + params.requiredLength,
		'maxlength': (params) => 'Maximum karakter sayısı ' + params.requiredLength,
		'pattern': (params) => 'Girilmesi gereken patern: ' + params.requiredPattern,
		'years': (params) => params.message,
		'countryCity': (params) => params.message,
		'uniqueName': (params) => params.message,
		'telephoneNumbers': (params) => params.message,
		'telephoneNumber': (params) => params.message,
		'mismatch': (params) => params.message
	};

	hasError(control?): boolean {
		if (!control) {
			control = this.control;
		}
		if (control && control.errors)
			return true;
		else
			return false;
	}

	hasParentError(parentControl): boolean {
		return (parentControl && parentControl.parent && parentControl.status == "INVALID")
	}

	listOfErrors(control?): string[] {
		if (!control) {
			control = this.control;
		}
		if (this.hasError(control))
			return Object.keys(control.errors)
				.map(field => this.getMessage(field, control.errors[field]));
		else
			return null;
	}

	private getMessage(type: string, params: any) {
		return ValidationTooltip.errorMessages[type](params);
	}

	@Input('pValidationTooltip') set text(text: string) {
		this._text = '';
		if (this.hasError()) {
			this.renderer.setElementClass(this.el.nativeElement, 'ng-dirty', true);
			this.renderer.setElementClass(this.el.nativeElement, 'ng-invalid', true);
			for (let error of this.listOfErrors()) {
				this._text += error + '\n';
			}
		}
		else {
			this.renderer.setElementClass(this.el.nativeElement, 'ng-dirty', false);
			this.renderer.setElementClass(this.el.nativeElement, 'ng-invalid', false);
		}
		//this._text = text;
		if (this.active) {
			if (this._text) {
				if (this.container && this.container.offsetParent)
					this.updateText();
				else
					this.show();
			}
			else {
				this.hide();
			}
		}
	}

	create() {


		this.container = document.createElement('div');

		let tooltipArrow = document.createElement('div');
		tooltipArrow.className = 'ui-validation-tooltip-arrow';
		this.container.appendChild(tooltipArrow);

		this.tooltipText = document.createElement('div');
		this.tooltipText.className = 'ui-validation-tooltip-text ui-shadow ui-corner-all';

		this.updateText();

		if (this.positionStyle) {
			this.container.style.position = this.positionStyle;
		}

		this.container.appendChild(this.tooltipText);

		if (this.appendTo === 'body')
			document.body.appendChild(this.container);
		else if (this.appendTo === 'target')
			this.domHandler.appendChild(this.container, this.image);
		else if (this.appendTo === 'target')
			this.domHandler.appendChild(this.container, this.el.nativeElement);
		else
			this.domHandler.appendChild(this.container, this.appendTo);

		this.container.style.display = 'inline-block';
	}

	show() {
		if (!this.text || this.disabled) {
			return;
		}

		this.create();
		this.align();
		this.domHandler.fadeIn(this.container, 250);

		if (this.tooltipZIndex === 'auto')
			this.container.style.zIndex = ++DomHandler.zindex;
		else
			this.container.style.zIndex = this.tooltipZIndex;

		this.bindDocumentResizeListener();
	}

	remove() {
		if (this.container && this.container.parentElement) {
			if (this.appendTo === 'body')
				document.body.removeChild(this.container);
			else if (this.appendTo === 'target')
				this.el.nativeElement.removeChild(this.container);
			else
				this.domHandler.removeChild(this.container, this.appendTo);
		}

		this.unbindDocumentResizeListener();
		this.clearTimeouts();
		this.container = null;
	}

	hide() {
		this.remove();
	}

	updateText() {
		if (this.escape) {
			this.tooltipText.innerHTML = '';
			this.tooltipText.appendChild(document.createTextNode(this._text));
		}
		else {
			this.tooltipText.innerHTML = this._text;
		}
	}

	align() {
		let position = this.tooltipPosition;

		switch (position) {
			case 'top':
				this.alignTop();
				if (this.isOutOfBounds()) {
					this.alignBottom();
				}
				break;

			case 'bottom':
				this.alignBottom();
				if (this.isOutOfBounds()) {
					this.alignTop();
				}
				break;

			case 'left':
				this.alignLeft();
				if (this.isOutOfBounds()) {
					this.alignRight();

					if (this.isOutOfBounds()) {
						this.alignTop();

						if (this.isOutOfBounds()) {
							this.alignBottom();
						}
					}
				}
				break;

			case 'right':
				this.alignRight();
				if (this.isOutOfBounds()) {
					this.alignLeft();

					if (this.isOutOfBounds()) {
						this.alignTop();

						if (this.isOutOfBounds()) {
							this.alignBottom();
						}
					}
				}
				break;
		}
	}

	getHostOffset() {
		if (this.appendTo === 'body' || this.appendTo === 'target') {
			let offset = this.image.getBoundingClientRect();
			let targetLeft = offset.left + this.domHandler.getWindowScrollLeft();
			let targetTop = offset.top + this.domHandler.getWindowScrollTop();

			return { left: targetLeft, top: targetTop };
		}
		else {
			return { left: 0, top: 0 };
		}
	}

	alignRight() {
		this.preAlign('right');
		let hostOffset = this.getHostOffset();
		let left = hostOffset.left + this.domHandler.getOuterWidth(this.image);
		let top = hostOffset.top + (this.domHandler.getOuterHeight(this.image) - this.domHandler.getOuterHeight(this.container)) / 2;
		this.container.style.left = left + 'px';
		this.container.style.top = top + 'px';
	}

	alignLeft() {
		this.preAlign('left');
		let hostOffset = this.getHostOffset();
		let left = hostOffset.left - this.domHandler.getOuterWidth(this.container);
		let top = hostOffset.top + (this.domHandler.getOuterHeight(this.image) - this.domHandler.getOuterHeight(this.container)) / 2;
		this.container.style.left = left + 'px';
		this.container.style.top = top + 'px';
	}

	alignTop() {
		this.preAlign('top');
		let hostOffset = this.getHostOffset();
		let left = hostOffset.left + (this.domHandler.getOuterWidth(this.image) - this.domHandler.getOuterWidth(this.container)) / 2;
		let top = hostOffset.top - this.domHandler.getOuterHeight(this.container);
		this.container.style.left = left + 'px';
		this.container.style.top = top + 'px';
	}

	alignBottom() {
		this.preAlign('bottom');
		let hostOffset = this.getHostOffset();
		let left = hostOffset.left + (this.domHandler.getOuterWidth(this.image) - this.domHandler.getOuterWidth(this.container)) / 2;
		let top = hostOffset.top + this.domHandler.getOuterHeight(this.image);
		this.container.style.left = left + 'px';
		this.container.style.top = top + 'px';
	}

	preAlign(position: string) {
		this.container.style.left = -999 + 'px';
		this.container.style.top = -999 + 'px';

		let defaultClassName = 'ui-validation-tooltip ui-widget ui-validation-tooltip-' + position;
		this.container.className = this.tooltipStyleClass ? defaultClassName + ' ' + this.tooltipStyleClass : defaultClassName;
	}

	isOutOfBounds(): boolean {
		let offset = this.container.getBoundingClientRect();
		let targetTop = offset.top;
		let targetLeft = offset.left;
		let width = this.domHandler.getOuterWidth(this.container);
		let height = this.domHandler.getOuterHeight(this.container);
		let viewport = this.domHandler.getViewport();

		return (targetLeft + width > viewport.width) || (targetLeft < 0) || (targetTop < 0) || (targetTop + height > viewport.height);
	}

	onWindowResize(e: Event) {
		this.checkValidation();
		this.hide();
	}

	bindDocumentResizeListener() {
		this.zone.runOutsideAngular(() => {
			this.resizeListener = this.onWindowResize.bind(this);
			window.addEventListener('resize', this.resizeListener);
		});
	}

	unbindDocumentResizeListener() {
		if (this.resizeListener) {
			window.removeEventListener('resize', this.resizeListener);
			this.resizeListener = null;
		}
	}

	unbindEvents() {
		if (this.tooltipEvent === 'hover') {
			this.el.nativeElement.removeEventListener('mouseenter', this.mouseEnterListener);
			this.el.nativeElement.removeEventListener('mouseleave', this.mouseLeaveListener);
			this.el.nativeElement.removeEventListener('click', this.clickListener);
			this.el.nativeElement.removeEventListener('change', this.changeListener);
			this.el.nativeElement.removeEventListener('blur', this.blurListener);
			if (this.imageContainer) {
				if (typeof this.imageContainer.removeEventListener !== 'undefined') {
					this.imageContainer.removeEventListener('mouseenter', this.mouseEnterListener);
					this.imageContainer.removeEventListener('mouseleave', this.mouseLeaveListener);
					this.imageContainer.removeEventListener('click', this.mouseEnterListener);
				}
			}
		}
		else if (this.tooltipEvent === 'focus') {
			this.el.nativeElement.removeEventListener('focus', this.focusListener);
			this.el.nativeElement.removeEventListener('blur', this.blurListener);
		}

		this.unbindDocumentResizeListener();
	}

	clearShowTimeout() {
		if (this.showTimeout) {
			clearTimeout(this.showTimeout);
			this.showTimeout = null;
		}
	}

	clearHideTimeout() {
		if (this.hideTimeout) {
			clearTimeout(this.hideTimeout);
			this.hideTimeout = null;
		}
	}

	clearTimeouts() {
		this.clearShowTimeout();
		this.clearHideTimeout();
	}

	ngOnDestroy() {
		this.unbindEvents();
		this.remove();
	}
}

@NgModule({
	imports: [CommonModule],
	exports: [ValidationTooltip],
	declarations: [ValidationTooltip]
})
export class ValidationTooltipModule { }
