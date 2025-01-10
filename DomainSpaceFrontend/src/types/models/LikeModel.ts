export class LikeModel {
  contentId?: string;
  userId?: string;

  constructor({ contentId, userId }: LikeModel) {
    this.contentId = contentId;
    this.userId = userId;
  }
}
