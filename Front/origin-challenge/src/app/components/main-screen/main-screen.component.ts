import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpressionStatement } from '@angular/compiler';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.css']
})
export class MainScreenComponent implements OnInit {

  constructor(private router: Router) { }

  error: boolean = true;
  message: string = "Test message";
  showMessage: boolean = false;

  ngOnInit(): void {
  }

  balance(event: any){
    this.router.navigate(['/balance'])
  }

  withdrawal(event: any){
    this.router.navigate(['/withdrawal'])
  }

}
