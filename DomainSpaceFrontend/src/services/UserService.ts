import axios from "axios";
import { API_URL_PREFIX } from "../config";
import {
  HttpHeaders,
  PagedList,
  ResponseModel,
  UpdateUserModel,
  UserFilter,
  UserModel,
} from "../types";

const URL_PREFIX = API_URL_PREFIX + "/user";

export class UserService {
  async getPagedAsync(
    filter: UserFilter
  ): Promise<ResponseModel<PagedList<UserModel>>> {
    try {
      const response = await axios.get(URL_PREFIX, {
        headers: HttpHeaders.get(),
        params: filter,
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = PagedList.fromJson<UserModel>(response.data, UserModel);

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }

  async getByIdAsync(id: string): Promise<ResponseModel<UserModel>> {
    try {
      const response = await axios.get(URL_PREFIX + `/${id}`, {
        headers: HttpHeaders.get(),
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = UserModel.fromJson(response.data);

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }

  async updateAsync(
    id: string,
    model: UpdateUserModel
  ): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.put(URL_PREFIX + `/${id}`, model, {
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

  async deleteAsync(id: string): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.delete(URL_PREFIX + `/${id}`, {
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
}
