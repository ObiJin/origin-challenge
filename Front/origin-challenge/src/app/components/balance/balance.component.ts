import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Card } from 'src/app/models/card.model';
import { CardsService } from 'src/app/services/cards.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-balance',
  templateUrl: './balance.component.html',
  styleUrls: ['./balance.component.css']
})
export class BalanceComponent implements OnInit {

  constructor(private router: Router,
    private cardsService: CardsService,
    private loginService: LoginService) { }

  error: boolean = false;
  showMessage: boolean = false;
  message: string = "";

  card: Card = new Card();

  ngOnInit(): void {
    var user = this.loginService.getUserLogin();
    this.cardsService.getBalance(user.userName,
      (data: any) => {
        this.card.number = data.cardNumber;
        this.card.balance = data.balance;
        this.card.expireDate = data.expireDate;
      },
      (error: any) => { this.setMessage(error, true) });
  }

  back(event: any) {
    this.router.navigate(['/main'])
  }

  setMessage(message: string, error: boolean) {
    this.showMessage = message.length > 0;
    this.error = error;
    this.message = message;
  }
}
