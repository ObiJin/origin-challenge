import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Login } from 'src/app/models/login.model';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private loginService: LoginService,
    private router: Router) {
      this.userLogedObserver = this.loginService.loginListener().subscribe(state =>
        this.isUserLogged = state.userName != "" && state.userName != undefined)
  }

  isUserLogged: boolean = false;
  userLogedObserver: Subscription;

  ngOnInit(): void {
    this.userLogedObserver = this.loginService.loginListener().subscribe(state =>
      this.isUserLogged = state.userName != "" && state.userName != undefined)
  }

  logout(): void {
    this.loginService.logOut();
    this.router.navigate(['/']);
  }

  ngOnDestroy() {
    this.userLogedObserver.unsubscribe(); // make sure to unsubscribe
  }

}
