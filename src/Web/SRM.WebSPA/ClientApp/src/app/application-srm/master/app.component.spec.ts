import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from 'application-srm/master/app.component';
import { AppTopBarComponent } from 'application-srm/master/app.topbar.component';
import { AppFooterComponent } from 'application-srm/master/app.footer.component';
import { AppMenuComponent, AppSubMenuComponent } from 'application-srm/master/app.menu.component';
import { ScrollPanelModule} from 'primeng/primeng';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ RouterTestingModule, ScrollPanelModule ],
      declarations: [
          AppComponent,
          AppTopBarComponent,
          AppMenuComponent,
          AppSubMenuComponent,
          AppFooterComponent
      ],
    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
});
