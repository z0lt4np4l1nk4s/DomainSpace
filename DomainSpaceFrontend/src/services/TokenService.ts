import { AuthResponseModel, RoleEnum } from "../types";

const authTokenKey = "domainspace_auth_token";
const refreshTokenKey = "domainspace_refresh_token";

export class TokenService {
  static setAuthToken(token: string): void {
    localStorage.setItem(authTokenKey, token);
  }

  static setRefreshToken(refreshToken: string): void {
    localStorage.setItem(refreshTokenKey, refreshToken);
  }

  static removeTokens(): void {
    localStorage.removeItem(authTokenKey);
    localStorage.removeItem(refreshTokenKey);
  }

  static getAuthToken(): string | null {
    return localStorage.getItem(authTokenKey);
  }

  static getRefreshToken(): string | null {
    return localStorage.getItem(refreshTokenKey);
  }

  static getUserPayload(): AuthResponseModel | null {
    const token = this.getAuthToken();

    if (!token) {
      return null;
    }

    const payloadBase64 = token.split(".")[1];
    const payloadDecoded = atob(payloadBase64);
    const json = JSON.parse(payloadDecoded);

    const result = AuthResponseModel.fromJson(json);

    return result;
  }

  static isInRole(role: RoleEnum): boolean {
    const payload = this.getUserPayload();

    return (
      payload != null && payload.roles != null && payload.roles.includes(role)
    );
  }

  static getUserId(): string | null {
    const payload = this.getUserPayload();

    if (payload == null || payload.userId == null) {
      return null;
    }

    return payload!.userId;
  }

  static isAuthenticated(): boolean {
    const payload = this.getUserPayload();

    if (payload == null) {
      return false;
    }

    const now = new Date();
    const tokenExpiry = new Date(payload.expirationTime);

    if (tokenExpiry < now) {
      return false;
    }

    return true;
  }
}
