export class UserModel {
  id?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  roles?: string[];

  constructor({ id, firstName, lastName, email, roles }: UserModel) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.roles = roles;
  }

  static fromJson(data: any): UserModel {
    return new UserModel({
      id: data["id"],
      firstName: data["firstName"],
      lastName: data["lastName"],
      email: data["email"],
      roles: data["roles"],
    });
  }
}
