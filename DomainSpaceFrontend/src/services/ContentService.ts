import axios from "axios";
import { API_URL_PREFIX } from "../config";
import {
  AddContentModel,
  ContentFilter,
  ContentModel,
  HttpHeaders,
  PagedList,
  ResponseModel,
  UpdateContentModel,
} from "../types";

const URL_PREFIX = API_URL_PREFIX + "/content";

export class ContentService {
  async getPagedAsync(
    filter: ContentFilter
  ): Promise<ResponseModel<PagedList<ContentModel>>> {
    try {
      const response = await axios.get(URL_PREFIX, {
        headers: HttpHeaders.get(),
        params: filter,
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = PagedList.fromJson<ContentModel>(
        response.data,
        ContentModel
      );

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }

  async getByIdAsync(id: string): Promise<ResponseModel<ContentModel>> {
    try {
      const response = await axios.get(URL_PREFIX + `/${id}`, {
        headers: HttpHeaders.get(),
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = ContentModel.fromJson(response.data);

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }

  async addAsync(model: AddContentModel): Promise<ResponseModel<boolean>> {
    const formData = new FormData();

    formData.append("title", model.title || "");
    formData.append("text", model.text || "");
    formData.append("domain", model.domain || "");
    formData.append("subjectId", model.subjectId || "");

    if (model.files && model.files.length > 0) {
      model.files!.forEach((file) => {
        formData.append(`files`, file);
      });
    }

    try {
      const response = await axios.post(URL_PREFIX, formData, {
        headers: {
          ...HttpHeaders.get(),
          "Content-Type": "multipart/form-data",
        },
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

  async updateAsync(
    model: UpdateContentModel
  ): Promise<ResponseModel<boolean>> {
    try {
      const formData = new FormData();

      formData.append("title", model.title || "");
      formData.append("text", model.text || "");
      formData.append("subjectId", model.subjectId || "");
      formData.append("oldFiles", JSON.stringify(model.oldFiles || []));

      if (model.localFiles && model.localFiles.length > 0) {
        model.localFiles!.forEach((file) => {
          formData.append(`files`, file);
        });
      }

      const response = await axios.put(URL_PREFIX + `/${model.id}`, formData, {
        headers: {
          ...HttpHeaders.get(),
          "Content-Type": "multipart/form-data",
        },
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
