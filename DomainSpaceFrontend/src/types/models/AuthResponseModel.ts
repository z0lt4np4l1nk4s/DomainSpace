export class AuthResponseModel {
  userId: string;
  email?: string;
  roles: string[];
  expirationTime: Date;

  constructor({ expirationTime, roles, userId, email }: AuthResponseModel) {
    this.expirationTime = expirationTime;
    this.roles = roles;
    this.userId = userId;
    this.email = email;
  }

  static fromJson(data: any): AuthResponseModel {
    return new AuthResponseModel({
      expirationTime: data["exp"] && new Date(data["exp"] * 1000),
      roles: data["role"],
      userId: data["nameid"],
      email: data["email"],
    });
  }
}
