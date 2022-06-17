import { TestBed, ComponentFixture } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { ValidationTooltip } from './validation-tooltip';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('Tooltip', () => {
  
  let tooltip: ValidationTooltip;
  let fixture: ComponentFixture<ValidationTooltip>;
  
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        NoopAnimationsModule
      ],
      declarations: [
        ValidationTooltip
      ]
    });
    
    fixture = TestBed.createComponent(ValidationTooltip);
    tooltip = fixture.componentInstance;
  });
});
