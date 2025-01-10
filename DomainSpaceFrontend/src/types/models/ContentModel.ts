import { ContentUserInfoModel } from "./ContentUserInfoModel";
import { FileModel } from "./FileModel";

export class ContentModel {
  id?: string;
  user?: ContentUserInfoModel;
  subjectId?: string;
  subjectName?: string;
  domain?: string;
  title?: string;
  text?: string;
  creationTime?: Date;
  files?: FileModel[];
  likeCount?: number;
  liked?: boolean;

  constructor({
    id,
    user,
    subjectId,
    subjectName,
    domain,
    title,
    text,
    creationTime,
    files,
    likeCount,
    liked,
  }: ContentModel) {
    this.id = id;
    this.user = user;
    this.subjectId = subjectId;
    this.subjectName = subjectName;
    this.domain = domain;
    this.title = title;
    this.text = text;
    this.creationTime = creationTime;
    this.files = files;
    this.likeCount = likeCount;
    this.liked = liked;
  }

  static fromJson(data: any): ContentModel {
    return new ContentModel({
      id: data["id"],
      user: data["user"] && ContentUserInfoModel.fromJson(data["user"]),
      subjectId: data["subjectId"],
      subjectName: data["subjectName"],
      domain: data["domain"],
      title: data["title"],
      text: data["text"],
      creationTime: data["creationTime"],
      files:
        data["files"] &&
        data["files"].map((file: any) => FileModel.fromJson(file)),
      likeCount: data["likeCount"],
      liked: data["liked"],
    });
  }
}
