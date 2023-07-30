import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Login } from 'src/app/models/login.model';
import { CardsService } from 'src/app/services/cards.service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router,
    private cardService: CardsService,
    private loginService: LoginService) {
    var usr = loginService.getUserLogin();

    this.userLogedObserver = this.loginService.loginListener().subscribe(state => {
      this.cardNumber = state.userName;
      this.showPassword = this.cardNumber.length > 0;
    })

    if (usr.token != "" && usr.token != undefined && usr.userName != undefined && usr.userName != "")
      router.navigate(['/main']);
  }

  error: boolean = false;
  showMessage: boolean = false;
  showPassword: boolean = false;
  retries: number = 0;
  message: string = "";
  inputValue: string = "";

  isUserLogged: boolean = false;
  userLogedObserver: Subscription;

  private validLength: number = 16;
  private cardNumber: string = "";

  ngOnInit(): void {
    this.inputValue = "";
    this.showMessage = false;
  }

  onKeyboardEvent(value: any) {
    if (this.inputValue.length < this.validLength)
      this.inputValue += value;
  }

  onClean(event: any) {
    this.inputValue = "";
    this.setMessage("", false);
  }

  onAccept(event: any) {
    if (this.cardNumber === "") {
      if (this.inputValue.length < this.validLength)
        this.setMessage("Número de tarjeta inválido", true);
      else {
        this.findCard(this.inputValue);
      }
    }
    else {
      this.validatePIN(this.cardNumber, this.inputValue);
    }
  }

  findCard(number: string) {
    this.cardService.get(this.inputValue,
      (data: any) => {
        this.cardNumber = number;
        this.inputValue = "";
        this.showPassword = true;

        var login = new Login();
        login.userName = data.cardNumber;

        this.loginService.saveInStorage(login);
      },
      (error: any) => {
        this.setMessage(error.error, true);
      }
    )
  }

  validatePIN(cardNumber: string, pin: string) {
    var body = {
      "cardNumber": cardNumber,
      "pinNumber": pin
    };

    this.cardService.login(body,
      (data: Login) => {
        this.loginService.saveInStorage(data);
        this.router.navigate(['/main']);
      },
      (error: any) => {
        this.retries++;
        if (this.retries > 3) {
          this.resetForm();
          console.log('Bloquea2');
          this.cardService.block(body,
            (data: any) => { this.setMessage(`La tarjeta ${data.cardNumber} ha sido bloqueada por exceder el número de intentos.`, true) },
            (error: any) => { this.setMessage(error, true) }
          )
        }

        this.setMessage(error.error, true);
      }
    );
  }

  resetForm() {
    this.cardNumber = "";
    this.inputValue = "";
    this.retries = 0;
    this.showPassword = false;
    this.setMessage("", false);
  }

  setMessage(message: string, error: boolean) {
    this.showMessage = message.length > 0;
    this.error = error;
    this.message = message;
  }

  ngOnDestroy() {
    this.userLogedObserver.unsubscribe(); // make sure to unsubscribe
  }
}
