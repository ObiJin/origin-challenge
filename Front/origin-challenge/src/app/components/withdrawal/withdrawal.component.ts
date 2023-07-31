import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CardsService } from 'src/app/services/cards.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-withdrawal',
  templateUrl: './withdrawal.component.html',
  styleUrls: ['./withdrawal.component.css']
})
export class WithdrawalComponent implements OnInit {

  constructor(private router: Router,
    private cardsService: CardsService,
    private loginService: LoginService) { }

  inputValue: string = "";
  error: boolean = false;
  showMessage: boolean = false;
  message: string = "";

  ngOnInit(): void {
  }

  onKeyboardEvent(value: any) {
    this.inputValue += value;
  }

  onClean(event: any) {
    this.inputValue = "";
  }

  onAccept(event: any) {
    var body = {
      "cardNumber": this.loginService.getUserLogin().userName,
      "amount": this.inputValue,
    };

    this.cardsService.withdraw(body,
      (data: any) => {
        console.log('withdraw', data);
        this.router.navigate([`/operation/${data.id}`]);
      },
      (error: any) => { 
        debugger;
        console.log('withdrawError')
        this.setMessage(error.error || error.statusText || error, true) }
      )
  }

  setMessage(message: any, error: boolean) {
    this.showMessage = message.length ?? message.detail.length > 0;
    this.error = error;
    this.message = message.length > 0 ? message : message.detail;
  }
}
