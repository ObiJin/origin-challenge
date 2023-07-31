import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MainScreenComponent } from './components/main-screen/main-screen.component';
import { BalanceComponent } from './components/balance/balance.component';
import { WithdrawalComponent } from './components/withdrawal/withdrawal.component';
import { OperationComponent } from './components/operation/operation.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'main', component: MainScreenComponent },
  { path: 'withdrawal', component: WithdrawalComponent },
  { path: 'operation/:id', component: OperationComponent },
  { path: 'balance', component: BalanceComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
