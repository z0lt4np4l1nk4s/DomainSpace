export class FileModel {
  fileId?: string;
  name?: string;
  extension?: string;
  size?: number;
  path?: string;

  constructor({ fileId, name, extension, size, path }: FileModel) {
    this.fileId = fileId;
    this.name = name;
    this.extension = extension;
    this.size = size;
    this.path = path;
  }

  static fromJson(data: any): FileModel {
    return new FileModel({
      fileId: data["fileId"],
      name: data["name"],
      extension: data["extension"],
      size: data["size"],
      path: data["path"],
    });
  }
}
