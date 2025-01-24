import axios from "axios";
import { API_URL_PREFIX } from "../config";
import { JokeModel, ResponseModel } from "../types";

const URL_PREFIX = API_URL_PREFIX + "/joke";

export class JokeService {
  async getAsync(): Promise<ResponseModel<JokeModel>> {
    try {
      const response = await axios.get(URL_PREFIX, {
        headers: { "Content-Type": "application/json" },
      });

      if (response.status !== 200) return ResponseModel.failure();

      const result = JokeModel.fromJson(response.data);

      return ResponseModel.success({ result });
    } catch {
      return ResponseModel.failure();
    }
  }
}
