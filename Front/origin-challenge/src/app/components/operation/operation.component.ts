import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Operation } from 'src/app/models/operation.model';
import { CardsService } from 'src/app/services/cards.service';

@Component({
  selector: 'app-operation',
  templateUrl: './operation.component.html',
  styleUrls: ['./operation.component.css']
})
export class OperationComponent implements OnInit {

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private cardsService: CardsService) { 
      this.activatedRoute.params.subscribe(params => {
      this.idOperation = params['id'];
      this.getOperation();
    });
  }

  ngOnInit(): void {
  }

  idOperation: number= 0;
  operation: Operation = new Operation();

  back(event:any){
    this.router.navigate(['/main'])
  }

  getOperation(){
    this.cardsService.getOperation(this.idOperation, 
      (data: any) => {
        this.operation.amount = data.amount;
        this.operation.cardNumber = data.cardNumber;
        this.operation.operationDate = data.date;
        this.operation.balance = data.balance;
      },
      (error: any) => { console.log('error', error)})
  }
}
