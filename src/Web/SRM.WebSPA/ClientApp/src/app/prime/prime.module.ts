import { NgModule } from '@angular/core';

import * as prime from 'primeng/primeng';
import { TableModule } from 'prime/table/table';
import { ToastModule } from 'prime/toast/toast';
import { DataViewModule } from 'prime/dataview/dataview';
import { RadioButtonModule } from 'prime/radiobutton/radiobutton';
import { CalendarModule } from 'prime/calendar/calendar';
import { DataTableModule } from 'prime/datatable/datatable';
import { DomHandler } from 'primeng/primeng';
import { BlockUIModule } from 'prime/blockui/blockui';
import { SrmCheckboxModule } from 'prime/srm-checkbox/srm-checkbox';
import { MessageService } from 'prime/message/messageservice';
import { ToasterModule } from 'prime/toast/toaster';
import { NotificationService } from 'prime/message/notification.service';
import { DialogModule } from 'prime/dialog/dialog';
import { TriStateCheckboxModule } from './tristatecheckbox/tristatecheckbox';

@NgModule({
	exports: [
		/* Prime */
		prime.AccordionModule,
		prime.AutoCompleteModule,
		prime.BreadcrumbModule,
		prime.ButtonModule,
		prime.CalendarModule,
		prime.CarouselModule,
		prime.ChartModule,
		prime.CheckboxModule,
		prime.ChipsModule,
		prime.CodeHighlighterModule,
		prime.ConfirmDialogModule,
		prime.ColorPickerModule,
		prime.SharedModule,
		prime.ContextMenuModule,
		prime.DataGridModule,
		prime.DataListModule,
		prime.DataScrollerModule,
		DataTableModule,
		DialogModule,
		prime.DragDropModule,
		prime.DropdownModule,
		prime.EditorModule,
		prime.FieldsetModule,
		prime.FileUploadModule,
		prime.GalleriaModule,
		prime.GMapModule,
		prime.GrowlModule,
		prime.InputMaskModule,
		prime.InputSwitchModule,
		prime.InputTextModule,
		prime.InputTextareaModule,
		prime.LightboxModule,
		prime.ListboxModule,
		prime.MegaMenuModule,
		prime.MenuModule,
		prime.MenubarModule,
		prime.MessagesModule,
		prime.MessageModule,
		prime.MultiSelectModule,
		prime.OrderListModule,
		prime.OrganizationChartModule,
		prime.OverlayPanelModule,
		prime.PaginatorModule,
		prime.PanelModule,
		prime.PanelMenuModule,
		prime.PasswordModule,
		prime.PickListModule,
		prime.ProgressBarModule,
		prime.ProgressSpinnerModule,
		//prime.RadioButtonModule,
		prime.RatingModule,
		prime.ScheduleModule,
		prime.ScrollPanelModule,
		prime.SelectButtonModule,
		prime.SlideMenuModule,
		prime.SliderModule,
		prime.SpinnerModule,
		prime.SplitButtonModule,
		prime.StepsModule,
		DataViewModule,
		TableModule,
		RadioButtonModule,
		prime.TabMenuModule,
		prime.TabViewModule,
		prime.TerminalModule,
		prime.TieredMenuModule,
		prime.ToggleButtonModule,
		prime.ToolbarModule,
		prime.TooltipModule,
		prime.TreeModule,
		prime.TreeTableModule,
		TriStateCheckboxModule,
		ToastModule,
		BlockUIModule,
		SrmCheckboxModule,
		ToasterModule
		/* Prime */
	],
	declarations:[
	],
	providers: [
		/* Prime Services*/
		MessageService,
		NotificationService,
		prime.ConfirmationService,
		DomHandler
	],
})
export class PrimeModule {

}
