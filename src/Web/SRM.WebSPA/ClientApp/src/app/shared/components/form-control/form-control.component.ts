import { NgModule, Component, ElementRef, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

let idx: number = 0;

@Component({
    selector: 'srm-formcontrol',
    template: `
    <div class="ui-g">
        <div class="ui-g-12 ui-md-4">
            <div class="ui-g-12 ui-md-12">
				<div class="ui-g-12 ui-md-4"  [ngStyle]="style" [class]="styleClass">
					<label for="{{for}}">{{text}}:</label>
				</div>
            </div>
        </div>
        <div class="ui-g-12 ui-md-8">
            <ng-content></ng-content>
        </div>
    </div>
    `
})
export class SrmFormControl {

    @Input() text: string;
    @Input() for: string;
    @Input() style: any;
    @Input() styleClass: string;

    animating: boolean;

    id: string = `ui-form-${idx++}`;

    constructor(private el: ElementRef) { }
}

