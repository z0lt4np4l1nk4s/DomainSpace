export class ResetPasswordModel {
  email?: string;
  token?: string;
  password?: string;
  confirmPassword?: string;

  constructor({
    email,
    token: code,
    password,
    confirmPassword,
  }: ResetPasswordModel) {
    this.email = email;
    this.token = code;
    this.password = password;
    this.confirmPassword = confirmPassword;
  }
}
