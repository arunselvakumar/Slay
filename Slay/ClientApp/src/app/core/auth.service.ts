import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import {UserManager, User, WebStorageStateStore} from 'oidc-client';

@Injectable()
export class AuthService {
  private _userManager: UserManager;
  private _user: User;

  constructor(private httpClient: HttpClient) {
    const config = {
      authority: 'http://localhost:50366',
      client_id: 'socialnetwork',
      redirect_uri: 'http://localhost:4200/assets/html/oidc/oidc-login-redirect.html',
      scope: 'openid socialnetwork_fullaccess',
      response_type: 'id_token token',
      post_logout_redirect_uri: 'http://localhost:4200/?postLogout=true',
      userStore: new WebStorageStateStore({store: window.localStorage})
    };

    this._userManager = new UserManager(config);
    this._userManager.getUser().then(user => {
      if (user && !user.expired) {
        this._user = user;
      }
    });
  }

  login(): Promise<any> {
    return this._userManager.signinRedirect();
  }

  logout(): Promise<any> {
    return this._userManager.signoutRedirect();
  }

  isLoggedIn(): boolean {
    console.log(this._user);
    return this._user && this._user.access_token && !this._user.expired;
  }

  getAccessToken(): string {
    return this._user ? this._user.access_token : '';
  }

  signOutRedirectCallback(): Promise<any> {
    return this._userManager.signoutRedirectCallback();
  }
}
