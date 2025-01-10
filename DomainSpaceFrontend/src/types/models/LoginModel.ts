export class LoginModel {
  email?: string;
  password?: string;

  constructor({ email, password }: LoginModel = {}) {
    this.email = email;
    this.password = password;
  }
}
