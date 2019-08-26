import { NgModule } from '@angular/core';

import { ConfirmDialogModule } from 'primeng/components/confirmdialog/confirmdialog';
import { DialogModule } from 'primeng/components/dialog/dialog';
import { GrowlModule } from 'primeng/components/growl/growl';
import { ToastModule } from 'primeng/components/toast/toast';
import { TooltipModule } from 'primeng/components/tooltip/tooltip';
import { DropdownModule } from 'primeng/components/dropdown/dropdown';
import { TableModule } from 'primeng/components/table/table';
import {CalendarModule} from 'primeng/components/calendar/calendar';


@NgModule({
  imports: [
    CalendarModule,
    ConfirmDialogModule,
    DialogModule,
    DropdownModule,
    GrowlModule,
    TooltipModule,
    TableModule,
    ToastModule
  ],
  declarations: [],
  exports: [
    CalendarModule,
    ConfirmDialogModule,
    DialogModule,
    DropdownModule,
    GrowlModule,
    TooltipModule,
    TableModule,
    ToastModule
  ]
})
export class PrimeNgSharedModule { }
