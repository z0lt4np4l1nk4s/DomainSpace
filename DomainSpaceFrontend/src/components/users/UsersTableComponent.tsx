import { DefaultButton, TableComponent } from "../common";
import { RoleEnum, UserModel } from "../../types";

export default function UsersTableComponent({
  items,
  onMakeModeratorClick,
  onRemoveClick,
}: {
  items: UserModel[];
  onMakeModeratorClick: (item: UserModel) => void;
  onRemoveClick: (item: UserModel) => void;
}) {
  return (
    <TableComponent>
      <thead>
        <tr>
          <th scope="col" className="col-2">
            {"First Name"}
          </th>
          <th scope="col" className="col-2">
            {"Last Name"}
          </th>
          <th scope="col" className="col-4">
            {"Email"}
          </th>
          <th scope="col" className="col-2">
            {"Role"}
          </th>
          <th scope="col" className="col-4">
            {"Actions"}
          </th>
        </tr>
      </thead>
      <tbody>
        {items.map((item) => (
          <tr key={item.id}>
            <td>{item.firstName}</td>
            <td>{item.lastName}</td>
            <td>{item.email}</td>
            <td>{(item.roles || []).join(",")}</td>
            <td>
              <div className="btn-group gap-1">
                <DefaultButton
                  width={145}
                  color="primary"
                  onClick={() => {
                    onMakeModeratorClick(item);
                  }}
                  disabled={
                    (item.roles?.length || 0) !== 1 ||
                    item.roles![0].toLowerCase() !== RoleEnum.User.toLowerCase()
                  }
                >
                  {"Make moderator"}
                </DefaultButton>
                <DefaultButton
                  width={80}
                  color="danger"
                  onClick={() => {
                    onRemoveClick(item);
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
