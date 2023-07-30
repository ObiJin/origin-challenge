import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MainScreenComponent } from './components/main-screen/main-screen.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'main', component: MainScreenComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
