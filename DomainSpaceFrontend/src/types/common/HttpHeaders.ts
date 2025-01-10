import { UserDataService } from "../../services";

export class HttpHeaders {
  static get() {
    const userToken = UserDataService.getUserToken();

    return {
      Authorization: "Bearer " + userToken?.token,
      "Content-Type": "application/json",
    };
  }
}
