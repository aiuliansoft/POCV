import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import CheckOtpRequest from '../models/checkOtpRequest';

@Component({
  selector: 'check-otp',
  templateUrl: './check-otp.component.html',
  styleUrls: ['./check-otp.component.css'],
})
export class CheckOtpComponent implements OnInit {
  //Hardcoded userId
  userId: string = '80c416ce-801b-42db-a383-4608ac10bc50';
  code: number = 0;
  otpStatus: string = 'N/A';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  checkOTP() {
    const checkOtpRequest: CheckOtpRequest = {
      otpCode: this.code,
      userId: this.userId,
    };

    const headers = {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    };
    console.log(this.code);
    this.http
      .post<Boolean>('http://localhost:5158/Otp/check', checkOtpRequest, {
        headers,
      })
      .subscribe((data) => {
        this.otpStatus = data ? 'Valid' : 'Invalid';
      });
  }
}
