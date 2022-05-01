import { Component, OnInit } from '@angular/core';
import { UserForLogin } from '../_models/Account/UserForLogin';
import { AccountService } from '../_services/account.service';
import { BearerToken } from '../_models/Account/BearerToken';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private returnUrl: string;

  public userLoginForm: FormGroup;

  constructor(private accountService: AccountService,
              private router: Router,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder
    ) { }

  ngOnInit() {
    this.userLoginForm = this.formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/admin';

  }

  login() {
    const userLogin: UserForLogin = {
      ...this.userLoginForm.value
    };
    this.accountService.login(userLogin)
      .subscribe((bearerToken: BearerToken) => {
        localStorage.setItem('accessToken', bearerToken.accessToken);
        this.router.navigate([this.returnUrl]);
      });

  }

}
