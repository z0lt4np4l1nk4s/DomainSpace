export class RegisterModel {
  firstName?: string;
  lastName?: string;
  dateOfBirth?: Date;
  email?: string;
  password?: string;

  constructor({
    firstName,
    lastName,
    dateOfBirth,
    email,
    password,
  }: RegisterModel = {}) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.dateOfBirth = dateOfBirth;
    this.email = email;
    this.password = password;
  }
}
