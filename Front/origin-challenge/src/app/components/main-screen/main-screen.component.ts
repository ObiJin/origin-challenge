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

  constructor() { }

  error: boolean = true;
  message: string = "Test message";
  showMessage: boolean = false;

  ngOnInit(): void {
  }

}
