import DefaultNavBar from "./DefaultNavBar";
import { UserDataService } from "../../services";
import { RoleEnum } from "../../types";
import AdminNavBar from "./AdminNavBar";
import ModeratorNavBar from "./ModeratorNavBar";
import UserNavBar from "./UserNavBar";

export default function NavBar() {
  if (UserDataService.isInRole(RoleEnum.Admin)) {
    return <AdminNavBar />;
  }

  if (UserDataService.isInRole(RoleEnum.Moderator)) {
    return <ModeratorNavBar />;
  }

  if (UserDataService.isInRole(RoleEnum.User)) {
    return <UserNavBar />;
  }

  return <DefaultNavBar />;
}
