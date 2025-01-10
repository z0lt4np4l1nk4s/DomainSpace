import axios from "axios";
import { API_URL_PREFIX } from "../config";
import { HttpHeaders, LikeModel, ResponseModel } from "../types";

const URL_PREFIX = API_URL_PREFIX + "/like";

export class LikeService {
  async likeAsync(model: LikeModel): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.post(URL_PREFIX + "/like", model, {
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

  async dislikeAsync(model: LikeModel): Promise<ResponseModel<boolean>> {
    try {
      const response = await axios.post(URL_PREFIX + "/dislike", model, {
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
