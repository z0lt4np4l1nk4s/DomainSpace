export class ContentUserInfoModel {
  userId?: string;
  firstName?: string;
  lastName?: string;
  fullName?: string;

  constructor({ userId, firstName, lastName }: ContentUserInfoModel) {
    this.userId = userId;
    this.firstName = firstName;
    this.lastName = lastName;
    this.fullName = `${firstName} ${lastName}`;
  }

  static fromJson(data: any): ContentUserInfoModel {
    return new ContentUserInfoModel({
      userId: data["userId"],
      firstName: data["firstName"],
      lastName: data["lastName"],
    });
  }
}
