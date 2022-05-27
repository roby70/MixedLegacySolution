import { Injectable } from '@angular/core';
import {  UserManager, UserManagerSettings, User } from 'oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private manager =new UserManager(getClientSettings());
  
  private user: User = null;

  costructor() {
    this.manager.getUser().then(user => {
      this.user = user;
    });
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  // generate an authorization header (token type + token) from the User object
  getAuthorizationHeeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
    });
  }
}


export function getClientSettings(): UserManagerSettings {
  return {
    authority: 'https://localhost:7046/',
    client_id: 'AngularApp',
    redirect_uri: 'http://localhost:4200/auth-callback',
    post_logout_redirect_uri: 'http://localhost:4200/',
    response_type: 'id_token token',
    scope: 'openid profile myApiScope',
    filterProtocolClaims: true,
    loadUserInfo: true
  };
}
