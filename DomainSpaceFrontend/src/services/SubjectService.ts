import axios from "axios";
import { API_URL_PREFIX } from "../config";
import {
  HttpHeaders,
  PagedList,
  ResponseModel,
  SubjectFilter,
  SubjectModel,
} from "../types";

const URL_PREFIX = API_URL_PREFIX + "/subject";

export class SubjectService {
  async getPagedAsync(
    filter: SubjectFilter
  ): Promise<ResponseModel<PagedList<SubjectModel>>> {
    try {
      const response = await axios.get(URL_PREFIX, {
        headers: HttpHeaders.get(),
        params: filter,
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = PagedList.fromJson<SubjectModel>(
        response.data,
        SubjectModel
      );

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }

  async getByIdAsync(id: string): Promise<ResponseModel<SubjectModel>> {
    try {
      const response = await axios.get(URL_PREFIX + `/${id}`, {
        headers: HttpHeaders.get(),
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = SubjectModel.fromJson(response.data);

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }

  async addAsync(model: SubjectModel): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.post(URL_PREFIX, model, {
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

  async updateAsync(model: SubjectModel): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.put(URL_PREFIX + `/${model.id}`, model, {
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
