import { FileModel } from "./FileModel";

export class UpdateContentModel {
  id?: string;
  subjectId?: string;
  title?: string;
  text?: string;
  localFiles?: File[];
  oldFiles?: FileModel[];

  constructor({
    id,
    subjectId,
    title,
    text,
    localFiles,
    oldFiles,
  }: UpdateContentModel) {
    this.id = id;
    this.subjectId = subjectId;
    this.title = title;
    this.text = text;
    this.localFiles = localFiles;
    this.oldFiles = oldFiles;
  }
}
