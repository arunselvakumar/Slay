import {Component, OnInit} from '@angular/core';
import {AuthService} from './core/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Slay';

  constructor(private _authService: AuthService, private _router: Router) {

  }

  ngOnInit() {
    if (window.location.href.indexOf('?postLogout=true') > 0) {
      this._authService.signOutRedirectCallback().then(() => {
        const url: string = this._router.url.substring(0, this._router.url.indexOf('?'));
        this._router.navigateByUrl(url);
      });
    }
  }
}
