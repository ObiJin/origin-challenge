import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-keyboard',
  templateUrl: './keyboard.component.html',
  styleUrls: ['./keyboard.component.css']
})
export class KeyboardComponent implements OnInit {

  constructor() { }

  @Output() keyboardEvent = new EventEmitter<string>();
  @Output() cleanEvent = new EventEmitter();
  @Output() acceptEvent = new EventEmitter();

  ngOnInit(): void {
  }

  click(clickedButton: string){
    this.keyboardEvent.emit(clickedButton);
  }

  clean(){
    this.cleanEvent.emit();
  }

  accept(){
    this.acceptEvent.emit();
  }

}
