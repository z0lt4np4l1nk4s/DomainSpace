import { ErrorDescriberModel } from "./ErrorDescriberModel";

export class ResponseModel<T> {
  isSuccess: boolean;
  message?: string;
  errorMessage?: ErrorDescriberModel;
  result?: T | null;

  constructor({ isSuccess, message, errorMessage, result }: ResponseModel<T>) {
    this.isSuccess = isSuccess;
    this.message = message;
    this.errorMessage = errorMessage;
    this.result = result;
  }

  static success<T>({
    message,
    errorMessage,
    result,
  }: Partial<ResponseModel<T>> = {}): ResponseModel<T> {
    return new ResponseModel<T>({
      isSuccess: true,
      message,
      errorMessage,
      result,
    });
  }

  static failure<T>({
    message,
    errorMessage,
    result,
  }: Partial<ResponseModel<T>> = {}): ResponseModel<T> {
    return new ResponseModel<T>({
      isSuccess: false,
      message,
      errorMessage,
      result,
    });
  }

  static fromJson<T>(data: any): ResponseModel<T> {
    if (!data) {
      return ResponseModel.failure();
    }

    let message = undefined;

    if (data["errorMessages"] && data["errorMessages"].length > 0) {
      message = ErrorDescriberModel.fromJson(data["errorMessages"][0]);
    }

    return new ResponseModel({
      isSuccess: data["isSuccess"],
      errorMessage: message,
    });
  }
}
