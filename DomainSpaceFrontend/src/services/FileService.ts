import { API_URL_PREFIX } from "../config";
import { ResponseModel } from "../types";

const URL_PREFIX = API_URL_PREFIX + "/file";

export class FileService {
  async downloadAllFilesAsync(
    contentId: string
  ): Promise<ResponseModel<boolean>> {
    try {
      const downloadUrl = `${URL_PREFIX}/download-all/${contentId}`;
      const newTab = window.open(downloadUrl, "_blank");

      if (!newTab) {
        return ResponseModel.failure();
      }

      return ResponseModel.success();
    } catch {
      return ResponseModel.failure();
    }
  }
}
