import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { CDBRoutingModule } from './cdb-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        CDBRoutingModule,
        NgxPaginationModule
    ],
    declarations: [
        LayoutComponent,
        ListComponent
    ]
})
export class CDBModule { }  