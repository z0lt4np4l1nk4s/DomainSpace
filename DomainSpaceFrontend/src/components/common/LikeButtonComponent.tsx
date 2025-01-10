import { useState } from "react";
import { LikeService, UserDataService } from "../../services";
import { FaThumbsUp, FaThumbsDown } from "react-icons/fa";
import { ContentModel } from "../../types";
import { LikeModel } from "../../types/models/LikeModel";

const likeService = new LikeService();

export default function LikeButtonComponent({
  content,
}: {
  content: ContentModel;
}) {
  const [liked, setLiked] = useState<boolean>(content.liked || false);

  const handleLike = async () => {
    const model = new LikeModel({
      contentId: content.id,
      userId: UserDataService.getUserId()!,
    });
    if (liked) {
      const response = await likeService.dislikeAsync(model);

      if (response.isSuccess) {
        setLiked(false);
      } else {
        console.log(response.errorMessage?.description);
      }
    } else {
      const response = await likeService.likeAsync(model);

      if (response.isSuccess) {
        setLiked(true);
      } else {
        console.log(response.errorMessage?.description);
      }
    }
  };

  return (
    <div className="d-flex align-items-center mt-2 mb-2">
      <button
        onClick={handleLike}
        className="btn btn-outline-light d-flex align-items-center"
      >
        {liked ? (
          <FaThumbsDown className="me-2" />
        ) : (
          <FaThumbsUp className="me-2" />
        )}
        {liked ? "Unlike" : "Like"}
      </button>
      <span className="ms-2">
        {content.likeCount} {content.likeCount === 1 ? "like" : "likes"}
      </span>
    </div>
  );
}
