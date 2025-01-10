import { ContentModel } from "../../types";
import ContentCardComponent from "./ContentCardComponent";

export default function ContentHolderComponent({
  items,
  onDeleteClick,
}: {
  items: ContentModel[];
  onDeleteClick: (content: ContentModel) => void;
}) {
  return (
    <div className="row justify-content-center mt-5">
      <div className="col-md-8">
        {items.map((item) => (
          <ContentCardComponent
            key={item.id}
            item={item}
            onDeleteClick={onDeleteClick}
          />
        ))}
      </div>
    </div>
  );
}
