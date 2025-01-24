import {
  ContentModel,
  DropdownButtonModel,
  PageRouteEnum,
  RoleEnum,
} from "../../types";
import { FaEllipsisH } from "react-icons/fa";
import { DefaultButton, DropdownButton, LikeButtonComponent } from "../common";
import { TokenService } from "../../services";
import { useNavigate } from "react-router-dom";

export default function ContentCardComponent({
  item,
  onDeleteClick,
}: {
  item: ContentModel;
  onDeleteClick: (content: ContentModel) => void;
}) {
  const navigate = useNavigate();
  const getDropdownOptions = () => {
    const options: DropdownButtonModel[] = [];

    options.push(
      new DropdownButtonModel("Details", () => {
        navigate(PageRouteEnum.ContentDetails.replace(":id", item.id!));
      })
    );

    if (
      TokenService.isInRole(RoleEnum.Admin) ||
      TokenService.getUserId() === item.user?.userId
    ) {
      options.push(
        new DropdownButtonModel("Edit", () => {
          navigate(PageRouteEnum.EditContent.replace(":id", item.id!));
        })
      );
      options.push(
        new DropdownButtonModel("Delete", () => {
          onDeleteClick(item);
        })
      );
    } else if (TokenService.isInRole(RoleEnum.Moderator)) {
      options.push(
        new DropdownButtonModel("Delete", () => {
          onDeleteClick(item);
        })
      );
    }

    return options;
  };

  return (
    <div className="mb-4">
      <div className="card">
        <div className="card-header d-flex justify-content-between align-items-center">
          <div>
            <strong>{item.user?.fullName || "Anonymous"}</strong>
          </div>
          <div className="text-muted d-flex justify-content-end align-items-center">
            {item.subjectName}

            <DropdownButton items={getDropdownOptions()}>
              <FaEllipsisH />
            </DropdownButton>
          </div>
        </div>

        <div className="card-body">
          <h4 className="card-title">{item.title}</h4>
          <p className="card-text max-5-rows">{item.text}</p>
          <div className="d-flex justify-content-between align-items-center mt-2">
            <div>
              <strong>{(item?.files || []).length} File(s)</strong>
            </div>

            <div>
              <DefaultButton
                onClick={() =>
                  navigate(
                    PageRouteEnum.ContentDetails.replace(":id", item.id!)
                  )
                }
              >
                Details
              </DefaultButton>
            </div>
          </div>
        </div>

        <div className="card-footer d-flex justify-content-center align-items-center">
          <LikeButtonComponent content={item} />
        </div>
      </div>
    </div>
  );
}
