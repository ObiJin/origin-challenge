import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { KeyboardComponent } from './components/shared/keyboard/keyboard.component';
import { CardMaskPipe } from './pipes/card-mask.pipe';
import { MainScreenComponent } from './components/main-screen/main-screen.component';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/shared/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MainScreenComponent,
    KeyboardComponent,
    CardMaskPipe,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
