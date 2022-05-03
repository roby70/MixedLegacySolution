import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-protected',
  templateUrl: './protected.component.html',
  styleUrls: ['./protected.component.css']
})
export class ProtectedComponent implements OnInit {
  response: Object;

  constructor(private http: HttpClient, private authService: AuthService) { }

  ngOnInit(): void {
    let headers = new HttpHeaders()
      .set('Authorization', this.authService.getAuthorizationHeeaderValue())
      .set('Access-Control-Allow-Origin', '*')
      .set('Access-Control-Allow-Methods', 'GET,HEAD,OPTIONS,POST,PUT');

    this.http.get("https://localhost:44360/api/cars", { headers: headers})
    .subscribe(response => this.response = response);
  }

}
