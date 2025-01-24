import axios from "axios";
import { API_URL_PREFIX } from "../config";
import {
  AuthResponseModel,
  ChangePasswordModel,
  HttpHeaders,
  LoginModel,
  RegisterModel,
  ResetPasswordModel,
  ResponseModel,
} from "../types";
import { TokenService } from "./TokenService";

const URL_PREFIX = API_URL_PREFIX + "/auth";

export class AuthService {
  async loginAsync(model: LoginModel): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.post(URL_PREFIX + `/login`, model, {
        headers: {
          ...HttpHeaders.get(),
        },
      });

      if (response.status === 200) {
        const result = response.data.result;

        TokenService.setAuthToken(result.token);
        TokenService.setRefreshToken(result.refreshToken);

        return ResponseModel.success();
      }
      const result = ResponseModel.fromJson<boolean>(response.data);

      return result;
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<boolean>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }

  async logoutAsync(): Promise<ResponseModel<boolean>> {
    const payload = TokenService.getUserPayload();

    if (payload == null) {
      return ResponseModel.failure();
    }

    try {
      const response = await axios.post(
        URL_PREFIX + `/logout`,
        {},
        {
          headers: {
            ...HttpHeaders.get(),
          },
        }
      );

      if (response.status === 200) {
        TokenService.removeTokens();
        return ResponseModel.success();
      }
    } catch {}
    return ResponseModel.failure();
  }

  async registerAsync(model: RegisterModel): Promise<ResponseModel<any>> {
    try {
      const response = await axios.post(URL_PREFIX + `/register`, model, {
        headers: HttpHeaders.get(),
      });

      const result = ResponseModel.fromJson(response.data);

      return result;
    } catch (e) {
      if (axios.isAxiosError(e) && e.response) {
        const result = ResponseModel.fromJson<boolean>(e.response?.data);
        return result;
      }
      return ResponseModel.failure();
    }
  }

  async isAuthenticatedAsync(): Promise<boolean> {
    const payload = TokenService.getUserPayload();

    if (payload == null) {
      return false;
    }
    const now = new Date();
    const tokenExpiry = new Date(payload.expirationTime);

    if (tokenExpiry < now) {
      if (!TokenService.getRefreshToken()) {
        return false;
      }

      const result = await this.refreshTokenAsync();

      if (!result.isSuccess) {
        TokenService.removeTokens();
        return false;
      }
    }

    return true;
  }

  async refreshTokenAsync(): Promise<ResponseModel<any>> {
    const payload = TokenService.getUserPayload();

    if (payload == null) {
      return ResponseModel.failure();
    }

    try {
      const response = await axios.post(
        URL_PREFIX + `/refresh-token`,
        {
          refreshToken: TokenService.getRefreshToken(),
        },
        { headers: HttpHeaders.get() }
      );

      if (response.status === 200) {
        const result = response.data.result;

        TokenService.setAuthToken(result.token);
        TokenService.setRefreshToken(result.refreshTokenoken);

        return ResponseModel.success();
      }
    } catch {}
    return ResponseModel.failure();
  }

  async changeRoleAsync(model: {
    userId: string;
    role: string;
  }): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.post(URL_PREFIX + `/change-role`, model, {
        headers: HttpHeaders.get(),
      });

      if (response.status !== 200) return ResponseModel.failure();

      return ResponseModel.success();
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<boolean>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }

  async changePasswordAsync(
    model: ChangePasswordModel
  ): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.post(
        URL_PREFIX + `/change-password`,
        model,
        {
          headers: HttpHeaders.get(),
        }
      );

      return response.status === 200
        ? ResponseModel.success()
        : ResponseModel.failure();
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<boolean>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }

  async confirmEmailAsync(token: string): Promise<ResponseModel<any>> {
    try {
      const response = await axios.post(
        URL_PREFIX + `/confirm-email/${token}`,
        {},
        {
          headers: HttpHeaders.get(),
        }
      );

      return response.status === 200
        ? ResponseModel.success()
        : ResponseModel.failure();
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<any>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }

  async resetPasswordAsync(
    model: ResetPasswordModel
  ): Promise<ResponseModel<any>> {
    try {
      const response = await axios.post(URL_PREFIX + `/reset-password`, model, {
        headers: HttpHeaders.get(),
      });

      return response.status === 200
        ? ResponseModel.success()
        : ResponseModel.failure();
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<any>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }

  async sendResetPasswordEmailAsync(
    email: string
  ): Promise<ResponseModel<any>> {
    try {
      const response = await axios.post(
        URL_PREFIX + `/send-reset-password-email`,
        { email },
        {
          headers: HttpHeaders.get(),
        }
      );

      return response.status === 200
        ? ResponseModel.success()
        : ResponseModel.failure();
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<any>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }

  async verifyResetPasswordTokenAsync(
    email: string,
    token: string
  ): Promise<ResponseModel<any>> {
    try {
      const response = await axios.post(
        URL_PREFIX + `/verify-reset-password-token`,
        { email, token },
        {
          headers: HttpHeaders.get(),
        }
      );

      return response.status === 200
        ? ResponseModel.success()
        : ResponseModel.failure();
    } catch (e) {
      if (axios.isAxiosError(e)) {
        const result = ResponseModel.fromJson<any>(e.response?.data);
        return result;
      }
    }
    return ResponseModel.failure();
  }
}
