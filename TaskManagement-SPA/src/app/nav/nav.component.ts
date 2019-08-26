import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { Message } from 'primeng/api';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  growlMessage: Message[] = [];

  constructor(
    private router: Router,
    public authService: AuthService
  ) { }

  ngOnInit() {
  }

  isLoggedIn() {    
    return this.authService.loggedIn();
  }

  isAdmin(){
    if (this.authService.decodedToken.role === "Admin") {      
      return true;
    }
    return false;
  }

  onLogout() {
    localStorage.removeItem('token');    
    this.growlMessage = [];
    this.growlMessage.push({ severity: 'success', summary: 'Logged out successfully', detail: '' });
    this.router.navigate(['/login']);
  }
}
