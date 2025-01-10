import { AuthResponseModel, RoleEnum } from "../types";

const userTokenKey = "domainspace_user_token";

export class UserDataService {
  static setUserAuthData(model: AuthResponseModel): void {
    localStorage.setItem(userTokenKey, JSON.stringify(model));
  }

  static removeUserData(): void {
    localStorage.removeItem(userTokenKey);
  }

  static getUserToken(): AuthResponseModel | null {
    const jsonData = localStorage.getItem(userTokenKey);

    if (jsonData) {
      const data = JSON.parse(jsonData) as AuthResponseModel;
      return data;
    }

    return null;
  }

  static isInRole(role: RoleEnum): boolean {
    const userToken = this.getUserToken();

    return (
      userToken != null &&
      userToken.roles != null &&
      userToken.roles.includes(role)
    );
  }

  static getUserId(): string | null {
    const userToken = this.getUserToken();

    if (userToken == null || userToken.userId == null) {
      return null;
    }

    return userToken!.userId;
  }

  static isAuthenticated(): boolean {
    const userToken = this.getUserToken();

    if (userToken == null) {
      return false;
    }

    const now = new Date();
    const tokenExpiry = new Date(userToken.expirationTime);

    if (tokenExpiry < now) {
      return false;
    }

    return true;
  }
}
