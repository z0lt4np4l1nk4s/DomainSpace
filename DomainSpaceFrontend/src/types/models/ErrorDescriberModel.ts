export class ErrorDescriberModel {
  errorCode?: string;
  description?: string;

  constructor({ errorCode, description }: ErrorDescriberModel) {
    this.errorCode = errorCode;
    this.description = description;
  }

  static fromJson(data: any): ErrorDescriberModel {
    return new ErrorDescriberModel({
      errorCode: data["errorCode"],
      description: data["description"],
    });
  }
}
