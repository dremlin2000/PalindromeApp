import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PalindromeComponent } from './components/palindrome/palindrome.component';

import { UtilityService } from './services/utility.service';

import { PalindromeService } from './services/palindrome.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PalindromeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'palidrome', pathMatch: 'full' },
            { path: 'palidrome', component: PalindromeComponent },
            { path: '**', redirectTo: 'palidrome' }
        ])
    ],
    providers: [
        UtilityService,
        PalindromeService
    ]
})
export class AppModuleShared {
}
