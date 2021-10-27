import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LayoutComponent } from './layout.component';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCard } from '@angular/material/card';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AccountRoutingModule,
        BrowserAnimationsModule,
        FormsModule,
        HttpClientModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSelectModule,
        MatCard
    ],
    declarations: [
        LayoutComponent,
        //LoginComponent,
        //RegisterComponent
    ]
})
export class AccountModule { }
