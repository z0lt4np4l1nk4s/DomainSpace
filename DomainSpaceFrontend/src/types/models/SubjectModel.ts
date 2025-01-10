export class SubjectModel {
  id?: string;
  userId?: string;
  name?: string;
  domain?: string;

  constructor({ id, userId, name, domain }: SubjectModel) {
    this.id = id;
    this.userId = userId;
    this.name = name;
    this.domain = domain;
  }

  static fromJson(data: any): SubjectModel {
    return new SubjectModel({
      id: data["id"],
      userId: data["userId"],
      name: data["name"],
      domain: data["domain"],
    });
  }
}
