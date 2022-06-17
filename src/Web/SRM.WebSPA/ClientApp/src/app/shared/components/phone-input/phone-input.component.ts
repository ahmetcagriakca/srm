import { Component, Input } from "@angular/core";
import { FormGroup, FormBuilder } from "@angular/forms";

class MyTel {
    constructor(public area: string, public exchange: string, public subscriber: string) {}
  }
  
  @Component({
    selector: 'srm-phone-input',
    template: `
      <div [formGroup]="parts">
        <input class="area" formControlName="area" size="3">
        <span>&ndash;</span>
        <input class="exchange" formControlName="exchange" size="3">
        <span>&ndash;</span>
        <input class="subscriber" formControlName="subscriber" size="4">
      </div>
    `,
    styles: [`
      div {
        display: flex;
      }
      input {
        border: none;
        background: none;
        padding: 0;
        outline: none;
        font: inherit;
        text-align: center;
      }
    `],
  })
  class MyPhoneInput {
    parts: FormGroup;
  
    @Input()
    get value(): MyTel | null {
      let n = this.parts.value;
      if (n.area.length == 3 && n.exchange.length == 3 && n.subscriber.length == 4) {
        return new MyTel(n.area, n.exchange, n.subscriber);
      }
      return null;
    }
    set value(tel: MyTel | null) {
      tel = tel || new MyTel('', '', '');
      this.parts.setValue({area: tel.area, exchange: tel.exchange, subscriber: tel.subscriber});
    }
  
    constructor(fb: FormBuilder) {
      this.parts =  fb.group({
        'area': '',
        'exchange': '',
        'subscriber': '',
      });
    }
  }