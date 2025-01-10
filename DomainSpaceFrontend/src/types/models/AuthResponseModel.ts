export class AuthResponseModel {
  userId: string;
  email?: string;
  roles: string[];
  token: string;
  refreshToken: string;
  expirationTime: Date;

  constructor({
    token,
    expirationTime,
    refreshToken,
    roles,
    userId,
    email,
  }: AuthResponseModel) {
    this.token = token;
    this.expirationTime = expirationTime;
    this.refreshToken = refreshToken;
    this.roles = roles;
    this.userId = userId;
    this.email = email;
  }

  static fromJson(data: any): AuthResponseModel {
    return new AuthResponseModel({
      token: data["token"],
      expirationTime:
        data["expirationTime"] && new Date(data["expirationTime"]),
      refreshToken: data["refreshToken"],
      roles: data["roles"],
      userId: data["userId"],
      email: data["email"],
    });
  }
}
