export interface UserRegister {
  readonly username: string;
  readonly email: string;
  readonly password: string;
}

export interface UserLogin {
  readonly username: string;
  readonly password: string;
}

export interface UserResponse {
  readonly username: string;
  readonly email: string;
  readonly token: string;
}
