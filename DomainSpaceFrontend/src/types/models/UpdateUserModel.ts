export class UpdateUserModel {
  firstName?: string;
  lastName?: string;
  dateOfBirth?: Date;

  constructor({ firstName, lastName, dateOfBirth }: UpdateUserModel) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.dateOfBirth = dateOfBirth;
  }
}
