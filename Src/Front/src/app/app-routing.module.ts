import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home';
import { AuthGuard } from './_helpers';

const CDBModule = () => import('./cdb/cdb.module').then(x => x.CDBModule)

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    // { path: 'cdb', component: ListComponent, canActivate: [AuthGuard] },
    { path: 'cdb', loadChildren: CDBModule, canActivate: [AuthGuard] },



    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }