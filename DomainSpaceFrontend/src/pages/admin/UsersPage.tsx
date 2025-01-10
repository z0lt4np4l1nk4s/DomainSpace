import { useEffect, useState } from "react";
import {
  FooterComponent,
  Loader,
  ModalComponent,
  NavBar,
  NoItemsComponent,
  PaginationComponent,
  UsersFilterComponent,
  UsersTableComponent,
} from "../../components";
import { PagedList, RoleEnum, UserFilter, UserModel } from "../../types";
import { AuthService, UserService } from "../../services";
import { ToastUtil } from "../../utils";

const userService = new UserService();
const authService = new AuthService();

export default function UsersPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [showChangeRoleModal, setShowChangeRoleModal] = useState(false);
  const [showRemoveModal, setShowRemoveModal] = useState(false);
  const [selectedUser, setSelectedUser] = useState<UserModel>();
  const [usersFilter, setUsersFilter] = useState<UserFilter>({});
  const [usersPaged, setUsersPaged] = useState<PagedList<UserModel>>();

  useEffect(() => {
    setIsLoading(true);
    const getUsersPagedAsync = async () => {
      const response = await userService.getPagedAsync(usersFilter);

      if (response.isSuccess) {
        setUsersPaged(response.result!);
      }

      setIsLoading(false);
    };

    getUsersPagedAsync();
  }, [usersFilter]);

  const changeRoleAsync = async () => {
    const response = await authService.changeRoleAsync({
      userId: selectedUser?.id!,
      role: RoleEnum.Moderator,
    });

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Role changed successfully.");
      setShowChangeRoleModal(false);
      setUsersFilter({ ...usersFilter });
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  const removeUserAsync = async () => {
    const response = await userService.deleteAsync(selectedUser?.id!);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("User removed successfully.");
      setShowRemoveModal(false);
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <div className="container mt-3">
            <h2 className="mb-3">Users</h2>
            <div className="d-flex justify-content-end">
              <UsersFilterComponent
                filter={usersFilter}
                onChange={(filter) => {
                  setUsersFilter(filter);
                }}
              />
            </div>
            <UsersTableComponent
              onMakeModeratorClick={(item: UserModel) => {
                setSelectedUser(item);
                setShowChangeRoleModal(true);
              }}
              onRemoveClick={(item: UserModel) => {
                setSelectedUser(item);
                setShowRemoveModal(true);
              }}
              items={usersPaged?.items || []}
            />
            {(usersPaged?.items || []).length === 0 && <NoItemsComponent />}
            {(usersPaged?.lastPage || 1) > 1 && (
              <PaginationComponent
                onPageChange={(page) => {
                  setUsersFilter({ ...usersFilter, pageNumber: page });
                }}
                currentPage={usersPaged?.pageNumber || 1}
                pageCount={usersPaged?.lastPage || 1}
              />
            )}
          </div>
        </Loader>
        <ModalComponent
          title={"Change role"}
          show={showChangeRoleModal}
          onShowChange={setShowChangeRoleModal}
          onConfirm={changeRoleAsync}
        >
          <p>{`Are you sure you want to promote ${selectedUser?.firstName} ${selectedUser?.lastName} (${selectedUser?.email}) to a moderator?`}</p>
        </ModalComponent>
        <ModalComponent
          title={"Remove"}
          show={showRemoveModal}
          onShowChange={setShowRemoveModal}
          onConfirm={removeUserAsync}
        >
          <p>{`Are you sure you want to remove the user ${selectedUser?.firstName} ${selectedUser?.lastName} (${selectedUser?.email}) ?`}</p>
        </ModalComponent>
      </div>
      <FooterComponent />
    </div>
  );
}
