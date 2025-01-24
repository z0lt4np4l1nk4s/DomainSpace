import { TokenService } from "../../services";

export class HttpHeaders {
  static get() {
    return {
      Authorization: "Bearer " + TokenService.getAuthToken(),
      "Content-Type": "application/json",
    };
  }
}
