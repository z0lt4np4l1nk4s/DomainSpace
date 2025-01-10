import { DefaultButton, TableComponent } from "../common";
import { RoleEnum, SubjectModel } from "../../types";
import { UserDataService } from "../../services";

export default function SubjectsTableComponent({
  items,
  onEditClick,
  onRemoveClick,
}: {
  items: SubjectModel[];
  onEditClick: (item: SubjectModel) => void;
  onRemoveClick: (item: SubjectModel) => void;
}) {
  const adminGuid = "26eac08e-c543-41e8-90c3-41d36faaab50";
  const defaultSubjectName = "Default";

  return (
    <TableComponent>
      <thead>
        <tr>
          <th scope="col" className="col-4">
            {"Name"}
          </th>
          {UserDataService.isInRole(RoleEnum.Admin) && (
            <th scope="col" className="col-4">
              {"Domain"}
            </th>
          )}
          <th scope="col" className="col-2">
            {"Actions"}
          </th>
        </tr>
      </thead>
      <tbody>
        {items.map((item) => (
          <tr key={item.id}>
            <td>{item.name}</td>
            {UserDataService.isInRole(RoleEnum.Admin) && <td>{item.domain}</td>}
            <td>
              <div className="btn-group gap-1">
                <DefaultButton
                  width={80}
                  disabled={
                    item.userId === adminGuid &&
                    item.name === defaultSubjectName
                  }
                  color="secondary"
                  onClick={() => {
                    if (onEditClick) {
                      onEditClick(item);
                    }
                  }}
                >
                  {"Edit"}
                </DefaultButton>
                <DefaultButton
                  width={80}
                  disabled={
                    item.userId === adminGuid &&
                    item.name === defaultSubjectName
                  }
                  color="danger"
                  onClick={() => {
                    if (onRemoveClick) {
                      onRemoveClick(item);
                    }
                  }}
                >
                  {"Remove"}
                </DefaultButton>
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </TableComponent>
  );
}
