import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { UserRegistrationDto } from "../dtos/user-registration-dto";
import { UserLoginDto } from "../dtos/user-login-dto";
import { Router } from "@angular/router";
import { UserDto } from "../dtos/user-dto";

@Injectable({
  providedIn: "root",
})

export class UserService {
  private baseUrl = "http://localhost:21212/api/Users";
  public currentUser: UserDto = new UserDto();

  constructor(
    private http: HttpClient,
    private router: Router) {}

  initializeUser(): Promise<{}> {
    return new Promise((resolve) => {
      return this.http
        .get<UserDto>(`${this.baseUrl}/CurrentUser`)
        .subscribe((userData) => {
          this.currentUser = userData;
          resolve(true);
        });
    });
  }

  register(userDto: UserRegistrationDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Register`, userDto);
  }

  login(userDto: UserLoginDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Login`, userDto);
  }

  logout(): Observable<void> {
    this.currentUser = null;
    localStorage.clear();
    this.router.navigate(["/"]);
    return;
  }
}