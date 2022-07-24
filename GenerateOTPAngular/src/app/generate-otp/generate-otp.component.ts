import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Otp from '../models/otp';
import GenerateOtpRequest from '../models/generateOtpRequest';

@Component({
  selector: 'generate-otp',
  templateUrl: './generate-otp.component.html',
  styleUrls: ['./generate-otp.component.css'],
})
export class GenerateOtpComponent implements OnInit {
  //Hardcoded userId
  userId: string = '80c416ce-801b-42db-a383-4608ac10bc50';
  code: number = 0;
  countdownTime: number = 0;
  intervalId: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.generateOTP();
  }

  generateOTP() {
    clearInterval();

    const generateOtpRequest: GenerateOtpRequest = {
      timestamp: new Date(),
      userId: this.userId,
    };

    const headers = {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    };

    this.http
      .post<Otp>('http://localhost:5158/Otp', generateOtpRequest, { headers })
      .subscribe((data) => {
        if (this.intervalId != null) {
          clearInterval(this.intervalId);
        }

        this.code = data.code;
        this.countdownTime = data.validity;

        this.intervalId = setInterval(() => {
          if (this.countdownTime == 0) {
            clearInterval(this.intervalId);
          } else {
            this.countdownTime--;
          }
        }, 1000);
      });
  }
}
