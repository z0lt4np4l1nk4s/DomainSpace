import DefaultNavBar from "./DefaultNavBar";
import { TokenService } from "../../services";
import { RoleEnum } from "../../types";
import AdminNavBar from "./AdminNavBar";
import ModeratorNavBar from "./ModeratorNavBar";
import UserNavBar from "./UserNavBar";

export default function NavBar() {
  if (TokenService.isInRole(RoleEnum.Admin)) {
    return <AdminNavBar />;
  }

  if (TokenService.isInRole(RoleEnum.Moderator)) {
    return <ModeratorNavBar />;
  }

  if (TokenService.isInRole(RoleEnum.User)) {
    return <UserNavBar />;
  }

  return <DefaultNavBar />;
}
