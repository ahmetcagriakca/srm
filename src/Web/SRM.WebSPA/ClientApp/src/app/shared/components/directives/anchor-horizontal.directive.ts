import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
    selector: '[AnchorHorizontal]'
})
export class AnchorHorizontalDirective {
    @Input() AnchorHorizontal: string;

    constructor(el: ElementRef) {
        if (this.AnchorHorizontal) {
            el.nativeElement.style.width = this.AnchorHorizontal;
        }
        else{
            el.nativeElement.style.width = '100%';
        }
    }
}