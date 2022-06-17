import { TestBed, ComponentFixture } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { SrmCheckbox } from './srm-checkbox';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('SrmCheckbox', () => {

    let checkbox: SrmCheckbox;
    let fixture: ComponentFixture<SrmCheckbox>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                NoopAnimationsModule
            ],
            declarations: [
                SrmCheckbox
            ]
        });

        fixture = TestBed.createComponent(SrmCheckbox);
        checkbox = fixture.componentInstance;
    });

    it('should check the input on click', () => {
        const boxEl = fixture.nativeElement.querySelector('.ui-chkbox-box');
        boxEl.click();
        fixture.detectChanges();

        const input = fixture.nativeElement.querySelector('input');
        expect(input.checked).toBe(true);
    });
});